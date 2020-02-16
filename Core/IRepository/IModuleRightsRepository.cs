using System.Collections.Generic;
using System.Threading.Tasks;
using BusMeal.API.Core.Models;
using BusMeal.API.Helpers;
using BusMeal.API.Helpers.Params;

namespace BusMeal.API.Core.IRepository
{
  public interface IModuleRightsRepository
  {
    Task<ModuleRights> GetOne(int id);
    void Add(ModuleRights moduleRights);
    void Remove(ModuleRights moduleRights);
    Task<IEnumerable<ModuleRights>> GetAll();
    Task<PagedList<ModuleRights>> GetPagedModuleRights(ModuleRightsParams moduleRightsParams);

  }
}