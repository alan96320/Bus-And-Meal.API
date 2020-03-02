using System.Collections.Generic;
using System.Threading.Tasks;
using BusMeal.API.Core.Models;
using BusMeal.API.Helpers;
using BusMeal.API.Helpers.Params;

namespace BusMeal.API.Core.IRepository
{
  public interface IBusOrderRepository
  {
    Task<BusOrder> GetOne(int id, int? userId = null);
    void Add(BusOrder busOrder);
    void Remove(BusOrder busOrder);

    Task<IEnumerable<BusOrder>> GetAll(int? userId= null);
    Task<PagedList<BusOrder>> GetPagedBusOrder(BusOrderParams busOrderParams, int? userId = null);
  }
}
