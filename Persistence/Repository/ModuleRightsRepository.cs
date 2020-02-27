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
  public class ModuleRightsRepository : IModuleRightsRepository
  {
    private readonly DataContext context;

    public ModuleRightsRepository(DataContext context)
    {
      this.context = context;
    }

    public async Task<ModuleRight> GetOne(int id)
    {
      return await context.ModuleRight.FindAsync(id);
    }

    public void Add(ModuleRight moduleRights)
    {
      context.ModuleRight.Add(moduleRights);
    }

    public void Remove(ModuleRight moduleRights)
    {
      context.Remove(moduleRights);
    }

    public async Task<IEnumerable<ModuleRight>> GetAll()
    {
      var modules = await context.ModuleRight.ToListAsync();

      return modules;
    }

    public async Task<PagedList<ModuleRight>> GetPagedModuleRights(ModuleRightsParams moduleRightsParams)
    {
      var modules = context.ModuleRight.AsQueryable();

      // filter
      if (!string.IsNullOrEmpty(moduleRightsParams.Code))
      {
        modules = modules.Where(m => m.Code.Contains(moduleRightsParams.Code, StringComparison.OrdinalIgnoreCase));
      }

      if (!string.IsNullOrEmpty(moduleRightsParams.Description))
      {
        modules = modules.Where(m => m.Description.Contains(moduleRightsParams.Description, StringComparison.OrdinalIgnoreCase));
      }

      //  sort
      if (moduleRightsParams.isDescending)
      {
        if (!string.IsNullOrEmpty(moduleRightsParams.OrderBy))
        {
          switch (moduleRightsParams.OrderBy.ToLower())
          {
            case "code":
              modules = modules.OrderByDescending(m => m.Code);
              break;
            case "description":
              modules = modules.OrderByDescending(m => m.Description);
              break;
            default:
              modules = modules.OrderByDescending(m => m.Code);
              break;
          }
        }
        else
        {
          modules = modules.OrderByDescending(m => m.Code);
        }
      }
      else
      {
        if (!string.IsNullOrEmpty(moduleRightsParams.OrderBy))
        {
          switch (moduleRightsParams.OrderBy.ToLower())
          {
            case "code":
              modules = modules.OrderBy(m => m.Code);
              break;
            case "description":
              modules = modules.OrderBy(m => m.Description);
              break;
            default:
              modules = modules.OrderBy(m => m.Code);
              break;
          }
        }
        else
        {
          modules = modules.OrderBy(m => m.Code);
        }
      }

      return await PagedList<ModuleRight>.CreateAsync(modules, moduleRightsParams.PageNumber, moduleRightsParams.PageSize);
    }
  }
}