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

    private readonly IDepartmentRepository departmentRepository;
    public ReportController(
    IMapper mapper,
    IDepartmentRepository departmenRepository,
    IMealtypeRepository mealtypeRepository
    )
    {
      this.departmentRepository = departmenRepository;
      this.mapper = mapper;
      this.mealtypeRepository = mealtypeRepository;
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

      var result = mapper.Map<IEnumerable<ViewDepartmentResource>>(mealtypes);

      return Ok(result);
    }
  }
}