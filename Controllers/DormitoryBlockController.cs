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
    public class DormitoryBlockController : Controller
    {
        private readonly IMapper mapper;
        private readonly IDormitoryBlockRepository dormitoryBlockRepository;
        private readonly IUnitOfWork unitOfWork;
        public DormitoryBlockController(IMapper mapper, IDormitoryBlockRepository dormitoryBlockRepository, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.dormitoryBlockRepository = dormitoryBlockRepository;
            this.unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var dormitoryBlock = await dormitoryBlockRepository.GetAll();

            var result = mapper.Map<IEnumerable<ViewDormitoryBlockResource>>(dormitoryBlock);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne(int id)
        {
            var dormitoryBlock = await dormitoryBlockRepository.GetOne(id);

            if (dormitoryBlock == null)
                return NotFound();

            var result = mapper.Map<DormitoryBlock, ViewDormitoryBlockResource>(dormitoryBlock);

            return Ok(result);
        }

        [HttpGet("paged")]
        public async Task<IActionResult> GetPagedDormitoryBlock([FromQuery]DormitoryBlockParams dormitoryBlockParams)
        {
            var dormitoryBlock = await dormitoryBlockRepository.GetPagedDormitoryBlock(dormitoryBlockParams);

            var result = mapper.Map<IEnumerable<ViewDormitoryBlockResource>>(dormitoryBlock);

            Response.AddPagination(dormitoryBlock.CurrentPage, dormitoryBlock.PageSize, dormitoryBlock.TotalCount, dormitoryBlock.TotalPages);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]SaveDormitoryBlockResource dormitoryBlockResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var dormitoryBlock = mapper.Map<SaveDormitoryBlockResource, DormitoryBlock>(dormitoryBlockResource);

            dormitoryBlockRepository.Add(dormitoryBlock);
            if (await unitOfWork.CompleteAsync() == false)
            {
                throw new Exception(message: $"Create new dormitoryBlock fail on save");
            }

            dormitoryBlock = await dormitoryBlockRepository.GetOne(dormitoryBlock.Id);
            var result = mapper.Map<DormitoryBlock, ViewDormitoryBlockResource>(dormitoryBlock);
            return Ok(result);

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody]SaveDormitoryBlockResource dormitoryBlockResource)
        {
            if (!ModelState.IsValid)
            return BadRequest(ModelState);

            var dormitoryBlock = await dormitoryBlockRepository.GetOne(id);

            if (dormitoryBlock == null)
            return NotFound();

            dormitoryBlock = mapper.Map(dormitoryBlockResource, dormitoryBlock);

            if (await unitOfWork.CompleteAsync() == false)
            {
                throw new Exception(message: $"Updating dormitoryBlock {id} failed on save");
            }

            dormitoryBlock = await dormitoryBlockRepository.GetOne(dormitoryBlock.Id);

            var result = mapper.Map<DormitoryBlock, ViewDormitoryBlockResource>(dormitoryBlock);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemovedormitoryBlock(int id)
        {
            var dormitoryBlock = await dormitoryBlockRepository.GetOne(id);

            if (dormitoryBlock == null)
                return NotFound();

            dormitoryBlockRepository.Remove(dormitoryBlock);

            if (await unitOfWork.CompleteAsync() == false)
            {
                throw new Exception(message: $"Deleting dormitoryBlock {id} failed");
            }

            return Ok($"{id}");
        }



    }
}