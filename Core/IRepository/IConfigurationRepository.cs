using System.Collections.Generic;
using System.Threading.Tasks;
using BusMeal.API.Core.Models;
using BusMeal.API.Helpers;
using BusMeal.API.Helpers.Params;

namespace BusMeal.API.Core.IRepository
{
  public interface IConfigurationRepository
  {
    Task<AppConfiguration> GetOne(int? id = null);
    void Add(AppConfiguration configuration);
    void Remove(AppConfiguration configuration);
    void Update(AppConfiguration configuration);    
    Task<IEnumerable<AppConfiguration>> GetAll();
    Task<PagedList<AppConfiguration>> GetPagedConfiguration(ConfigurationParams configurationParams);
  }
}