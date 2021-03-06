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
  public class MealTypeController : Controller
  {
    private readonly IMapper mapper;
    private readonly IMealtypeRepository mealtypeRepository;
    private readonly IUnitOfWork unitOfWork;
    public MealTypeController(IMapper mapper, IMealtypeRepository mealtypeRepository, IUnitOfWork unitOfWork)
    {
      this.mapper = mapper;
      this.mealtypeRepository = mealtypeRepository;
      this.unitOfWork = unitOfWork;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
      var mealType = await mealtypeRepository.GetAll();

      var result = mapper.Map<IEnumerable<MealType>>(mealType);

      return Ok(result);
    }

    [Authorize(Roles = "Meal Type.R, Administrator")]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetOne(int id)
    {
      var mealType = await mealtypeRepository.GetOne(id);

      if (mealType == null)
        return NotFound();

      var result = mapper.Map<MealType, ViewMealTypeResource>(mealType);

      return Ok(result);
    }

    [Authorize(Roles = "Meal Type.R, Administrator")]
    [HttpGet("paged")]
    public async Task<IActionResult> GetPagedmealType([FromQuery]MealTypeParams mealTypeParams)
    {
      var mealTypes = await mealtypeRepository.GetPagedmealType(mealTypeParams);

      var result = mapper.Map<IEnumerable<ViewMealTypeResource>>(mealTypes);

      Response.AddPagination(mealTypes.CurrentPage, mealTypes.PageSize, mealTypes.TotalCount, mealTypes.TotalPages);

      return Ok(result);
    }

    [Authorize(Roles = "Meal Type.W, Administrator")]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody]SaveMealTypeResource mealTypeResource)
    {
      if (!ModelState.IsValid)
        return BadRequest(ModelState);

      var mealType = mapper.Map<SaveMealTypeResource, MealType>(mealTypeResource);

      mealtypeRepository.Add(mealType);
      if (await unitOfWork.CompleteAsync() == false)
      {
        throw new Exception(message: $"Create new mealType fail on save");
      }

      mealType = await mealtypeRepository.GetOne(mealType.Id);
      var result = mapper.Map<MealType, ViewMealTypeResource>(mealType);
      return Ok(result);

    }

    [Authorize(Roles = "Meal Type.W, Administrator")]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody]SaveMealTypeResource mealTypeResource)
    {
      if (!ModelState.IsValid)
        return BadRequest(ModelState);

      var mealType = await mealtypeRepository.GetOne(id);

      if (mealType == null)
        return NotFound();

      mealType = mapper.Map(mealTypeResource, mealType);

      mealtypeRepository.Update(mealType);

      if (await unitOfWork.CompleteAsync() == false)
      {
        throw new Exception(message: $"Updating mealType {id} failed on save");
      }

      mealType = await mealtypeRepository.GetOne(mealType.Id);

      var result = mapper.Map<MealType, ViewMealTypeResource>(mealType);

      return Ok(result);
    }

    [Authorize(Roles = "Meal Type.W, Administrator")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> RemovemealType(int id)
    {
      var mealType = await mealtypeRepository.GetOne(id);

      if (mealType == null)
        return NotFound();

      mealtypeRepository.Remove(mealType);

      if (await unitOfWork.CompleteAsync() == false)
      {
        throw new Exception(message: $"Deleting meal type failed");
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