using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BusMeal.API.Controllers.Resources;
using BusMeal.API.Core.Models;
using BusMeal.API.Helpers;
using Microsoft.AspNetCore.Mvc;
using BusMeal.API.Core.IRepository;
using BusMeal.API.Helpers.Params;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace BusMeal.API.Controllers
{
  [Route("api/[controller]")]

  public class UserController : Controller
  {
    private readonly IMapper mapper;
    private readonly IUserRepository userRepository;
    private IModuleRightsRepository moduleRightsRepository;
    private readonly IUserModuleRightsRepository userModuleRightsRepository;
    private readonly IUnitOfWork unitOfWork;

    public UserController(
      IMapper mapper,
      IUserRepository userRepository,
      IUnitOfWork unitOfWork,
      IModuleRightsRepository moduleRightsRepository,
      IUserModuleRightsRepository userModuleRightsRepository
      )
    {
      this.mapper = mapper;
      this.userRepository = userRepository;
      this.moduleRightsRepository = moduleRightsRepository;
      this.userModuleRightsRepository = userModuleRightsRepository;
      this.unitOfWork = unitOfWork;
    }

    // [Authorize(Roles = "Administrator")]
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
      var users = await userRepository.GetAll();

      var result = mapper.Map<IEnumerable<ViewUserResource>>(users);

      return Ok(result);
    }

    // [Authorize(Roles = "Administrator")]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetOne(int id)
    {
      var user = await userRepository.GetOne(id);

      if (user == null)
        return NotFound();

      var result = mapper.Map<Core.Models.User, ViewUserResource>(user);

      return Ok(result);
    }

    // [Authorize(Roles = "Administrator")]
    [HttpGet("paged")]
    public async Task<IActionResult> GetPagedUser([FromQuery]UserParams userParams)
    {
      var user = await userRepository.GetPagedUsers(userParams);

      var result = mapper.Map<IEnumerable<ViewUserResource>>(user);

      Response.AddPagination(user.CurrentPage, user.PageSize, user.TotalCount, user.TotalPages);

      return Ok(result);
    }

    // [Authorize(Roles = "Administrator")]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] AddUserResource userResource)
    {
      if (!ModelState.IsValid)
        return BadRequest(ModelState);

      var user = mapper.Map<Core.Models.User>(userResource);

      // Add user
      userRepository.Add(user, userResource.Password);

      // Copy all right to module right
      var rightLists = await moduleRightsRepository.GetAll();
      foreach (ModuleRight list in rightLists)
      {
        var userModuleRights = new UserModuleRight
        {
          ModuleRightsId = list.Id,
          UserId = user.Id,
          Read = false,
          Write = false
        };

        var saveUserModule = mapper.Map<UserModuleRight>(userModuleRights);

        userModuleRightsRepository.Add(saveUserModule);
      }

      if (await unitOfWork.CompleteAsync() == false)
      {
        throw new Exception(message: "Create new user fail on save");
      }

      user = await userRepository.GetOne(user.Id);

      var result = mapper.Map<Core.Models.User, ViewUserResource>(user);

      return Ok(result);
    }

    // [Authorize(Roles = "Administrator")]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody]SaveUserResource userResource)
    {
      if (!ModelState.IsValid)
        return BadRequest(ModelState);

      var user = await userRepository.GetOne(id);

      if (user == null)
        return NotFound();

      user = mapper.Map(userResource, user);

      if (await unitOfWork.CompleteAsync() == false)
      {
        throw new Exception(message: $"Updating user with id: {id} failed on save");
      }

      user = await userRepository.GetOne(user.Id);

      var result = mapper.Map<Core.Models.User, ViewUserResource>(user);

      return Ok(result);
    }

    // [Authorize(Roles = "Administrator")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> RemoveUser(int id)
    {
      var user = await userRepository.GetOne(id);

      if (user == null)
        return NotFound();

      userRepository.Remove(user);

      if (await unitOfWork.CompleteAsync() == false)
      {
        throw new Exception(message: $"Deleting user with id: {id} failed");
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