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
  public class UserModuleRightsRepository : IUserModuleRightsRepository
  {
    private readonly DataContext context;

    public UserModuleRightsRepository(DataContext context)
    {
      this.context = context;
    }

    public async Task<UserModuleRights> GetOne(int id)
    {
      return await context.UserModuleRights.FindAsync(id);
    }

    public void Add(UserModuleRights userModuleRights)
    {
      context.UserModuleRights.Add(userModuleRights);
    }

    public void Remove(UserModuleRights userModuleRights)
    {
      context.Remove(userModuleRights);
    }

    public async Task<IEnumerable<UserModuleRights>> GetAll()
    {
      var usermodulergihts = await context.UserModuleRights.ToListAsync();
      return usermodulergihts;
    }

    public async Task<PagedList<UserModuleRights>> GetPagedUserModuleRights(UserModuleRightsParams userModuleRightsParams)
    {
      var usermodulerights = context.UserModuleRights.AsQueryable();

      // filter
      if (userModuleRightsParams.UserId > 0)
      {
        usermodulerights = usermodulerights.Where(u => u.UserId == userModuleRightsParams.UserId);
      }

      if (userModuleRightsParams.ModuleRightsId > 0)
      {
        usermodulerights = usermodulerights.Where(u => u.ModuleRightsId == userModuleRightsParams.ModuleRightsId);
      }

      // sort
      if (userModuleRightsParams.isDescending)
      {
        if (!string.IsNullOrEmpty(userModuleRightsParams.OrderBy))
        {
          switch (userModuleRightsParams.OrderBy.ToLower())
          {
            case "userid":
              usermodulerights = usermodulerights.OrderByDescending(u => u.UserId);
              break;
            case "modulerightsid":
              usermodulerights = usermodulerights.OrderByDescending(u => u.ModuleRightsId);
              break;
            default:
              usermodulerights = usermodulerights.OrderByDescending(u => u.UserId);
              break;
          }
        }
        else
        {
          usermodulerights = usermodulerights.OrderByDescending(u => u.UserId);
        }
      }
      else
      {
        if (!string.IsNullOrEmpty(userModuleRightsParams.OrderBy))
        {
          switch (userModuleRightsParams.OrderBy.ToLower())
          {
            case "userid":
              usermodulerights = usermodulerights.OrderBy(u => u.UserId);
              break;
            case "modulerightsid":
              usermodulerights = usermodulerights.OrderBy(u => u.ModuleRightsId);
              break;
            default:
              usermodulerights = usermodulerights.OrderBy(u => u.UserId);
              break;
          }
        }
        else
        {
          usermodulerights = usermodulerights.OrderBy(u => u.UserId);
        }
      }

      return await PagedList<UserModuleRights>.CreateAsync(usermodulerights, userModuleRightsParams.PageNumber, userModuleRightsParams.PageSize);
    }
  }
}