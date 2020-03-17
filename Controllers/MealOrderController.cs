using System.Linq;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BusMeal.API.Controllers.Resources;
using BusMeal.API.Core.IRepository;
using BusMeal.API.Core.Models;
using Microsoft.AspNetCore.Mvc;
using BusMeal.API.Helpers.Params;
using BusMeal.API.Helpers;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace BusMeal.API.Controllers
{
  [Route("api/[controller]")]
  public class MealOrderController : Controller
  {
    private readonly IMapper mapper;
    private readonly IMealOrderRepository mealOrderRepository;
    private readonly IUnitOfWork unitOfWork;
    private IUserRepository userRepository;
    private readonly IMealOrderVerificationRepository orderVerificationRepository;

    public MealOrderController(IMapper mapper,
    IMealOrderRepository mealOrderRepository,
    IUserRepository userRepository,
    IUnitOfWork unitOfWork,
    IMealOrderVerificationRepository orderVerificationRepository
    )
    {
      this.mapper = mapper;
      this.mealOrderRepository = mealOrderRepository;
      this.unitOfWork = unitOfWork;
      this.userRepository = userRepository;
      this.orderVerificationRepository = orderVerificationRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
      var userId = getUserId();
      var mealOrders = await mealOrderRepository.GetAll(userId);

      var result = mapper.Map<IEnumerable<ViewMealOrderResource>>(mealOrders);

      return Ok(result);
    }

    // [Authorize(Roles = "Meal Order.R, Administrator")]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetOne(int id)
    {
      var userId = getUserId();
      var mealOrder = await mealOrderRepository.GetOne(id, userId);

      if (mealOrder == null)
        return NotFound();

      var result = mapper.Map<MealOrder, ViewMealOrderResource>(mealOrder);

      return Ok(result);
    }

    // [Authorize(Roles = "Meal Order.R, Administrator")]
    [HttpGet("paged")]
    public async Task<IActionResult> GetPagedMealOrderEntryHeader([FromQuery]MealOrderParams mealOrderParams)
    {
      var userId = getUserId();
      var mealOrders = await mealOrderRepository.GetPagedMealOrder(mealOrderParams, userId);

      var result = mapper.Map<IEnumerable<ViewMealOrderResource>>(mealOrders);

      Response.AddPagination(mealOrders.CurrentPage, mealOrders.PageSize, mealOrders.TotalCount, mealOrders.TotalPages);

      return Ok(result);
    }

    // [Authorize(Roles = "Meal Order.W, Administrator")]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody]SaveMealOrderResource mealOrderResource)
    {
      if (!ModelState.IsValid)
        return BadRequest(ModelState);

      var mealOrder = mapper.Map<SaveMealOrderResource, MealOrder>(mealOrderResource);


      mealOrderRepository.Add(mealOrder);

      if (await unitOfWork.CompleteAsync() == false)
      {
        throw new Exception(message: "Create new order failed on save");
      }

      mealOrder = await mealOrderRepository.GetOne(mealOrder.Id);

      var result = mapper.Map<MealOrder, ViewMealOrderResource>(mealOrder);

      return Ok(result);

    }

    // [Authorize(Roles = "Meal Order.W, Administrator")]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody]SaveMealOrderResource mealOrderResource)
    {
      if (!ModelState.IsValid)
        return BadRequest(ModelState);

      var mealOrder = await mealOrderRepository.GetOne(id);

      if (mealOrder == null)
        return NotFound();

      if (mealOrder.IsReadyToCollect == true)
      {
        return BadRequest("Can't edit the record since it marked as ready to collect");
      }

      mealOrder = mapper.Map(mealOrderResource, mealOrder);

      if (await unitOfWork.CompleteAsync() == false)
      {
        throw new Exception(message: $"Updating order with id: {id} failed on save");
      }

      mealOrder = await mealOrderRepository.GetOne(mealOrder.Id);

      var result = mapper.Map<MealOrder, ViewMealOrderResource>(mealOrder);

      return Ok(result);
    }

    // [Authorize(Roles = "Meal Order.W, Administrator")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Remove(int id)
    {
      var mealOrder = await mealOrderRepository.GetOne(id);
      if (mealOrder == null)
        return NotFound();

      if (mealOrder.IsReadyToCollect == true)
      {
        return BadRequest("Can't delete the record since it marked as ready to collect");
      }

      var userId = getUserId();
      if (userId < 0)
        return BadRequest("You are not authorize to delete the record");

      var user = await userRepository.GetOne(userId);
      if (user.AdminStatus != true)
      {
        if (mealOrder.UserId != userId)
          return BadRequest("You are not authorize to delete the record");
      }

      if (mealOrder.MealOrderVerificationId != null)
      {
        var verificationId = mealOrder.MealOrderVerificationId;
        var orderVerification = await orderVerificationRepository.GetOne(verificationId.Value);
        if (orderVerification.IsClosed == true)
          return BadRequest("The order is already closed");
      }

      if (mealOrder.IsReadyToCollect == true)
        return BadRequest("Can not delete the order which is already confirm as ready to collect");

      mealOrderRepository.Remove(mealOrder);

      if (await unitOfWork.CompleteAsync() == false)
      {
        throw new Exception(message: $"Deleting order with id: {id} failed");
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