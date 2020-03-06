using System.Linq;
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
  public class MealOrderVerificationController : Controller
  {
    private readonly IMapper mapper;
    private readonly IMealOrderVerificationRepository mealOrderVerificationRepository;
    private readonly IUnitOfWork unitOfWork;
    private IMealtypeRepository mealtypeRepository;

    public MealOrderVerificationController(IMapper mapper, IMealOrderVerificationRepository mealOrderVerificationRepository, IMealtypeRepository mealtypeRepository, IUnitOfWork unitOfWork)
    {
      this.mapper = mapper;
      this.mealOrderVerificationRepository = mealOrderVerificationRepository;
      this.unitOfWork = unitOfWork;
      this.mealtypeRepository = mealtypeRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
      var mealOrderVerifications = await mealOrderVerificationRepository.GetAll();

      var result = mapper.Map<IEnumerable<ViewMealOrderVerificationResource>>(mealOrderVerifications);

      return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetOne(int id)
    {
      var mealOrderVerification = await mealOrderVerificationRepository.GetOne(id);

      if (mealOrderVerification == null)
        return NotFound();

      var result = mapper.Map<MealOrderVerification, ViewMealOrderVerificationResource>(mealOrderVerification);

      return Ok(result);
    }

    [HttpGet("paged")]
    public async Task<IActionResult> GetPagedMealOrderVerification([FromQuery]MealOrderVerificationParams mealOrderVerificationParams)
    {
      var mealOrderVerifications = await mealOrderVerificationRepository.GetPagedMealOrderVerification(mealOrderVerificationParams);

      var result = mapper.Map<IEnumerable<ViewMealOrderVerificationResource>>(mealOrderVerifications);

      Response.AddPagination(mealOrderVerifications.CurrentPage, mealOrderVerifications.PageSize, mealOrderVerifications.TotalCount, mealOrderVerifications.TotalPages);

      return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody]SaveMealOrderVerificationResource mealOrderVerificationResource)
    {
      if (!ModelState.IsValid)
        return BadRequest(ModelState);

      var mealOrderVerification = mapper.Map<SaveMealOrderVerificationResource, MealOrderVerification>(mealOrderVerificationResource);

      var mealVerificationDetails = mealOrderVerification.MealOrderVerificationDetails;

      foreach (MealOrderVerificationDetail mealVerificationDetail in mealVerificationDetails)
      {
        var mealTypeRecord = await mealtypeRepository.GetOne(mealVerificationDetail.MealTypeId);
        mealVerificationDetail.VendorId = mealTypeRecord.MealVendorId;
      }

      mealOrderVerificationRepository.Add(mealOrderVerification);

      if (await unitOfWork.CompleteAsync() == false)
      {
        throw new Exception(message: "Create new order failed on save");
      }

      mealOrderVerification = await mealOrderVerificationRepository.GetOne(mealOrderVerification.Id);

      var result = mapper.Map<MealOrderVerification, ViewMealOrderVerificationResource>(mealOrderVerification);

      return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody]SaveMealOrderVerificationResource mealOrderVerificationResource)
    {
      if (!ModelState.IsValid)
        return BadRequest(ModelState);

      var mealOrderVerification = await mealOrderVerificationRepository.GetOne(id);

      if (mealOrderVerification == null)
        return NotFound();

      mealOrderVerification = mapper.Map(mealOrderVerificationResource, mealOrderVerification);

      if (await unitOfWork.CompleteAsync() == false)
      {
        throw new Exception(message: $"Updating order with id: {id} failed on save");
      }

      mealOrderVerification = await mealOrderVerificationRepository.GetOne(mealOrderVerification.Id);

      var result = mapper.Map<MealOrderVerification, ViewMealOrderVerificationResource>(mealOrderVerification);

      return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Remove(int id)
    {
      var mealOrderVerification = await mealOrderVerificationRepository.GetOne(id);

      if (mealOrderVerification == null)
        return NotFound();

      mealOrderVerificationRepository.Remove(mealOrderVerification);

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