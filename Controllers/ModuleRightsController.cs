using System;
using System.Collections.Generic;
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
  public class ModuleRightsController : Controller
  {
    private readonly IMapper mapper;
    private readonly IModuleRightsRepository moduleRepository;
    private readonly IUnitOfWork unitOfWork;
    private IUserModuleRightsRepository userModuleRightsRepository;
    private readonly IUserRepository userRepository;

    public ModuleRightsController(
    IMapper mapper,
    IModuleRightsRepository moduleRepository,
    IUserModuleRightsRepository userModuleRightsRepository,
    IUserRepository userRepository,
    IUnitOfWork unitOfWork
    )
    {
      this.mapper = mapper;
      this.moduleRepository = moduleRepository;
      this.unitOfWork = unitOfWork;
      this.userModuleRightsRepository = userModuleRightsRepository;
      this.userRepository = userRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
      var modules = await moduleRepository.GetAll();

      var result = mapper.Map<IEnumerable<ViewModuleRightsResource>>(modules);

      return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetActionResult(int id)
    {
      var module = await moduleRepository.GetOne(id);

      if (module == null)
        return NotFound();

      var result = mapper.Map<ModuleRight, ViewModuleRightsResource>(module);

      return Ok(result);
    }

    [HttpGet("paged")]
    public async Task<IActionResult> GetPagedModuleRights([FromQuery]ModuleRightsParams moduleRightsParams)
    {
      var modules = await moduleRepository.GetPagedModuleRights(moduleRightsParams);

      var result = mapper.Map<IEnumerable<ViewModuleRightsResource>>(modules);

      Response.AddPagination(modules.CurrentPage, modules.PageSize, modules.TotalCount, modules.TotalPages);

      return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody]SaveModuleRightsResource moduleResource)
    {
      if (!ModelState.IsValid)
        return BadRequest(ModelState);

      var module = mapper.Map<SaveModuleRightsResource, ModuleRight>(moduleResource);

      // Add module right
      moduleRepository.Add(module);

      // Add module right to user module right
      var users = await userRepository.GetAll();

      foreach (User list in users)
      {
        var userModuleRights = new UserModuleRight
        {
          ModuleRightsId = module.Id,
          UserId = list.Id,
          Read = false,
          Write = false
        };

        var saveUserModule = mapper.Map<UserModuleRight>(userModuleRights);

        userModuleRightsRepository.Add(saveUserModule);
      }

      if (await unitOfWork.CompleteAsync() == false)
      {
        throw new Exception(message: "Create new Module failed on save");
      }

      module = await moduleRepository.GetOne(module.Id);

      var result = mapper.Map<ModuleRight, ViewModuleRightsResource>(module);

      return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody]SaveModuleRightsResource moduleResource)
    {
      if (!ModelState.IsValid)
        return BadRequest(ModelState);

      var module = await moduleRepository.GetOne(id);

      if (module == null)
        return NotFound();

      module = mapper.Map(moduleResource, module);

      if (await unitOfWork.CompleteAsync() == false)
      {
        throw new Exception(message: $"Updating moduleright with id: {id} failed on save");
      }

      module = await moduleRepository.GetOne(module.Id);

      var result = mapper.Map<ModuleRight, ViewModuleRightsResource>(module);

      return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> RemoveModule(int id)
    {
      var module = await moduleRepository.GetOne(id);

      if (module == null)
        return NotFound();

      moduleRepository.Remove(module);

      if (await unitOfWork.CompleteAsync() == false)
      {
        throw new Exception(message: $"Delete module with id: {id} failed on save");
      }

      return Ok($"{id}");
    }
  }
}