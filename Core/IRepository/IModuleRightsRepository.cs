using System.Collections.Generic;
using System.Threading.Tasks;
using BusMeal.API.Core.Models;
using BusMeal.API.Helpers;
using BusMeal.API.Helpers.Params;

namespace BusMeal.API.Core.IRepository
{
  public interface IModuleRightsRepository
  {
    Task<ModuleRight> GetOne(int id);
    void Add(ModuleRight moduleRights);
    void Remove(ModuleRight moduleRights);
    Task<IEnumerable<ModuleRight>> GetAll();
    Task<PagedList<ModuleRight>> GetPagedModuleRights(ModuleRightsParams moduleRightsParams);

  }
}