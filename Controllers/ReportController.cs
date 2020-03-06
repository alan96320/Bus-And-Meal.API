using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BusMeal.API.Controllers.Resources;
using BusMeal.API.Core.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace BusMeal.API.Controllers
{
  [Route("api/[controller]")]
  public class ReportController : Controller
  {
    private readonly IMapper mapper;
    public IMealtypeRepository mealtypeRepository { get; }
    private readonly IEmployeeRepository employeeRepository;
    private IDormitoryBlockRepository dormitoryBlockRepository;
    private readonly IDepartmentRepository departmentRepository;
    public ReportController(
    IMapper mapper,
    IDepartmentRepository departmenRepository,
    IMealtypeRepository mealtypeRepository,
    IEmployeeRepository employeeRepository,
    IDormitoryBlockRepository dormitoryBlockRepository
    )
    {
      this.departmentRepository = departmenRepository;
      this.mapper = mapper;
      this.mealtypeRepository = mealtypeRepository;
      this.employeeRepository = employeeRepository;
      this.dormitoryBlockRepository = dormitoryBlockRepository;
    }

    [HttpGet("department")]
    public async Task<IActionResult> GetDepartmentReport()
    {
      var departments = await departmentRepository.GetAll();

      var result = mapper.Map<IEnumerable<ViewDepartmentResource>>(departments);

      return Ok(result);
    }

    [HttpGet("mealtype")]
    public async Task<IActionResult> GetMealTypeReport()
    {
      var mealtypes = await mealtypeRepository.GetAll();

      var result = mapper.Map<IEnumerable<ViewMealTypeResource>>(mealtypes);

      return Ok(result);
    }

    [HttpGet("employee")]
    public async Task<IActionResult> GetEmployeeReport()
    {
      var employees = await employeeRepository.GetAll();

      var result = mapper.Map<IEnumerable<ViewDepartmentResource>>(employees);

      return Ok(result);
    }

    [HttpGet("dormitoryblock")]
    public async Task<IActionResult> GetDormitoryBlockReport()
    {
      var dormitoryblocks = await dormitoryBlockRepository.GetAll();

      var result = mapper.Map<IEnumerable<ViewDepartmentResource>>(dormitoryblocks);

      return Ok(result);
    }
  }
}