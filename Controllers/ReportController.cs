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
    public IMealtypeRepository mealtypeRepository;
    private readonly IEmployeeRepository employeeRepository;
    private IDormitoryBlockRepository dormitoryBlockRepository;
    private readonly IMealVendorRepository mealVendorRepository;
    private IBusTimeRepository busTimeRepository;
    private readonly ICounterRepository counterRepository;
    private IUserRepository userRepository;
    private readonly IMealOrderRepository mealOrderRepository;
    private IBusOrderRepository busOrderRepository;
    private readonly IDepartmentRepository departmentRepository;
    public ReportController(
    IMapper mapper,
    IDepartmentRepository departmenRepository,
    IMealtypeRepository mealtypeRepository,
    IEmployeeRepository employeeRepository,
    IDormitoryBlockRepository dormitoryBlockRepository,
    IMealVendorRepository mealVendorRepository,
    IBusTimeRepository busTimeRepository,
    ICounterRepository counterRepository,
    IUserRepository userRepository,
    IMealOrderRepository mealOrderRepository,
    IBusOrderRepository busOrderRepository
    )
    {
      this.departmentRepository = departmenRepository;
      this.mapper = mapper;
      this.mealtypeRepository = mealtypeRepository;
      this.employeeRepository = employeeRepository;
      this.dormitoryBlockRepository = dormitoryBlockRepository;
      this.mealVendorRepository = mealVendorRepository;
      this.busTimeRepository = busTimeRepository;
      this.counterRepository = counterRepository;
      this.userRepository = userRepository;
      this.mealOrderRepository = mealOrderRepository;
      this.busOrderRepository = busOrderRepository;
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

      var result = mapper.Map<IEnumerable<ViewEmployeeResource>>(employees);

      return Ok(result);
    }

    [HttpGet("dormitoryblock")]
    public async Task<IActionResult> GetDormitoryBlockReport()
    {
      var dormitoryblocks = await dormitoryBlockRepository.GetAll();

      var result = mapper.Map<IEnumerable<ViewDormitoryBlockResource>>(dormitoryblocks);

      return Ok(result);
    }

    [HttpGet("mealvendor")]
    public async Task<IActionResult> GetMealVendorReport()
    {
      var mealVendors = await mealVendorRepository.GetAll();

      var result = mapper.Map<IEnumerable<ViewMealVendorResource>>(mealVendors);

      return Ok(result);
    }

    [HttpGet("bustime")]
    public async Task<IActionResult> GetBusTimeReport()
    {
      var busTimes = await busTimeRepository.GetAll();

      var result = mapper.Map<IEnumerable<ViewBusTimeResource>>(busTimes);

      return Ok(result);
    }

    [HttpGet("counter")]
    public async Task<IActionResult> GetCounterReport()
    {
      var counters = await counterRepository.GetAll();

      var result = mapper.Map<IEnumerable<ViewCounterResource>>(counters);

      return Ok(result);
    }

    [HttpGet("user")]
    public async Task<IActionResult> GetUserReport()
    {
      var users = await userRepository.GetAll();

      var result = mapper.Map<IEnumerable<ViewUserResource>>(users);

      return Ok(result);
    }

    [HttpGet("mealorder")]
    public async Task<IActionResult> GetMealOrderReport()
    {

      var mealOrders = await mealOrderRepository.GetAll();
      var departments = await departmentRepository.GetAll();
      var mealtypes = await mealtypeRepository.GetAll();

      var mealOrderResult = mapper.Map<IEnumerable<ViewMealOrderResource>>(mealOrders);
      var departmentResult = mapper.Map<IEnumerable<ViewDepartmentResource>>(departments);
      var mealTypeResult = mapper.Map<IEnumerable<ViewMealTypeResource>>(mealtypes);

      return Ok(new
      {
        mealOrderResult,
        departmentResult,
        mealTypeResult
      });
    }

    [HttpGet("busorder")]
    public async Task<IActionResult> GetBusOrderReport()
    {

      var busOrder = await busOrderRepository.GetAll();
      var departments = await departmentRepository.GetAll();
      var bustime = await busTimeRepository.GetAll();

      var busOrderResult = mapper.Map<IEnumerable<ViewBusOrderResource>>(busOrder);
      var departmentResult = mapper.Map<IEnumerable<ViewDepartmentResource>>(departments);
      var bustimeResult = mapper.Map<IEnumerable<ViewBusTimeResource>>(bustime);

      object[] direction = new object[3];
      direction[0] = new Direction { id = 1, name = "Office to Dormitory" };
      direction[1] = new Direction { id = 2, name = "Dormitory to Office" };
      direction[2] = new Direction { id = 3, name = "Night Bus" };

      return Ok(new
      {
        busOrderResult,
        departmentResult,
        bustimeResult,
        direction
      }
    );
    }

    public class Direction
    {
      public int id { get; set; }
      public string name { get; set; }
    }
  }
}