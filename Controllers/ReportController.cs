using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BusMeal.API.Controllers.Resources;
using BusMeal.API.Core.IRepository;
using BusMeal.API.Helpers.Params;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BusMeal.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "Report.R, Administrator")]
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
        private readonly IMealOrderVerificationRepository mealOrderVerificationRepository;
        private IBusOrderVerificationRepository busOrderVerificationRepository;
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
        IBusOrderRepository busOrderRepository,
        IMealOrderVerificationRepository mealOrderVerificationRepository,
        IBusOrderVerificationRepository busOrderVerificationRepository
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
            this.mealOrderVerificationRepository = mealOrderVerificationRepository;
            this.busOrderVerificationRepository = busOrderVerificationRepository;
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

            var BusTime = mapper.Map<IEnumerable<ViewBusTimeResource>>(busTimes);

            object[] direction = new object[3];
            direction[0] = new Direction { id = 1, name = "Dormitory to Alcon 204" };
            direction[1] = new Direction { id = 2, name = "Alcon 204 to Dormitory" };
            direction[2] = new Direction { id = 3, name = "Night Bus" };

            return Ok(new
            {
                BusTime,
                direction
            });
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
        public async Task<IActionResult> GetMealOrderReport([FromQuery]MealOrderParams mealOrderParams)
        {

            var mealOrders = await mealOrderRepository.GetPagedMealOrder(mealOrderParams);
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

        [HttpGet("mealverification")]
        public async Task<IActionResult> GetMealVerificationReport([FromQuery]MealOrderVerificationParams mealVerificationParams)
        {

            var mealVerification = await mealOrderVerificationRepository.GetPagedMealOrderVerification(mealVerificationParams);
            var mealtypes = await mealtypeRepository.GetAll();

            var mealOrderResult = mapper.Map<IEnumerable<ViewMealOrderVerificationResource>>(mealVerification);
            var mealTypeResult = mapper.Map<IEnumerable<ViewMealTypeResource>>(mealtypes);

            return Ok(new
            {
                mealOrderResult,
                mealTypeResult
            });
        }

        [HttpGet("busorder")]
        public async Task<IActionResult> GetBusOrderReport([FromQuery]BusOrderParams busOrderParams)
        {

            var busOrder = await busOrderRepository.GetPagedBusOrder(busOrderParams);
            var departments = await departmentRepository.GetAll();
            var bustime = await busTimeRepository.GetAll();
            var dormitoryblock = await dormitoryBlockRepository.GetAll();

            var busOrderResult = mapper.Map<IEnumerable<ViewBusOrderResource>>(busOrder);
            var departmentResult = mapper.Map<IEnumerable<ViewDepartmentResource>>(departments);
            var bustimeResult = mapper.Map<IEnumerable<ViewBusTimeResource>>(bustime);
            var dormitoryblockResult = mapper.Map<IEnumerable<ViewDormitoryBlockResource>>(dormitoryblock);

            object[] direction = new object[3];
            direction[0] = new Direction { id = 1, name = "Dormitory to Alcon 204" };
            direction[1] = new Direction { id = 2, name = "Alcon 204 to Dormitory" };
            direction[2] = new Direction { id = 3, name = "Night Bus" };

            return Ok(new
            {
                busOrderResult,
                departmentResult,
                bustimeResult,
                direction,
                dormitoryblockResult
            }
          );
        }

        [HttpGet("busverification")]
        public async Task<IActionResult> GetBusVerificationReport([FromQuery]BusOrderVerificationParams busVerificationParams)
        {

            var busVerification = await busOrderVerificationRepository.GetPagedBusOrderVerification(busVerificationParams);
            var bustime = await busTimeRepository.GetAll();
            var dormitoryblock = await dormitoryBlockRepository.GetAll();

            var busVerificationResult = mapper.Map<IEnumerable<ViewBusOrderVerificationResource>>(busVerification);
            var bustimeResult = mapper.Map<IEnumerable<ViewBusTimeResource>>(bustime);
            var dormitoryblockResult = mapper.Map<IEnumerable<ViewDormitoryBlockResource>>(dormitoryblock);

            object[] direction = new object[3];
            direction[0] = new Direction { id = 1, name = "Dormitory to Alcon 204" };
            direction[1] = new Direction { id = 2, name = "Alcon 204 to Dormitory" };
            direction[2] = new Direction { id = 3, name = "Night Bus" };

            return Ok(new
            {
                busVerificationResult,
                bustimeResult,
                direction,
                dormitoryblockResult
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