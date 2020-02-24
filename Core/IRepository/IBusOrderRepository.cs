using System.Collections.Generic;
using System.Threading.Tasks;
using BusMeal.API.Core.Models;
using BusMeal.API.Helpers;
using BusMeal.API.Helpers.Params;

namespace BusMeal.API.Core.IRepository
{
  public interface IBusOrderRepository
  {
    Task<BusOrderEntryHeader> GetOne(int id);
    void Add(BusOrderEntryHeader busOrderEntryHeader);
    void Remove(BusOrderEntryHeader busOrderEntryHeader);

    Task<IEnumerable<BusOrderEntryHeader>> GetAll();
    Task<PagedList<BusOrderEntryHeader>> GetPagedBusOrder(BusOrderParams busOrderParams);
  }
}
