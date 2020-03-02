using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusMeal.API.Core.IRepository;
using BusMeal.API.Core.Models;
using BusMeal.API.Helpers;
using BusMeal.API.Helpers.Params;
using Microsoft.EntityFrameworkCore;

namespace BusMeal.API.Persistence.Repository
{
  public class ConfigurationRepository : IConfigurationRepository
  {
    private readonly DataContext context;

    public ConfigurationRepository(DataContext context)
    {
      this.context = context;
    }

    public async Task<AppConfiguration> GetOne(int? id=null)
    {
      if (id != null)
      {
        return await context.AppConfiguration.FindAsync(id);
      } else {
        return await context.AppConfiguration.FindAsync();
      }
    }

    public void Add(AppConfiguration configuration)
    {
      context.AppConfiguration.Add(configuration);
    }

    public void Remove(AppConfiguration configuration)
    {
      context.Remove(configuration);
    }

    public async Task<IEnumerable<AppConfiguration>> GetAll()
    {
      var configurations = await context.AppConfiguration.ToListAsync();

      return configurations;
    }

    public async Task<PagedList<AppConfiguration>> GetPagedConfiguration(ConfigurationParams configurationParams)
    {
      var configurations = context.AppConfiguration.AsQueryable();

      // Filtering
      if (configurationParams.RowGrid > 0)
      {
        configurations = configurations.Where(c => c.RowGrid == configurationParams.RowGrid);
      }

      // if (configurationParams.LockedBusOrder != DateTime.Now.Date)
      // {
      //   configurations = configurations.Where(c => c.LockedBusOrder == configurationParams.LockedBusOrder);
      // }

      // if (configurationParams.LockedMealOrder != DateTime.Now.Date)
      // {
      //   configurations = configurations.Where(c => c.LockedMealOrder == configurationParams.LockedMealOrder);
      // }

      // Sorting
      if (configurationParams.isDescending)
      {
        if (!string.IsNullOrEmpty(configurationParams.OrderBy))
        {
          switch (configurationParams.OrderBy.ToLower())
          {
            case "rowgrid":
              configurations = configurations.OrderByDescending(c => c.RowGrid);
              break;
            case "lockedbusorder":
              configurations = configurations.OrderByDescending(c => c.LockedBusOrder);
              break;
            case "lockmealorder":
              configurations = configurations.OrderByDescending(c => c.LockedMealOrder);
              break;
            default:
              configurations = configurations.OrderByDescending(c => c.RowGrid);
              break;
          }
        }
        else
        {
          configurations = configurations.OrderByDescending(c => c.RowGrid);
        }
      }
      else
      {
        if (!string.IsNullOrEmpty(configurationParams.OrderBy))
        {
          switch (configurationParams.OrderBy.ToLower())
          {
            case "rowgrid":
              configurations = configurations.OrderBy(c => c.RowGrid);
              break;
            case "lockedbusorder":
              configurations = configurations.OrderBy(c => c.LockedBusOrder);
              break;
            case "lockmealorder":
              configurations = configurations.OrderBy(c => c.LockedMealOrder);
              break;
            default:
              configurations = configurations.OrderBy(c => c.RowGrid);
              break;
          }
        }
        else
        {
          configurations = configurations.OrderBy(c => c.RowGrid);
        }
      }

      return await PagedList<AppConfiguration>.CreateAsync(configurations, configurationParams.PageNumber, configurationParams.PageSize);
    }
  }
}