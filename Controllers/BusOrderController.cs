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
  public class BusOrderController : Controller
  {
    private readonly IMapper mapper;
    private readonly IBusOrderRepository busOrderRepository;
    private readonly IUnitOfWork unitOfWork;

    public IBusOrderVerificationRepository busOrderVerificationRepository;

    public BusOrderController(IMapper mapper, IBusOrderRepository busOrderRepository, IUnitOfWork unitOfWork, IBusOrderVerificationRepository busOrderVerificationRepository)
    {
      this.mapper = mapper;
      this.busOrderRepository = busOrderRepository;
      this.unitOfWork = unitOfWork;
      this.busOrderVerificationRepository = busOrderVerificationRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
      var userId = getUserId();
      var busOrders = await busOrderRepository.GetAll(userId);

      var result = mapper.Map<IEnumerable<ViewBusOrderResource>>(busOrders);

      return Ok(result);
    }

    [Authorize(Roles = "Bus Order.R, Administrator")]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetActionResult(int id)
    {
      var userId = getUserId();
      var busOrder = await busOrderRepository.GetOne(id, userId);

      if (busOrder == null)
        return NotFound();

      var result = mapper.Map<BusOrder, ViewBusOrderResource>(busOrder);

      return Ok(result);
    }

    [Authorize(Roles = "Bus Order.R, Administrator")]
    [HttpGet("paged")]
    public async Task<IActionResult> GetPagedBusOrder([FromQuery]BusOrderParams busOrderParams)
    {
      var userId = getUserId();
      var busOrders = await busOrderRepository.GetPagedBusOrder(busOrderParams, userId);

      var result = mapper.Map<IEnumerable<ViewBusOrderResource>>(busOrders);

      Response.AddPagination(busOrders.CurrentPage, busOrders.PageSize, busOrders.TotalCount, busOrders.TotalPages);

      return Ok(result);
    }

    [Authorize(Roles = "Bus Order.W, Administrator")]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody]SaveBusOrderResource busOrderResource)
    {
      // TODO : cegah save jika sudah lewat waktu      
      if (!ModelState.IsValid)
        return BadRequest(ModelState);

      var busOrder = mapper.Map<SaveBusOrderResource, BusOrder>(busOrderResource);

      busOrderRepository.Add(busOrder);

      if (await unitOfWork.CompleteAsync() == false)
      {
        throw new Exception(message: "Create new order failed on save");
      }

      busOrder = await busOrderRepository.GetOne(busOrder.Id);

      var result = mapper.Map<BusOrder, ViewBusOrderResource>(busOrder);

      return Ok(result);
    }

    [Authorize(Roles = "Bus Order.W, Administrator")]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody]SaveBusOrderResource busOrderResource)
    {
      // TODO : cegah save jika sudah lewat waktu
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      var busOrder = await busOrderRepository.GetOne(id);

      if (busOrder == null)
      {
        return NotFound();
      }

      if (busOrder.UserId != busOrderResource.UserId)
      {
        busOrderResource.UserId = busOrder.UserId;
      }

      if (busOrder.IsReadyToCollect == true && busOrderResource.isReadyToCollect == true)
      {
        return BadRequest("Can't edit the record since it was ready to collect, please disable ready to collect then save and try again");
      }

      if (busOrder.IsReadyToCollect == false)
      {
        busOrder = mapper.Map(busOrderResource, busOrder);
      }

      if (busOrder.IsReadyToCollect == true && busOrderResource.isReadyToCollect == false)
      {
        if (busOrder.BusOrderVerificationId != null)
        {
          var busVerification = await busOrderVerificationRepository.GetOne(busOrder.BusOrderVerificationId.Value);
          if (busVerification.IsClosed == true)
          {
            return BadRequest("Can't edit the record since it was close in verification");
          }
        }
        busOrder.IsReadyToCollect = false;
      }

      if (await unitOfWork.CompleteAsync() == false)
      {
        throw new Exception(message: $"Updating order with id: {id} failed on save");
      }

      busOrder = await busOrderRepository.GetOne(busOrder.Id);

      var result = mapper.Map<BusOrder, ViewBusOrderResource>(busOrder);

      return Ok(result);
    }

    [Authorize(Roles = "Bus Order.W, Administrator")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Remove(int id)
    {
      var busOrder = await busOrderRepository.GetOne(id);

      if (busOrder == null)
      {
        return NotFound();
      }

      if (busOrder.IsReadyToCollect == true)
      {
        return BadRequest("Can't delete the record since it marked as ready to collect");
      }

      busOrderRepository.Remove(busOrder);

      if (await unitOfWork.CompleteAsync() == false)
      {
        throw new Exception(message: $"Deleting order failed");
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