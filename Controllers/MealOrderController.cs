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

namespace BusMeal.API.Controllers
{
  [Route("api/[controller]")]
  public class MealOrderController : Controller
  {
    private readonly IMapper mapper;
    private readonly IMealOrderRepository mealOrderRepository;
    private readonly IUnitOfWork unitOfWork;

    public MealOrderController(IMapper mapper, IMealOrderRepository mealOrderRepository, IUnitOfWork unitOfWork)
    {
      this.mapper = mapper;
      this.mealOrderRepository = mealOrderRepository;
      this.unitOfWork = unitOfWork;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
      // TODO : send userId to getAll
      var mealOrders = await mealOrderRepository.GetAll();

      var result = mapper.Map<IEnumerable<ViewMealOrderResource>>(mealOrders);

      return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetOne(int id)
    {
      // TODO : send userId to getOne
      var mealOrder = await mealOrderRepository.GetOne(id);

      if (mealOrder == null)
        return NotFound();

      var result = mapper.Map<MealOrder, ViewMealOrderResource>(mealOrder);

      return Ok(result);
    }

    [HttpGet("paged")]
    public async Task<IActionResult> GetPagedMealOrderEntryHeader([FromQuery]MealOrderParams mealOrderParams)
    {
            // TODO : send userId to getPaged
      var mealOrders = await mealOrderRepository.GetPagedMealOrder(mealOrderParams);

      var result = mapper.Map<IEnumerable<ViewMealOrderResource>>(mealOrders);

      Response.AddPagination(mealOrders.CurrentPage, mealOrders.PageSize, mealOrders.TotalCount, mealOrders.TotalPages);

      return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody]SaveMealOrderResource mealOrderResource)
    {
      // TODO : cegah save jika sudah lewat waktu  lockedMeal
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

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody]SaveMealOrderResource mealOrderResource)
    {
      // TODO : cegah save jika sudah lewat waktu  lockedMeal

      if (!ModelState.IsValid)
        return BadRequest(ModelState);

      var mealOrder = await mealOrderRepository.GetOne(id);

      if (mealOrder == null)
        return NotFound();

      mealOrder = mapper.Map(mealOrderResource, mealOrder);

      if (await unitOfWork.CompleteAsync() == false)
      {
        throw new Exception(message: $"Updating order with id: {id} failed on save");
      }

      mealOrder = await mealOrderRepository.GetOne(mealOrder.Id);

      var result = mapper.Map<MealOrder, ViewMealOrderResource>(mealOrder);

      return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Remove(int id)
    {
      var mealOrder = await mealOrderRepository.GetOne(id);

      if (mealOrder == null)
        return NotFound();

      mealOrderRepository.Remove(mealOrder);

      if (await unitOfWork.CompleteAsync() == false)
      {
        throw new Exception(message: $"Deleting order with id: {id} failed");
      }

      return Ok($"{id}");
    }
  }
}