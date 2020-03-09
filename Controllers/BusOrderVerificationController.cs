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
  public class BusOrderVerificationController : Controller
  {
    private readonly IMapper mapper;
    private readonly IBusOrderVerificationRepository busOrderVerificationRepository;
    private readonly IUnitOfWork unitOfWork;
    private readonly IBusOrderRepository busOrderRepository;    

    public BusOrderVerificationController(
      IMapper mapper, 
      IBusOrderVerificationRepository busOrderVerificationRepository,
      IUnitOfWork unitOfWork,
      IBusOrderRepository busOrderRepository)
    {
      this.mapper = mapper;
      this.busOrderVerificationRepository = busOrderVerificationRepository;
      this.unitOfWork = unitOfWork;
      this.busOrderRepository = busOrderRepository;      
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
      var busOrderVerifications = await busOrderVerificationRepository.GetAll();

      var result = mapper.Map<IEnumerable<ViewBusOrderVerificationResource>>(busOrderVerifications);

      return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetOne(int id)
    {
      var busOrderVerification = await busOrderVerificationRepository.GetOne(id);

      if (busOrderVerification == null)
        return NotFound();

      var result = mapper.Map<BusOrderVerification, ViewBusOrderVerificationResource>(busOrderVerification);

      return Ok(result);
    }

    [HttpGet("paged")]
    public async Task<IActionResult> GetPagedBusOrderVerification([FromQuery]BusOrderVerificationParams busOrderVerificationParams)
    {
      var busOrderVerifications = await busOrderVerificationRepository.GetPagedBusOrderVerification(busOrderVerificationParams);

      var result = mapper.Map<IEnumerable<ViewBusOrderVerificationResource>>(busOrderVerifications);

      Response.AddPagination(busOrderVerifications.CurrentPage, busOrderVerifications.PageSize, busOrderVerifications.TotalCount, busOrderVerifications.TotalPages);

      return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody]SaveBusOrderVerificationResource busOrderVerificationResource)
    {

      if (!ModelState.IsValid)
        return BadRequest(ModelState);

      var OrderLists = busOrderVerificationResource.OrderList;

      var busOrderVerification = mapper.Map<SaveBusOrderVerificationResource, BusOrderVerification>(busOrderVerificationResource);

      foreach (int item in OrderLists)
      {
        var Order = await busOrderRepository.GetOne(item);
        Order.BusOrderVerification = busOrderVerification;
      }

      busOrderVerificationRepository.Add(busOrderVerification);

      if (await unitOfWork.CompleteAsync() == false)
      {
        throw new Exception(message: "Create new order failed on save");
      }

      busOrderVerification = await busOrderVerificationRepository.GetOne(busOrderVerification.Id);

      var result = mapper.Map<BusOrderVerification, ViewBusOrderVerificationResource>(busOrderVerification);

      return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody]SaveBusOrderVerificationResource busOrderVerificationResource)
    {
      if (!ModelState.IsValid)
        return BadRequest(ModelState);

      var busOrderVerification = await busOrderVerificationRepository.GetOne(id);

      if (busOrderVerification == null)
        return NotFound();

      var OrderLists = busOrderVerificationResource.OrderList;

      busOrderVerification = mapper.Map(busOrderVerificationResource, busOrderVerification);

      foreach (int item in OrderLists)
      {
        var Order = await busOrderRepository.GetOne(item);
        Order.BusOrderVerification = busOrderVerification;
      }

      if (await unitOfWork.CompleteAsync() == false)
      {
        throw new Exception(message: $"Updating order with id: {id} failed on save");
      }

      busOrderVerification = await busOrderVerificationRepository.GetOne(busOrderVerification.Id);

      var result = mapper.Map<BusOrderVerification, ViewBusOrderVerificationResource>(busOrderVerification);

      return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Remove(int id)
    {
      var busOrderVerification = await busOrderVerificationRepository.GetOne(id);

      if (busOrderVerification == null)
        return NotFound();

      busOrderVerificationRepository.Remove(busOrderVerification);

      if (await unitOfWork.CompleteAsync() == false)
      {
        throw new Exception(message: $"Deleting order with id: {id} failed");
      }

      return Ok($"{id}");
    }
  }
}