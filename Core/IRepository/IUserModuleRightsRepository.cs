using System.Collections.Generic;
using System.Threading.Tasks;
using BusMeal.API.Core.Models;
using BusMeal.API.Helpers;
using BusMeal.API.Helpers.Params;

namespace BusMeal.API.Core.IRepository
{
  public interface IUserModuleRightsRepository
  {
    Task<UserModuleRight> GetOne(int id);
    void Add(UserModuleRight userModuleRights);
    void Remove(UserModuleRight userModuleRights);
    Task<IEnumerable<UserModuleRight>> GetAll();
    Task<PagedList<UserModuleRight>> GetPagedUserModuleRights(UserModuleRightsParams userModuleRightsParams);

  }
}