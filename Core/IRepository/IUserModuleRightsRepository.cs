using System.Collections.Generic;
using System.Threading.Tasks;
using BusMeal.API.Core.Models;
using BusMeal.API.Helpers;
using BusMeal.API.Helpers.Params;

namespace BusMeal.API.Core.IRepository
{
  public interface IUserModuleRightsRepository
  {
    Task<UserModuleRights> GetOne(int id);
    void Add(UserModuleRights userModuleRights);
    void Remove(UserModuleRights userModuleRights);
    Task<IEnumerable<UserModuleRights>> GetAll();
    Task<PagedList<UserModuleRights>> GetPagedUserModuleRights(UserModuleRightsParams userModuleRightsParams);

  }
}