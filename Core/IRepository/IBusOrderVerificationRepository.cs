using System.Collections.Generic;
using System.Threading.Tasks;
using BusMeal.API.Core.Models;
using BusMeal.API.Helpers;
using BusMeal.API.Helpers.Params;

namespace BusMeal.API.Core.IRepository
{
  public interface IBusOrderVerificationRepository
  {
    Task<BusOrderVerification> GetOne(int id);
    void Add(BusOrderVerification busOrderVerification);
    void Remove(BusOrderVerification busOrderVerification);

    Task<IEnumerable<BusOrderVerification>> GetAll();
    Task<PagedList<BusOrderVerification>> GetPagedBusOrderVerification(BusOrderVerificationParams busOrderVerificationParams);
  }
}