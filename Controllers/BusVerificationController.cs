using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BusMeal.API.Controllers.Resources;
using BusMeal.API.Core.IRepository;
using BusMeal.API.Core.Models;
using BusMeal.API.Helpers;
using BusMeal.API.Helpers.Params;
using Microsoft.AspNetCore.Mvc;

namespace BusMeal.API.Controllers
{
  [Route("api/[controller]")]
  public class BusVerificationController : Controller
  {
    private readonly IMapper mapper;
    private readonly IBusVerificationRepository busVerificationRepository;
    private readonly IUnitOfWork unitOfWork;

    public BusVerificationController(IMapper mapper, IBusVerificationRepository busVerificationRepository, IUnitOfWork unitOfWork)
    {
      this.mapper = mapper;
      this.busVerificationRepository = busVerificationRepository;
      this.unitOfWork = unitOfWork;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
      var busVerifications = await busVerificationRepository.GetAll();

      var result = mapper.Map<IEnumerable<ViewBusVerificationResource>>(busVerifications);

      return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetOne(int id)
    {
      var busVerification = await busVerificationRepository.GetOne(id);

      if (busVerification == null)
        return NotFound();

      var result = mapper.Map<BusOrderVerificationHeader, ViewBusVerificationResource>(busVerification);

      return Ok(result);
    }

    [HttpGet("paged")]
    public async Task<IActionResult> GetPagedBusOrderVerification([FromQuery]BusVerificationParams busVerificationParams)
    {
      var busVerifications = await busVerificationRepository.GetPagedBusOrderVerification(busVerificationParams);

      var result = mapper.Map<IEnumerable<ViewBusVerificationResource>>(busVerifications);

      Response.AddPagination(busVerifications.CurrentPage, busVerifications.PageSize, busVerifications.TotalCount, busVerifications.TotalPages);

      return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody]SaveBusVerificationResource busVerificationResource)
    {
      if (!ModelState.IsValid)
        return BadRequest(ModelState);

      var busVerification = mapper.Map<SaveBusVerificationResource, BusOrderVerificationHeader>(busVerificationResource);


      busVerificationRepository.Add(busVerification);

      if (await unitOfWork.CompleteAsync() == false)
      {
        throw new Exception(message: "Create new order failed on save");
      }

      busVerification = await busVerificationRepository.GetOne(busVerification.Id);

      var result = mapper.Map<BusOrderVerificationHeader, ViewBusVerificationResource>(busVerification);

      return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody]SaveBusVerificationResource busVerificationResource)
    {
      if (!ModelState.IsValid)
        return BadRequest(ModelState);

      var busVerification = await busVerificationRepository.GetOne(id);

      if (busVerification == null)
        return NotFound();

      busVerification = mapper.Map(busVerificationResource, busVerification);

      if (await unitOfWork.CompleteAsync() == false)
      {
        throw new Exception(message: $"Updating order with id: {id} failed on save");
      }

      busVerification = await busVerificationRepository.GetOne(busVerification.Id);

      var result = mapper.Map<BusOrderVerificationHeader, ViewBusVerificationResource>(busVerification);

      return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Remove(int id)
    {
      var busVerification = await busVerificationRepository.GetOne(id);

      if (busVerification == null)
        return NotFound();

      busVerificationRepository.Remove(busVerification);

      if (await unitOfWork.CompleteAsync() == false)
      {
        throw new Exception(message: $"Deleting order with id: {id} failed");
      }

      return Ok($"{id}");
    }
  }
}