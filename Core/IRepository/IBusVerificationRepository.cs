using System.Collections.Generic;
using System.Threading.Tasks;
using BusMeal.API.Core.Models;
using BusMeal.API.Helpers;
using BusMeal.API.Helpers.Params;

namespace BusMeal.API.Core.IRepository
{
  public interface IBusVerificationRepository
  {
    Task<BusOrderVerificationHeader> GetOne(int id);
    void Add(BusOrderVerificationHeader busOrderVerificationHeader);
    void Remove(BusOrderVerificationHeader busOrderVerificationHeaderTotal);

    Task<IEnumerable<BusOrderVerificationHeader>> GetAll();
    Task<PagedList<BusOrderVerificationHeader>> GetPagedBusOrderVerification(BusVerificationParams busVerificationParams);
  }
}