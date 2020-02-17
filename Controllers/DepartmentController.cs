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

  public class DepartmentController : Controller
  {
    private readonly IMapper mapper;
    private readonly IDepartmentRepository departmentRepository;
    private readonly IUnitOfWork unitOfWork;

    public DepartmentController(IMapper mapper, IDepartmentRepository departmenRepository, IUnitOfWork unitOfWork)
    {
      this.unitOfWork = unitOfWork;
      this.departmentRepository = departmenRepository;
      this.mapper = mapper;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] SaveDepartmentResource departmentResource)
    {
      if (!ModelState.IsValid)
        return BadRequest(ModelState);

      var department = mapper.Map<SaveDepartmentResource, Department>(departmentResource);

      departmentRepository.Add(department);
      if (await unitOfWork.CompleteAsync() == false)
      {
        throw new Exception(message: $"Create new department failed on save");
      }

      department = await departmentRepository.GetOne(department.Id);
      var result = mapper.Map<Department, ViewDepartmentResource>(department);

      return Ok(result);

    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] SaveDepartmentResource departmentResource)
    {
      if (!ModelState.IsValid)
        return BadRequest(ModelState);

      var department = await departmentRepository.GetOne(id);


      if (department == null)
        return NotFound();

      department = mapper.Map(departmentResource, department);

      departmentRepository.Update(department);
      
      if (await unitOfWork.CompleteAsync() == false)
      {
        throw new Exception(message: $"Updating department {id} failed on save");
      }

      department = await departmentRepository.GetOne(department.Id);
      var result = mapper.Map<Department, ViewDepartmentResource>(department);

      return Ok(result);

    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetOne(int id)
    {
      var department = await departmentRepository.GetOne(id);

      if (department == null)
        return NotFound();

      var viewDepartmentResource = mapper.Map<Department, ViewDepartmentResource>(department);

      return Ok(viewDepartmentResource);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> RemoveDepartment(int id)
    {
      var department = await departmentRepository.GetOne(id);

      if (department == null)
        return NotFound();

      departmentRepository.Remove(department);
      if (await unitOfWork.CompleteAsync() == false)
      {
        throw new Exception(message: $"Deleting department {id} failed");
      }

      return Ok(id);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
      var departments = await departmentRepository.GetAll();

      var result = mapper.Map<IEnumerable<ViewDepartmentResource>>(departments);

      return Ok(result);
    }


    [HttpGet("paged")]
    public async Task<IActionResult> GetPagedDepartments([FromQuery] DepartmentParams departmentParams)
    {

      /*
        fill UserId Params untuk filtering department by UserId
            var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var userFromRepo = await _repo.GetUser(currentUserId);

            departmentParams.UserId = currentUserId;
      */

      var departments = await departmentRepository.GetPagedDepartments(departmentParams);

      var result = mapper.Map<IEnumerable<ViewDepartmentResource>>(departments);

      Response.AddPagination(departments.CurrentPage, departments.PageSize
                            , departments.TotalCount, departments.TotalPages);


      return Ok(result);
    }

  }

}