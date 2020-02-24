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
  public class BusOrderController : Controller
  {
    private readonly IMapper mapper;
    private readonly IBusOrderRepository busOrderRepository;
    private readonly IUnitOfWork unitOfWork;

    public BusOrderController(IMapper mapper, IBusOrderRepository busOrderRepository, IUnitOfWork unitOfWork)
    {
      this.mapper = mapper;
      this.busOrderRepository = busOrderRepository;
      this.unitOfWork = unitOfWork;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
      var busOrders = await busOrderRepository.GetAll();

      var result = mapper.Map<IEnumerable<ViewBusOrderResource>>(busOrders);

      return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetActionResult(int id)
    {
      var busOrder = await busOrderRepository.GetOne(id);

      if (busOrder == null)
        return NotFound();

      var result = mapper.Map<BusOrderEntryHeader, ViewBusOrderResource>(busOrder);

      return Ok(result);
    }

    [HttpGet("paged")]
    public async Task<IActionResult> GetPagedBusOrder([FromQuery]BusOrderParams busOrderParams)
    {
      var busOrders = await busOrderRepository.GetPagedBusOrder(busOrderParams);

      var result = mapper.Map<IEnumerable<ViewBusOrderResource>>(busOrders);

      Response.AddPagination(busOrders.CurrentPage, busOrders.PageSize, busOrders.TotalCount, busOrders.TotalPages);

      return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody]SaveBusOrderResource busOrderResource)
    {
      if (!ModelState.IsValid)
        return BadRequest(ModelState);

      var busOrder = mapper.Map<SaveBusOrderResource, BusOrderEntryHeader>(busOrderResource);

      busOrderRepository.Add(busOrder);

      if (await unitOfWork.CompleteAsync() == false)
      {
        throw new Exception(message: "Create new order failed on save");
      }

      busOrder = await busOrderRepository.GetOne(busOrder.Id);

      var result = mapper.Map<BusOrderEntryHeader, ViewBusOrderResource>(busOrder);

      return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody]SaveBusOrderResource busOrderResource)
    {
      if (!ModelState.IsValid)
        return BadRequest(ModelState);

      var busOrder = await busOrderRepository.GetOne(id);

      if (busOrder == null)
        return NotFound();

      busOrder = mapper.Map(busOrderResource, busOrder);

      if (await unitOfWork.CompleteAsync() == false)
      {
        throw new Exception(message: $"Updating order with id: {id} failed on save");
      }

      busOrder = await busOrderRepository.GetOne(busOrder.Id);

      var result = mapper.Map<BusOrderEntryHeader, ViewBusOrderResource>(busOrder);

      return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Remove(int id)
    {
      var busOrder = await busOrderRepository.GetOne(id);

      if (busOrder == null)
        return NotFound();

      busOrderRepository.Remove(busOrder);

      if (await unitOfWork.CompleteAsync() == false)
      {
        throw new Exception(message: $"Deleting order with id: {id} failed");
      }

      return Ok($"{id}");
    }

  }
}