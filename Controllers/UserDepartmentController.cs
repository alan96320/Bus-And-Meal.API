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

  public class UserDepartmentController : Controller
  {
    private readonly IMapper mapper;
    private readonly IUserDepartmentRepository userDepartmentRepository;
    private readonly IUnitOfWork unitOfWork;

    public UserDepartmentController(IMapper mapper, IUserDepartmentRepository userDepartmentRepository, IUnitOfWork unitOfWork)
    {
      this.mapper = mapper;
      this.userDepartmentRepository = userDepartmentRepository;
      this.unitOfWork = unitOfWork;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
      var userDepartments = await userDepartmentRepository.GetAll();

      var result = mapper.Map<IEnumerable<ViewUserDepartmentResource>>(userDepartments);

      return Ok(result);
    }

    // [Authorize(Roles = "User Department.R, Administrator")]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetOne(int id)
    {
      var userDepartment = await userDepartmentRepository.GetOne(id);

      if (userDepartment == null)
        return NotFound();

      var result = mapper.Map<UserDepartment, ViewUserDepartmentResource>(userDepartment);

      return Ok(result);
    }

    // [Authorize(Roles = "User Department.R, Administrator")]
    [HttpGet("paged")]
    public async Task<IActionResult> GetPagedUserDepartment([FromQuery]UserDepartmentParams userDepartmentParams)
    {
      var userDepartment = await userDepartmentRepository.GetPagedUserDepartment(userDepartmentParams);

      var result = mapper.Map<IEnumerable<ViewUserDepartmentResource>>(userDepartment);

      Response.AddPagination(userDepartment.CurrentPage, userDepartment.PageSize, userDepartment.TotalCount, userDepartment.TotalPages);

      return Ok(result);
    }

    // [Authorize(Roles = "User Department.W, Administrator")]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody]SaveUserDepartmentResource userDepartmentResource)
    {

      if (!ModelState.IsValid)
        return BadRequest(ModelState);

      var userDepartment = mapper.Map<SaveUserDepartmentResource, UserDepartment>(userDepartmentResource);

      userDepartmentRepository.Add(userDepartment);
      if (await unitOfWork.CompleteAsync() == false)
      {
        throw new Exception(message: "Create new userDepartment failed on save");
      }

      userDepartment = await userDepartmentRepository.GetOne(userDepartment.Id);

      var result = mapper.Map<UserDepartment, ViewUserDepartmentResource>(userDepartment);

      return Ok(result);
    }

    // [Authorize(Roles = "User Department.W, Administrator")]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody]SaveUserDepartmentResource userDepartmentResource)
    {
      if (!ModelState.IsValid)
        return BadRequest(ModelState);

      var userDepartment = await userDepartmentRepository.GetOne(id);

      if (userDepartment == null)
        return NotFound();

      userDepartment = mapper.Map(userDepartmentResource, userDepartment);

      if (await unitOfWork.CompleteAsync() == false)
      {
        throw new Exception(message: $"Updating userDepartment {id} failed on save");
      }

      userDepartment = await userDepartmentRepository.GetOne(userDepartment.Id);

      var result = mapper.Map<UserDepartment, ViewUserDepartmentResource>(userDepartment);

      return Ok(result);
    }

    // [Authorize(Roles = "User Department.W, Administrator")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> RemoveUserDepartment(int id)
    {
      var userDepartment = await userDepartmentRepository.GetOne(id);

      if (userDepartment == null)
        return NotFound();

      userDepartmentRepository.Remove(userDepartment);

      if (await unitOfWork.CompleteAsync() == false)
      {
        throw new Exception(message: $"Deleting user department with id: {id} failed");
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