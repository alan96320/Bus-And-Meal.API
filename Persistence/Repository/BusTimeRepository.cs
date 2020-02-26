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
  public class BusTimeRepository : IBusTimeRepository
  {
    private readonly DataContext context;
    public BusTimeRepository(DataContext context)
    {
      this.context = context;
    }

    public void Add(BusTime busTime)
    {
      context.BusTime.Add(busTime);
    }

    public async Task<IEnumerable<BusTime>> GetAll()
    {
      var busTime = await context.BusTime.ToListAsync();
      return busTime;
    }

    public async Task<BusTime> GetOne(int id)
    {
      return await context.BusTime.FindAsync(id);
    }

    public void Remove(BusTime busTime)
    {
      context.Remove(busTime);
    }
    public async Task<PagedList<BusTime>> GetPagedBusTimes(BusTimeParams busTimeParams)
    {
      var busTime = context.BusTime.AsQueryable();

      // perlu user id untuk membatasi 
      if (!string.IsNullOrEmpty(busTimeParams.Code))
      {
        busTime = busTime.Where(b => b.Code.Contains(busTimeParams.Code, StringComparison.OrdinalIgnoreCase));
      }

      if (!string.IsNullOrEmpty(busTimeParams.Time))
      {
        busTime = busTime.Where(b => b.Time.Contains(busTimeParams.Time, StringComparison.OrdinalIgnoreCase));
      }

      if (busTimeParams.DirectionEnum > 0)
      {
        busTime = busTime.Where(b => b.DirectionEnum == busTimeParams.DirectionEnum);
      }

      //name,sort
      if (busTimeParams.isDescending)
      {
        if (!string.IsNullOrEmpty(busTimeParams.OrderBy))
        {
          switch (busTimeParams.OrderBy.ToLower())
          {
            case "code":
              busTime = busTime.OrderByDescending(b => b.Code);
              break;
            case "time":
              busTime = busTime.OrderByDescending(b => b.Time);
              break;
            case "directionenum":
              busTime = busTime.OrderByDescending(b => b.DirectionEnum);
              break;
            default:
              busTime = busTime.OrderByDescending(b => b.Code);
              break;
          }
        }
        else
        {
          busTime = busTime.OrderByDescending(b => b.Code);
        }

      }
      else
      {
        if (!string.IsNullOrEmpty(busTimeParams.OrderBy))
        {
          switch (busTimeParams.OrderBy.ToLower())
          {
            case "code":
              busTime = busTime.OrderBy(b => b.Code);
              break;
            case "time":
              busTime = busTime.OrderBy(b => b.Time);
              break;
            case "directionenum":
              busTime = busTime.OrderBy(b => b.DirectionEnum);
              break;
            default:
              busTime = busTime.OrderBy(b => b.Code);
              break;
          }
        }
        else
        {
          busTime = busTime.OrderBy(b => b.Code);
        }
      }
      return await PagedList<BusTime>
          .CreateAsync(busTime, busTimeParams.PageNumber, busTimeParams.PageSize);
    }


  }
}