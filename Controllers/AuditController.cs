using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BusMeal.API.Controllers.Resources;
using BusMeal.API.Core.IRepository;
using BusMeal.API.Core.Models;
using BusMeal.API.Helpers;
using BusMeal.API.Helpers.Params;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BusMeal.API.Controllers
{
  [Route("api/[controller]")]
  public class AuditController : Controller
  {
    private IMapper mapper;
    private IAuditRepository auditRepository;
    private IUnitOfWork unitOfWork;

    public AuditController(IMapper mapper, IAuditRepository auditRepository, IUnitOfWork unitOfWork)
    {
      this.mapper = mapper;
      this.auditRepository = auditRepository;
      this.unitOfWork = unitOfWork;
    }

    [Authorize(Roles = "Administrator")]
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
      var audit = await auditRepository.GetAll();

      var result = mapper.Map<IEnumerable<ViewAuditResource>>(audit);

      return Ok(result);
    }

    [Authorize(Roles = "Administrator")]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetOne(int id)
    {
      var audit = await auditRepository.GetOne(id);

      if (audit == null)
        return NotFound();

      var result = mapper.Map<Audit, ViewAuditResource>(audit);

      return Ok(result);
    }

    [Authorize(Roles = "Administrator")]
    [HttpGet("paged")]
    public async Task<IActionResult> GetPagedAudit([FromQuery]AuditParams auditParams)
    {
      var audits = await auditRepository.GetPagedAudit(auditParams);

      var result = mapper.Map<IEnumerable<ViewAuditResource>>(audits);

      Response.AddPagination(audits.CurrentPage, audits.PageSize, audits.TotalCount, audits.TotalPages);

      return Ok(result);
    }

    [Authorize(Roles = "Administrator")]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody]SaveAuditResource auditResource)
    {
      if (!ModelState.IsValid)
        return BadRequest(ModelState);

      var audit = mapper.Map<SaveAuditResource, Audit>(auditResource);

      auditRepository.Add(audit);
      if (await unitOfWork.CompleteAsync() == false)
      {
        throw new Exception(message: $"Create new audit fail on save");
      }

      audit = await auditRepository.GetOne(audit.Id);
      var result = mapper.Map<Audit, ViewAuditResource>(audit);
      return Ok(result);

    }

    [Authorize(Roles = "Administrator")]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody]SaveAuditResource auditResource)
    {
      if (!ModelState.IsValid)
        return BadRequest(ModelState);

      var audit = await auditRepository.GetOne(id);

      if (audit == null)
        return NotFound();

      audit = mapper.Map(auditResource, audit);

      if (await unitOfWork.CompleteAsync() == false)
      {
        throw new Exception(message: $"Updating audit {id} failed on save");
      }

      audit = await auditRepository.GetOne(audit.Id);

      var result = mapper.Map<Audit, ViewAuditResource>(audit);

      return Ok(result);
    }

    [Authorize(Roles = "Administrator")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Removeaudit(int id)
    {
      var audit = await auditRepository.GetOne(id);

      if (audit == null)
        return NotFound();

      auditRepository.Remove(audit);

      if (await unitOfWork.CompleteAsync() == false)
      {
        throw new Exception(message: $"Deleting audit {id} failed");
      }

      return Ok($"{id}");
    }

    // FIXME : make me to be reuseable
    private int getUserId()
    {
      var idClaim = User.Claims.FirstOrDefault(c => c.Type.Equals("Id", StringComparison.InvariantCultureIgnoreCase));
      if (idClaim != null)
      {
        var id = int.Parse(idClaim.Value);
        return id;
      }
      return -1;
    }
  }
}