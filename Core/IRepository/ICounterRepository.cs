using System.Collections.Generic;
using System.Threading.Tasks;
using BusMeal.API.Core.Models;
using BusMeal.API.Helpers;
using BusMeal.API.Helpers.Params;

namespace BusMeal.API.Core.IRepository
{
    public interface ICounterRepository
    {
        Task<Counter> GetOne(int id);
        void Add(Counter counter);
        void Remove(Counter counter);
        void Update(Counter counter);
        Task<IEnumerable<Counter>> GetAll();
        Task<PagedList<Counter>> GetPagedCounter(CounterParams counterParams);
    }
}