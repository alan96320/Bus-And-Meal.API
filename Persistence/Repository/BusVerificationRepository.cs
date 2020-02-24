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
  public class BusVerificationRepository : IBusVerificationRepository
  {
    private readonly DataContext context;

    public BusVerificationRepository(DataContext context)
    {
      this.context = context;
    }

    public void Add(BusOrderVerificationHeader busOrderVerificationHeader)
    {
      context.BusOrderVerificationHeader.Add(busOrderVerificationHeader);
    }

    public async Task<IEnumerable<BusOrderVerificationHeader>> GetAll()
    {
      var busVerifications = await context.BusOrderVerificationHeader.Include(b => b.BusOrderVerificationDetail).ToListAsync();

      return busVerifications;
    }

    public async Task<BusOrderVerificationHeader> GetOne(int id)
    {
      return await context.BusOrderVerificationHeader.Include(b => b.BusOrderVerificationDetail).FirstOrDefaultAsync(b => b.Id == id);
    }

    public async Task<PagedList<BusOrderVerificationHeader>> GetPagedBusOrderVerification(BusVerificationParams busVerificationParams)
    {
      var busVerifications = context.BusOrderVerificationHeader.Include(b => b.BusOrderVerificationDetail).AsQueryable();

      // auth by user
      if (!string.IsNullOrEmpty(busVerificationParams.OrderNo))
      {
        busVerifications = busVerifications.Where(b => b.OrderNo.Contains(busVerificationParams.OrderNo, StringComparison.OrdinalIgnoreCase));
      }

      if (DateTime.Compare(busVerificationParams.OrderDate, new DateTime(01, 1, 1)) != 0)
      {
        busVerifications = busVerifications.Where(b => b.Orderdate.Date == busVerificationParams.OrderDate.Date);
      }

      if (busVerificationParams.OrderedStatus)
      {
        busVerifications = busVerifications.Where(b => b.OrderStatus == busVerificationParams.OrderedStatus);
      }

      // Sort
      if (busVerificationParams.isDescending)
      {
        if (!string.IsNullOrEmpty(busVerificationParams.OrderBy))
        {
          switch (busVerificationParams.OrderBy.ToLower())
          {
            case "orderno":
              busVerifications = busVerifications.OrderByDescending(b => b.OrderNo);
              break;
            case "orderdate":
              busVerifications = busVerifications.OrderByDescending(b => b.Orderdate);
              break;
            case "orderstatus":
              busVerifications = busVerifications.OrderByDescending(b => b.OrderStatus);
              break;
            default:
              busVerifications = busVerifications.OrderByDescending(b => b.OrderNo);
              break;
          }
        }
        else
        {
          busVerifications = busVerifications.OrderByDescending(b => b.OrderNo);
        }
      }
      else
      {
        if (!string.IsNullOrEmpty(busVerificationParams.OrderBy))
        {
          switch (busVerificationParams.OrderBy.ToLower())
          {
            case "orderno":
              busVerifications = busVerifications.OrderBy(b => b.OrderNo);
              break;
            case "orderdate":
              busVerifications = busVerifications.OrderBy(b => b.Orderdate);
              break;
            case "orderstatus":
              busVerifications = busVerifications.OrderBy(b => b.OrderStatus);
              break;
            default:
              busVerifications = busVerifications.OrderBy(b => b.OrderNo);
              break;
          }
        }
        else
        {
          busVerifications = busVerifications.OrderBy(b => b.OrderNo);
        }
      }
      var busVerificationToReturn = busVerifications.Include(b => b.BusOrderVerificationDetail);

      return await PagedList<BusOrderVerificationHeader>.CreateAsync(busVerificationToReturn, busVerificationParams.PageNumber, busVerificationParams.PageSize);
    }

    public void Remove(BusOrderVerificationHeader busOrderVerificationHeader)
    {
      context.Remove(busOrderVerificationHeader);
    }
  }
}