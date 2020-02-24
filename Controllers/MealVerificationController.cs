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
  public class MealVerificationController : Controller
  {
    private readonly IMapper mapper;
    private readonly IMealVerificationRepository mealVerificationRepository;
    private readonly IUnitOfWork unitOfWork;

    public MealVerificationController(IMapper mapper, IMealVerificationRepository mealVerificationRepository, IUnitOfWork unitOfWork)
    {
      this.mapper = mapper;
      this.mealVerificationRepository = mealVerificationRepository;
      this.unitOfWork = unitOfWork;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
      var mealVerifications = await mealVerificationRepository.GetAll();

      var result = mapper.Map<IEnumerable<ViewMealVerificationResource>>(mealVerifications);

      return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetOne(int id)
    {
      var mealVerification = await mealVerificationRepository.GetOne(id);

      if (mealVerification == null)
        return NotFound();

      var result = mapper.Map<MealOrderVerificationHeader, ViewMealVerificationResource>(mealVerification);

      return Ok(result);
    }

    [HttpGet("paged")]
    public async Task<IActionResult> GetPagedVerification([FromQuery]MealVerificationParams mealVerificationParams)
    {
      var mealVerifications = await mealVerificationRepository.GetPagedMealVerification(mealVerificationParams);

      var result = mapper.Map<IEnumerable<ViewMealVerificationResource>>(mealVerifications);

      Response.AddPagination(mealVerifications.CurrentPage, mealVerifications.PageSize, mealVerifications.TotalCount, mealVerifications.TotalPages);

      return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody]SaveMealVerificationResource mealVerificationResource)
    {
      if (!ModelState.IsValid)
        return BadRequest(ModelState);

      var mealVerification = mapper.Map<SaveMealVerificationResource, MealOrderVerificationHeader>(mealVerificationResource);


      mealVerificationRepository.Add(mealVerification);

      if (await unitOfWork.CompleteAsync() == false)
      {
        throw new Exception(message: "Create new order failed on save");
      }

      mealVerification = await mealVerificationRepository.GetOne(mealVerification.Id);

      var result = mapper.Map<MealOrderVerificationHeader, ViewMealVerificationResource>(mealVerification);

      return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody]SaveMealVerificationResource mealVerificationResource)
    {
      if (!ModelState.IsValid)
        return BadRequest(ModelState);

      var mealVerification = await mealVerificationRepository.GetOne(id);

      if (mealVerification == null)
        return NotFound();

      mealVerification = mapper.Map(mealVerificationResource, mealVerification);

      if (await unitOfWork.CompleteAsync() == false)
      {
        throw new Exception(message: $"Updating order with id: {id} failed on save");
      }

      mealVerification = await mealVerificationRepository.GetOne(mealVerification.Id);

      var result = mapper.Map<MealOrderVerificationHeader, ViewMealVerificationResource>(mealVerification);

      return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Remove(int id)
    {
      var mealVerification = await mealVerificationRepository.GetOne(id);

      if (mealVerification == null)
        return NotFound();

      mealVerificationRepository.Remove(mealVerification);

      if (await unitOfWork.CompleteAsync() == false)
      {
        throw new Exception(message: $"Deleting order with id: {id} failed");
      }

      return Ok($"{id}");
    }
  }
}