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
using Microsoft.AspNetCore.Authorization;

namespace BusMeal.API.Controllers
{
    [Route("api/[controller]")]
    public class MealOrderVerificationController : Controller
    {
        private readonly IMapper mapper;
        private readonly IMealOrderVerificationRepository mealOrderVerificationRepository;
        private readonly IUnitOfWork unitOfWork;
        private IMealtypeRepository mealtypeRepository;
        private readonly IMealOrderRepository mealOrderRepository;
        private readonly IUserRepository userRepository;

        public MealOrderVerificationController(
          IMapper mapper,
          IMealOrderVerificationRepository mealOrderVerificationRepository,
          IMealtypeRepository mealtypeRepository,
          IUnitOfWork unitOfWork,
          IMealOrderRepository mealOrderRepository,
          IUserRepository userRepository
          )
        {
            this.mapper = mapper;
            this.mealOrderVerificationRepository = mealOrderVerificationRepository;
            this.unitOfWork = unitOfWork;
            this.mealtypeRepository = mealtypeRepository;
            this.mealOrderRepository = mealOrderRepository;
            this.userRepository = userRepository;
        }

        [Authorize(Roles = "Administrator")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            // var userId = getUserId();
            // var user = await userRepository.GetOne(userId);
            // if (user.AdminStatus != true)
            // {
            //   return BadRequest("This data only available for admin");
            // }

            var mealOrderVerifications = await mealOrderVerificationRepository.GetAll();

            var result = mapper.Map<IEnumerable<ViewMealOrderVerificationResource>>(mealOrderVerifications);

            return Ok(result);
        }

        // [Authorize(Roles = "Administrator")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne(int id)
        {
            // var userId = getUserId();
            // var user = await userRepository.GetOne(userId);
            // if (user.AdminStatus != true)
            // {
            //   return BadRequest("This data only available for admin");
            // }

            var mealOrderVerification = await mealOrderVerificationRepository.GetOne(id);

            if (mealOrderVerification == null)
                return NotFound();

            var result = mapper.Map<MealOrderVerification, ViewMealOrderVerificationResource>(mealOrderVerification);

            return Ok(result);
        }

        // [Authorize(Roles = "Administrator")]
        [HttpGet("paged")]
        public async Task<IActionResult> GetPagedMealOrderVerification([FromQuery]MealOrderVerificationParams mealOrderVerificationParams)
        {
            // var userId = getUserId();
            // var user = await userRepository.GetOne(userId);
            // if (user.AdminStatus != true)
            // {
            //   return BadRequest("This data only available for admin");
            // }

            var mealOrderVerifications = await mealOrderVerificationRepository.GetPagedMealOrderVerification(mealOrderVerificationParams);

            var result = mapper.Map<IEnumerable<ViewMealOrderVerificationResource>>(mealOrderVerifications);

            Response.AddPagination(mealOrderVerifications.CurrentPage, mealOrderVerifications.PageSize, mealOrderVerifications.TotalCount, mealOrderVerifications.TotalPages);

            return Ok(result);
        }

        // [Authorize(Roles = "Administrator")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody]SaveMealOrderVerificationResource mealOrderVerificationResource)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var OrderLists = mealOrderVerificationResource.OrderList;
            // if (mealOrderVerificationResource.IsClosed == true)
            // {
            //   var orderVerificationDetail = mealOrderVerificationResource.MealOrderVerificationDetails;
            //   foreach (SaveMealOrderVerificationDetailResource item in orderVerificationDetail)
            //   {
            //     var mealTypeId = item.MealTypeId;
            //     var mealType = await mealtypeRepository.GetOne(mealTypeId);
            //     var vendorId = mealType.MealVendorId;
            //     item.VendorId = vendorId;
            //   }
            // }

            var mealOrderVerification = mapper.Map<SaveMealOrderVerificationResource, MealOrderVerification>(mealOrderVerificationResource);

            var mealVerificationDetails = mealOrderVerification.MealOrderVerificationDetails;

            foreach (MealOrderVerificationDetail mealVerificationDetail in mealVerificationDetails)
            {
                if (mealVerificationDetail.VendorId <= 0)
                {
                    return BadRequest("Vendor id is required");
                }
                // var mealTypeRecord = await mealtypeRepository.GetOne(mealVerificationDetail.MealTypeId);
                // mealVerificationDetail.VendorId = mealTypeRecord.MealVendorId;
            }

            mealOrderVerificationRepository.Add(mealOrderVerification);

            foreach (int item in OrderLists)
            {
                var Order = await mealOrderRepository.GetOne(item);
                Order.MealOrderVerificationId = mealOrderVerification.Id;
            }

            if (await unitOfWork.CompleteAsync() == false)
            {
                throw new Exception(message: "Create new order failed on save");
            }

            mealOrderVerification = await mealOrderVerificationRepository.GetOne(mealOrderVerification.Id);

            var result = mapper.Map<MealOrderVerification, ViewMealOrderVerificationResource>(mealOrderVerification);

            return Ok(result);
        }

        // [Authorize(Roles = "Administrator")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody]SaveMealOrderVerificationResource mealOrderVerificationResource)
        {
            // var userId = getUserId();
            // var user = await userRepository.GetOne(userId);
            // if (user.AdminStatus != true)
            // {
            //   return BadRequest("This data only available for admin");
            // }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var mealOrderVerification = await mealOrderVerificationRepository.GetOne(id);

            if (mealOrderVerification == null)
                return NotFound();

            if (mealOrderVerification.IsClosed == true)
            {
                return BadRequest("Can't edit the record since it was closed");
            }

            var OrderLists = mealOrderVerificationResource.OrderList;

            mealOrderVerification = mapper.Map(mealOrderVerificationResource, mealOrderVerification);

            foreach (int item in OrderLists)
            {
                var Order = await mealOrderRepository.GetOne(item);
                Order.MealOrderVerification = mealOrderVerification;
            }

            if (await unitOfWork.CompleteAsync() == false)
            {
                throw new Exception(message: $"Updating order with id: {id} failed on save");
            }

            mealOrderVerification = await mealOrderVerificationRepository.GetOne(mealOrderVerification.Id);

            var result = mapper.Map<MealOrderVerification, ViewMealOrderVerificationResource>(mealOrderVerification);

            return Ok(result);
        }

        // [Authorize(Roles = "Administrator")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            // var userId = getUserId();
            // var user = await userRepository.GetOne(userId);
            // if (user.AdminStatus != true)
            // {
            //   return BadRequest("This data only available for admin");
            // }

            var mealOrderVerification = await mealOrderVerificationRepository.GetOne(id);

            if (mealOrderVerification == null)
                return NotFound();

            if (mealOrderVerification.IsClosed == true)
            {
                return BadRequest("Can't delete the record since it was closed");
            }

            mealOrderVerificationRepository.Remove(mealOrderVerification);

            if (await unitOfWork.CompleteAsync() == false)
            {
                throw new Exception(message: $"Deleting order verification failed");
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