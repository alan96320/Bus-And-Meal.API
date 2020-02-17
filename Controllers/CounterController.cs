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
    public class CounterController : Controller
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;
        private readonly ICounterRepository counterRepository;

        public CounterController(IMapper mapper, ICounterRepository counterRepository, IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.counterRepository = counterRepository;
        }


        //Create new data Counter
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SaveCounterResource counterResource)
        {
            if(!ModelState.IsValid)
            return BadRequest(ModelState);

            var counter = mapper.Map<SaveCounterResource, Counter>(counterResource);
            counterRepository.Add(counter);
            if (await unitOfWork.CompleteAsync() == false)
            {
                throw new Exception(message: $"Create New Counter failed on Save");
            }
            counter = await counterRepository.GetOne(counter.Id);
            var result = mapper.Map<Counter, ViewCounterResource>(counter);

            return Ok(result);
        }

        //get data by ID for action after update
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne(int id)
        {
            var counter = await counterRepository.GetOne(id);

            if (counter == null)
                return NotFound();

            var result = mapper.Map<Counter, ViewCounterResource>(counter);

            return Ok(result);
        }

        //Delete data Counter
        [HttpDelete("{id}")]
        public async Task<IActionResult> Removecounter(int id)
        {
            var counter = await counterRepository.GetOne(id);

            if (counter == null)
                return NotFound();

            counterRepository.Remove(counter);

            if (await unitOfWork.CompleteAsync() == false)
            {
                throw new Exception(message: $"Deleting counter {id} failed");
            }

            return Ok($"{id}");
        }

        //get Data For pagination
        [HttpGet("paged")]
        public async Task<IActionResult> GetPagedcounter([FromQuery]CounterParams counterParams)
        {
            var counters = await counterRepository.GetPagedCounter(counterParams);

            var result = mapper.Map<IEnumerable<ViewCounterResource>>(counters);

            Response.AddPagination(counters.CurrentPage, counters.PageSize, counters.TotalCount, counters.TotalPages);

            return Ok(result);
        }

    }
}