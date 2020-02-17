using System.Collections.Generic;
using System.Threading.Tasks;
using BusMeal.API.Core.Models;
using BusMeal.API.Helpers;
using BusMeal.API.Helpers.Params;

namespace BusMeal.API.Core.IRepository
{
    public interface IBusTimeRepository
    {
        Task<BusTime> GetOne(int id);
        void Add(BusTime BusTime);
        void Remove(BusTime BusTime);
        //  void Update(BusTime BusTime)
        Task<IEnumerable<BusTime>> GetAll();
        Task<PagedList<BusTime>> GetPagedBusTimes(BusTimeParams busTimeParams);
    }
}