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
  public class UserModuleRightsController : Controller
  {
    private readonly IMapper mapper;
    private IUserModuleRightsRepository userModuleRightRepository;
    private IUnitOfWork unitOfWork;

    public UserModuleRightsController(IMapper mapper, IUserModuleRightsRepository userModuleRightRepository, IUnitOfWork unitOfWork)
    {
      this.mapper = mapper;
      this.userModuleRightRepository = userModuleRightRepository;
      this.unitOfWork = unitOfWork;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
      var usermodulerights = await userModuleRightRepository.GetAll();

      var result = mapper.Map<IEnumerable<ViewUserModuleRightsResource>>(usermodulerights);

      return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetOne(int id)
    {
      var usermoduleright = await userModuleRightRepository.GetOne(id);

      if (usermoduleright == null)
        return NotFound();

      var result = mapper.Map<UserModuleRights, ViewUserModuleRightsResource>(usermoduleright);

      return Ok(result);
    }

    [HttpGet("paged")]
    public async Task<IActionResult> GetPagedUserModuleRights([FromQuery]UserModuleRightsParams userModuleRightsParams)
    {
      var usermodulerights = await userModuleRightRepository.GetPagedUserModuleRights(userModuleRightsParams);

      var result = mapper.Map<IEnumerable<ViewUserModuleRightsResource>>(usermodulerights);

      Response.AddPagination(usermodulerights.CurrentPage, usermodulerights.PageSize, usermodulerights.TotalCount, usermodulerights.TotalPages);

      return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody]SaveUserModuleRightsResource userModuleRightsResource)
    {
      if (!ModelState.IsValid)
        return BadRequest(ModelState);

      var usermoduleright = mapper.Map<SaveUserModuleRightsResource, UserModuleRights>(userModuleRightsResource);

      userModuleRightRepository.Add(usermoduleright);

      if (await unitOfWork.CompleteAsync() == false)
      {
        throw new Exception(message: "Create new user module right failed on save");
      }

      usermoduleright = await userModuleRightRepository.GetOne(usermoduleright.Id);

      var result = mapper.Map<UserModuleRights, ViewUserModuleRightsResource>(usermoduleright);

      return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody]SaveUserModuleRightsResource userModuleRightsResource)
    {
      if (!ModelState.IsValid)
        return BadRequest(ModelState);

      var usermoduleright = await userModuleRightRepository.GetOne(id);

      if (usermoduleright == null)
        return NotFound();

      usermoduleright = mapper.Map(userModuleRightsResource, usermoduleright);

      if (await unitOfWork.CompleteAsync() == false)
      {
        throw new Exception(message: $"Updating user module right with id: {id} failed on save");
      }

      usermoduleright = await userModuleRightRepository.GetOne(usermoduleright.Id);

      var result = mapper.Map<UserModuleRights, ViewUserModuleRightsResource>(usermoduleright);

      return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> RemoveUserModuleRight(int id)
    {
      var usermoduleright = await userModuleRightRepository.GetOne(id);

      if (usermoduleright == null)
        return NotFound();

      userModuleRightRepository.Remove(usermoduleright);

      if (await unitOfWork.CompleteAsync() == false)
      {
        throw new Exception(message: $"Deleting user module right with id {id} failed");
      }

      return Ok($"{id}");
    }
  }
}