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
    private readonly IDepartmentRepository departmentRepository;
    public ReportController(IMapper mapper, IDepartmentRepository departmenRepository)
    {
      this.departmentRepository = departmenRepository;
      this.mapper = mapper;
    }

    [HttpGet("department")]
    public async Task<IActionResult> GetAll()
    {
      var departments = await departmentRepository.GetAll();

      var result = mapper.Map<IEnumerable<ViewDepartmentResource>>(departments);

      return Ok(result);
    }
  }
}