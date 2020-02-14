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
  [ApiController]
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

      var configuration = await configurationRepository.GetOne(id);

      if (configuration == null)
        return NotFound();

      configuration = mapper.Map(configurationResource, configuration);

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
        throw new Exception(message: $"Delete configuration with id: {id} failed on save");
      }

      return Ok($"{id}");
    }

  }

}