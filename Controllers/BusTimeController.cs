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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne(int id)
        {
            var busTime = await busTimeRepository.GetOne(id);

            if (busTime == null)
            return NotFound();

            var result = mapper.Map<BusTime, ViewBusTimeResource>(busTime);

            return Ok(result);
        }

        [HttpGet("paged")]
        public async Task<IActionResult> GetPagedBusTime([FromQuery]BusTimeParams busTimeParams)
        {
            var busTime = await busTimeRepository.GetPagedBusTimes(busTimeParams);

            var result = mapper.Map<IEnumerable<ViewBusTimeResource>>(busTime);

            Response.AddPagination(busTime.CurrentPage, busTime.PageSize, busTime.TotalCount, busTime.TotalPages);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]SaveBusTimeResource busTimeResource)
        {
            if (!ModelState.IsValid)
            return BadRequest(ModelState);

            var busTime = mapper.Map<SaveBusTimeResource, BusTime>(busTimeResource);

            busTimeRepository.Add(busTime);
            if (await unitOfWork.CompleteAsync() == false)
            {
                throw new Exception(message: $"Create new busTime fail on save");
            }

            busTime = await busTimeRepository.GetOne(busTime.Id);
            var result = mapper.Map<BusTime, ViewBusTimeResource>(busTime);
            return Ok(result);

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody]SaveBusTimeResource busTimeResource)
        {
            if (!ModelState.IsValid)
            return BadRequest(ModelState);

            var busTime = await busTimeRepository.GetOne(id);

            if (busTime == null)
            return NotFound();

            busTime = mapper.Map(busTimeResource, busTime);

            if (await unitOfWork.CompleteAsync() == false)
            {
                throw new Exception(message: $"Updating busTime {id} failed on save");
            }

            busTime = await busTimeRepository.GetOne(busTime.Id);

            var result = mapper.Map<BusTime, ViewBusTimeResource>(busTime);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemovebusTime(int id)
        {
            var busTime = await busTimeRepository.GetOne(id);

            if (busTime == null)
                return NotFound();

            busTimeRepository.Remove(busTime);

            if (await unitOfWork.CompleteAsync() == false)
            {
                throw new Exception(message: $"Deleting busTime {id} failed");
            }

            return Ok($"{id}");
        }


    }
}