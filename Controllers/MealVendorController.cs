using System;
using System.Collections;
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
  public class MealVendorController : Controller
  {
    private readonly IMapper mapper;
    private readonly IMealVendorRepository mealVendorRepository;
    private readonly IUnitOfWork unitOfWork;

    public MealVendorController(IMapper mapper, IMealVendorRepository mealVendorRepository, IUnitOfWork unitOfWork)
    {
      this.mapper = mapper;
      this.mealVendorRepository = mealVendorRepository;
      this.unitOfWork = unitOfWork;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
      var mealvendors = await mealVendorRepository.GetAll();

      var result = mapper.Map<IEnumerable<ViewMealVendorResource>>(mealvendors);

      return Ok(result);
    }

    // [Authorize(Roles = "Meal Vendor.R, Administrator")]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetOne(int id)
    {
      var mealvendor = await mealVendorRepository.GetOne(id);

      if (mealvendor == null)
        return NotFound();

      var result = mapper.Map<MealVendor, ViewMealVendorResource>(mealvendor);

      return Ok(result);
    }

    // [Authorize(Roles = "Meal Vendor.R, Administrator")]
    [HttpGet("paged")]
    public async Task<IActionResult> GetPagedMealVendor([FromQuery]MealVendorParams mealVendorParams)
    {
      var mealvendors = await mealVendorRepository.GetPagedMealVendor(mealVendorParams);

      var result = mapper.Map<IEnumerable<ViewMealVendorResource>>(mealvendors);

      Response.AddPagination(mealvendors.CurrentPage, mealvendors.PageSize, mealvendors.TotalCount, mealvendors.TotalPages);

      return Ok(result);
    }

    // [Authorize(Roles = "Meal Vendor.W, Administrator")]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody]SaveMealVendorResource mealVendorResource)
    {
      if (!ModelState.IsValid)
        return BadRequest(ModelState);

      var mealvendor = mapper.Map<SaveMealVendorResource, MealVendor>(mealVendorResource);

      mealVendorRepository.Add(mealvendor);

      if (await unitOfWork.CompleteAsync() == false)
      {
        throw new Exception(message: "Create new meal vendor failed on save");
      }

      mealvendor = await mealVendorRepository.GetOne(mealvendor.Id);

      var result = mapper.Map<MealVendor, ViewMealVendorResource>(mealvendor);

      return Ok(result);
    }

    // [Authorize(Roles = "Meal Vendor.W, Administrator")]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody]SaveMealVendorResource mealVendorResource)
    {
      if (!ModelState.IsValid)
        return BadRequest(ModelState);

      var mealvendor = await mealVendorRepository.GetOne(id);

      if (mealvendor == null)
        return NotFound();

      mealvendor = mapper.Map(mealVendorResource, mealvendor);

      if (await unitOfWork.CompleteAsync() == false)
      {
        throw new Exception(message: $"Updating meal vendor with id: {id} failed on save");
      }

      mealvendor = await mealVendorRepository.GetOne(mealvendor.Id);

      var result = mapper.Map<MealVendor, ViewMealVendorResource>(mealvendor);

      return Ok(result);
    }

    // [Authorize(Roles = "Meal Vendor.W, Administrator")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> RemoveMealVendor(int id)
    {
      var mealvendor = await mealVendorRepository.GetOne(id);

      if (mealvendor == null)
        return NotFound();

      mealVendorRepository.Remove(mealvendor);

      if (await unitOfWork.CompleteAsync() == false)
      {
        throw new Exception(message: $"Deleting meal vendor with id: {id} failed on save");
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