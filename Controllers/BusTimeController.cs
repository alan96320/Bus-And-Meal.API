using System;
using System.Collections.Generic;
using System.Linq;
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
    public class BusTimeController : Controller
    {
        private readonly IMapper mapper;
        private readonly IBusTimeRepository busTimeRepository;
        private readonly IUnitOfWork unitOfWork;
        public BusTimeController(IMapper mapper, IBusTimeRepository busTimeRepository, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.busTimeRepository = busTimeRepository;
            this.unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var busTime = await busTimeRepository.GetAll();

            var result = mapper.Map<IEnumerable<ViewBusTimeResource>>(busTime);

            return Ok(result);
        }

        // [Authorize(Roles = "Bus Time.R, Administrator")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne(int id)
        {
            var busTime = await busTimeRepository.GetOne(id);

            if (busTime == null)
                return NotFound();

            var result = mapper.Map<BusTime, ViewBusTimeResource>(busTime);

            return Ok(result);
        }

        // [Authorize(Roles = "Bus Time.R, Administrator")]
        [HttpGet("paged")]
        public async Task<IActionResult> GetPagedBusTime([FromQuery]BusTimeParams busTimeParams)
        {
            var busTime = await busTimeRepository.GetPagedBusTimes(busTimeParams);

            var result = mapper.Map<IEnumerable<ViewBusTimeResource>>(busTime);

            Response.AddPagination(busTime.CurrentPage, busTime.PageSize, busTime.TotalCount, busTime.TotalPages);

            return Ok(result);
        }

        // [Authorize(Roles = "Bus Time.W, Administrator")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody]SaveBusTimeResource busTimeResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var busTime = mapper.Map<SaveBusTimeResource, BusTime>(busTimeResource);

            busTimeRepository.Add(busTime);
            if (await unitOfWork.CompleteAsync() == false)
            {
                throw new Exception(message: $"Create new bus time fail on save");
            }

            busTime = await busTimeRepository.GetOne(busTime.Id);
            var result = mapper.Map<BusTime, ViewBusTimeResource>(busTime);
            return Ok(result);

        }

        // [Authorize(Roles = "Bus Time.W, Administrator")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody]SaveBusTimeResource busTimeResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var busTime = await busTimeRepository.GetOne(id);

            if (busTime == null)
                return NotFound();

            busTime = mapper.Map(busTimeResource, busTime);

            busTimeRepository.Update(busTime);

            if (await unitOfWork.CompleteAsync() == false)
            {
                throw new Exception(message: $"Updating bus time {id} failed on save");
            }

            busTime = await busTimeRepository.GetOne(busTime.Id);

            var result = mapper.Map<BusTime, ViewBusTimeResource>(busTime);

            return Ok(result);
        }

        // [Authorize(Roles = "Bus Time.W, Administrator")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> RemovebusTime(int id)
        {
            var busTime = await busTimeRepository.GetOne(id);

            if (busTime == null)
                return NotFound();

            busTimeRepository.Remove(busTime);

            if (await unitOfWork.CompleteAsync() == false)
            {
                throw new Exception(message: $"Deleting bus time failed");
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