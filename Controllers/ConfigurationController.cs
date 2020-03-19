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

        // [Authorize(Roles = "Configuration.R, Administrator")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne(int id)
        {
            var configuration = await configurationRepository.GetOne(id);

            if (configuration == null)
                return NotFound();

            var result = mapper.Map<AppConfiguration, ViewConfigurationResource>(configuration);

            return Ok(result);
        }

        // [Authorize(Roles = "Configuration.R, Administrator")]
        [HttpGet("paged")]
        public async Task<IActionResult> GetPagedConfiguration([FromQuery]ConfigurationParams configurationParams)
        {
            var configurations = await configurationRepository.GetPagedConfiguration(configurationParams);

            var result = mapper.Map<IEnumerable<ViewConfigurationResource>>(configurations);

            Response.AddPagination(configurations.CurrentPage, configurations.PageSize, configurations.TotalCount, configurations.TotalPages);

            return Ok(result);
        }

        // [Authorize(Roles = "Configuration.W, Administrator")]
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

        // [Authorize(Roles = "Configuration.W, Administrator")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody]SaveConfigurationResource configurationResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

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

        // [Authorize(Roles = "Configuration.W, Administrator")]
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