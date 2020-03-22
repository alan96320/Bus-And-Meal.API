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
    [Authorize(Roles = "Counter.W, Administrator")]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] SaveCounterResource counterResource)
    {
      if (!ModelState.IsValid)
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
    [Authorize(Roles = "Counter.R, Administrator")]
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
    [Authorize(Roles = "Counter.W, Administrator")]
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
    [Authorize(Roles = "Counter.R, Administrator")]
    [HttpGet("paged")]
    public async Task<IActionResult> GetPagedcounter([FromQuery]CounterParams counterParams)
    {
      var counters = await counterRepository.GetPagedCounter(counterParams);

      var result = mapper.Map<IEnumerable<ViewCounterResource>>(counters);

      Response.AddPagination(counters.CurrentPage, counters.PageSize, counters.TotalCount, counters.TotalPages);

      return Ok(result);
    }

    [Authorize(Roles = "Counter.W, Administrator")]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody]SaveCounterResource counterResource)
    {
      if (!ModelState.IsValid)
        return BadRequest(ModelState);

      var counter = await counterRepository.GetOne(id);

      if (counter == null)
        return NotFound();

      counter = mapper.Map(counterResource, counter);

      counterRepository.Update(counter);

      if (await unitOfWork.CompleteAsync() == false)
      {
        throw new Exception(message: $"Updating counter {id} failed on save");
      }

      counter = await counterRepository.GetOne(counter.Id);

      var result = mapper.Map<Counter, ViewCounterResource>(counter);

      return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
      var counters = await counterRepository.GetAll();

      var result = mapper.Map<IEnumerable<ViewCounterResource>>(counters);

      return Ok(result);
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