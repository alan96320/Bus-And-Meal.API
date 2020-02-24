using System;
using System.Threading.Tasks;
using AutoMapper;
using BusMeal.API.Controllers.Resources;
using BusMeal.API.Core.IRepository;
using BusMeal.API.Core.Models;
using Microsoft.AspNetCore.Mvc;

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

    [HttpPost]
    public async Task<IActionResult> Create([FromBody]SaveMealOrderResource mealOrderResource)
    {
      if (!ModelState.IsValid)
        return BadRequest(ModelState);

      var mealOrder = mapper.Map<SaveMealOrderResource, MealOrderEntryHeader>(mealOrderResource);


      mealOrderRepository.Add(mealOrder);

      if (await unitOfWork.CompleteAsync() == false)
      {
        throw new Exception(message: "Create new order failed on save");
      }

      mealOrder = await mealOrderRepository.GetOne(mealOrder.Id);

      return Ok(mealOrder);

    }
  }
}