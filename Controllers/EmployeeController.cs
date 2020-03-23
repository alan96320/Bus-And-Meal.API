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

  public class EmployeeController : Controller
  {
    private readonly IMapper mapper;
    private readonly IEmployeeRepository employeeRepository;
    private readonly IUnitOfWork unitOfWork;
    public EmployeeController(IMapper mapper, IEmployeeRepository employeeRepository, IUnitOfWork unitOfWork)
    {
      this.mapper = mapper;
      this.employeeRepository = employeeRepository;
      this.unitOfWork = unitOfWork;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
      var employees = await employeeRepository.GetAll();

      var result = mapper.Map<IEnumerable<ViewEmployeeResource>>(employees);

      return Ok(result);
    }

    [Authorize(Roles = "Employee.R, Administrator")]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetOne(int id)
    {
      var employee = await employeeRepository.GetOne(id);

      if (employee == null)
        return NotFound();

      var result = mapper.Map<Employee, ViewEmployeeResource>(employee);

      return Ok(result);
    }

    [Authorize(Roles = "Employee.R, Administrator")]
    [HttpGet("paged")]
    public async Task<IActionResult> GetPagedEmployee([FromQuery]EmployeeParams employeeParams)
    {
      var employees = await employeeRepository.GetPagedEmployees(employeeParams);

      var result = mapper.Map<IEnumerable<ViewEmployeeResource>>(employees);

      Response.AddPagination(employees.CurrentPage, employees.PageSize, employees.TotalCount, employees.TotalPages);

      return Ok(result);
    }

    [Authorize(Roles = "Employee.W, Administrator")]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody]SaveEmployeeResource employeeResource)
    {
      if (!ModelState.IsValid)
        return BadRequest(ModelState);

      var employee = mapper.Map<SaveEmployeeResource, Employee>(employeeResource);

      employeeRepository.Add(employee);
      if (await unitOfWork.CompleteAsync() == false)
      {
        throw new Exception(message: $"Create new employee fail on save");
      }

      employee = await employeeRepository.GetOne(employee.Id);
      var result = mapper.Map<Employee, ViewEmployeeResource>(employee);
      return Ok(result);

    }

    [Authorize(Roles = "Employee.W, Administrator")]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody]SaveEmployeeResource employeeResource)
    {
      if (!ModelState.IsValid)
        return BadRequest(ModelState);

      var employee = await employeeRepository.GetOne(id);

      if (employee == null)
        return NotFound();

      employee = mapper.Map(employeeResource, employee);

      employeeRepository.Update(employee);

      if (await unitOfWork.CompleteAsync() == false)
      {
        throw new Exception(message: $"Updating employee {id} failed on save");
      }

      employee = await employeeRepository.GetOne(employee.Id);

      var result = mapper.Map<Employee, ViewEmployeeResource>(employee);

      return Ok(result);
    }

    [Authorize(Roles = "Employee.W, Administrator")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> RemoveEmployee(int id)
    {
      var employee = await employeeRepository.GetOne(id);

      if (employee == null)
        return NotFound();

      employeeRepository.Remove(employee);

      if (await unitOfWork.CompleteAsync() == false)
      {
        throw new Exception(message: $"Deleting employee failed");
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