using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusMeal.API.Core.IRepository;
using BusMeal.API.Core.Models;
using BusMeal.API.Helpers;
using BusMeal.API.Helpers.Params;
using Microsoft.EntityFrameworkCore;

namespace BusMeal.API.Persistence.Repository
{
  public class CounterRepository : ICounterRepository
  {
    private readonly DataContext context;

    public CounterRepository(DataContext context)
    {
      this.context = context;
    }

    public void Add(Counter counter)
    {
      context.Counter.Add(counter);
    }

    public async Task<IEnumerable<Counter>> GetAll()
    {
      var counter = await context.Counter.ToListAsync();

      return counter;
    }

    public async Task<Counter> GetOne(int id)
    {
      return await context.Counter.FindAsync(id);
    }

    public void Remove(Counter counter)
    {
      context.Remove(counter);
    }

    public async Task<PagedList<Counter>> GetPagedCounter(CounterParams counterParams)
    {
      var counter = context.Counter.AsQueryable();

      // ini untuk Filter
      if (!string.IsNullOrEmpty(counterParams.Code))
      {
        counter = counter.Where(c => c.Code.Contains(counterParams.Code, StringComparison.OrdinalIgnoreCase));
      }

      if (!string.IsNullOrEmpty(counterParams.Name))
      {
        counter = counter.Where(c => c.Name.Contains(counterParams.Name, StringComparison.OrdinalIgnoreCase));
      }

      if (!string.IsNullOrEmpty(counterParams.Location))
      {
        counter = counter.Where(c => c.Location.Contains(counterParams.Location, StringComparison.OrdinalIgnoreCase));
      }

      if (counterParams.Status > 0)
      {
        counter = counter.Where(c => c.Status == counterParams.Status);
      }

      // ini untuk Sort
      if (counterParams.isDescending)
      {
        if (!string.IsNullOrEmpty(counterParams.OrderBy))
        {
          switch (counterParams.OrderBy.ToLower())
          {
            case "code":
              counter = counter.OrderByDescending(c => c.Code);
              break;
            case "name":
              counter = counter.OrderByDescending(c => c.Name);
              break;
            case "location":
              counter = counter.OrderByDescending(c => c.Location);
              break;
            case "status":
              counter = counter.OrderByDescending(c => c.Status);
              break;
            default:
              counter = counter.OrderByDescending(c => c.Code);
              break;
          }
        }
        else
        {
          counter = counter.OrderByDescending(c => c.Code);
        }
      }
      else
      {
        if (!string.IsNullOrEmpty(counterParams.OrderBy))
        {
          switch (counterParams.OrderBy.ToLower())
          {
            case "code":
              counter = counter.OrderBy(c => c.Code);
              break;
            case "name":
              counter = counter.OrderBy(c => c.Name);
              break;
            case "location":
              counter = counter.OrderBy(c => c.Location);
              break;
            case "status":
              counter = counter.OrderBy(c => c.Status);
              break;
            default:
              counter = counter.OrderBy(c => c.Code);
              break;
          }
        }
        else
        {
          counter = counter.OrderBy(c => c.Code);
        }
      }

      return await PagedList<Counter>.CreateAsync(counter, counterParams.PageNumber, counterParams.PageSize);
    }



  }
}