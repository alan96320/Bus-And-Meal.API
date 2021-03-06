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
    [Authorize(Roles = "Administrator")]
    public class ConfigurationController : Controller
    {
        private readonly IMapper mapper;
        private readonly IConfigurationRepository configurationRepository;
        private readonly IUnitOfWork unitOfWork;

        public ConfigurationController(IMapper mapper, IConfigurationRepository configurationRepository, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.configurationRepository = configurationRepository;
            this.unitOfWork = unitOfWork;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var configurations = await configurationRepository.GetAll();

            var result = mapper.Map<IEnumerable<ViewConfigurationResource>>(configurations);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne(int id)
        {
            var configuration = await configurationRepository.GetOne(id);

            if (configuration == null)
                return NotFound();

            var result = mapper.Map<AppConfiguration, ViewConfigurationResource>(configuration);

            return Ok(result);
        }

        [HttpGet("paged")]
        public async Task<IActionResult> GetPagedConfiguration([FromQuery]ConfigurationParams configurationParams)
        {
            var configurations = await configurationRepository.GetPagedConfiguration(configurationParams);

            var result = mapper.Map<IEnumerable<ViewConfigurationResource>>(configurations);

            Response.AddPagination(configurations.CurrentPage, configurations.PageSize, configurations.TotalCount, configurations.TotalPages);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]SaveConfigurationResource configurationResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var configuration = mapper.Map<SaveConfigurationResource, AppConfiguration>(configurationResource);

            configurationRepository.Add(configuration);

            if (await unitOfWork.CompleteAsync() == false)
            {
                throw new Exception(message: "Create new configuration fail on save");
            }

            configuration = await configurationRepository.GetOne(configuration.Id);

            var result = mapper.Map<AppConfiguration, ViewConfigurationResource>(configuration);

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody]SaveConfigurationResource configurationResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var StartTime1 = configurationResource.LockedBusOrderStart1;
            var EndTime1 = configurationResource.LockedBusOrderEnd1;
            var StartTime2 = configurationResource.LockedBusOrderStart2;
            var EndTime2 = configurationResource.LockedBusOrderEnd2;

            if (
              TimeSpan.Parse(EndTime1) < TimeSpan.Parse(StartTime1) ||
              TimeSpan.Parse(EndTime2) < TimeSpan.Parse(StartTime2) ||
              TimeSpan.Parse(StartTime2) < TimeSpan.Parse(StartTime1)
              )
            {
                return BadRequest("Time Setting Invalid");
            }

            var configuration = await configurationRepository.GetOne(id);

            if (configuration == null)
                return NotFound();

            configuration = mapper.Map(configurationResource, configuration);

            configurationRepository.Update(configuration);

            if (await unitOfWork.CompleteAsync() == false)
            {
                throw new Exception(message: $"Updating configuration with id: {id} failed on save");
            }

            configuration = await configurationRepository.GetOne(configuration.Id);

            var result = mapper.Map<AppConfiguration, ViewConfigurationResource>(configuration);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveConfiguration(int id)
        {
            var configuration = await configurationRepository.GetOne(id);

            if (configuration == null)
                return NotFound();

            configurationRepository.Remove(configuration);

            if (await unitOfWork.CompleteAsync() == false)
            {
                throw new Exception(message: $"Delete configuration failed");
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