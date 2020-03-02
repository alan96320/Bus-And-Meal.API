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
  public class BusOrderVerificationRepository : IBusOrderVerificationRepository
  {
    private readonly DataContext context;

    public BusOrderVerificationRepository(DataContext context)
    {
      this.context = context;
    }

    public void Add(BusOrderVerification busOrderVerification)
    {
      context.BusOrderVerification.Add(busOrderVerification);
    }

    public async Task<IEnumerable<BusOrderVerification>> GetAll()
    {
      var busOrderVerification = await context.BusOrderVerification.Include(b => b.BusOrderVerificationDetails).ToListAsync();

      return busOrderVerification;
    }

    public async Task<BusOrderVerification> GetOne(int id)
    {
      return await context.BusOrderVerification.Include(b => b.BusOrderVerificationDetails).FirstOrDefaultAsync(b => b.Id == id);
    }

    public async Task<PagedList<BusOrderVerification>> GetPagedBusOrderVerification(BusOrderVerificationParams busOrderVerificationParams)
    {
      var busOrderVerifications = context.BusOrderVerification.Include(b => b.BusOrderVerificationDetails).AsQueryable();

      // auth by user
      if (!string.IsNullOrEmpty(busOrderVerificationParams.OrderNo))
      {
        busOrderVerifications = busOrderVerifications.Where(b => b.OrderNo.Contains(busOrderVerificationParams.OrderNo, StringComparison.OrdinalIgnoreCase));
      }

      if (DateTime.Compare(busOrderVerificationParams.OrderDate, new DateTime(01, 1, 1)) != 0)
      {
        busOrderVerifications = busOrderVerifications.Where(b => b.Orderdate.Date == busOrderVerificationParams.OrderDate.Date);
      }

      if (!string.IsNullOrEmpty(busOrderVerificationParams.OrderStatus))
      {
          switch (busOrderVerificationParams.OrderStatus.ToLower())
          {
//            case "locked":
//                break;
            case "closed":
                busOrderVerifications = busOrderVerifications.Where(b => b.IsClosed == true);            
                break;
            default :
                break;
          }
      }

      // Sort
      if (busOrderVerificationParams.isDescending)
      {
        if (!string.IsNullOrEmpty(busOrderVerificationParams.OrderBy))
        {
          switch (busOrderVerificationParams.OrderBy.ToLower())
          {
            case "orderno":
              busOrderVerifications = busOrderVerifications.OrderByDescending(b => b.OrderNo);
              break;
            case "orderdate":
              busOrderVerifications = busOrderVerifications.OrderByDescending(b => b.Orderdate);
              break;
//            case "orderstatus":
//              busOrderVerifications = busOrderVerifications.OrderBy(b => b.OrderStatus);
            // TODO - order status adalah string, harus handle isClosed,isLocked -> ambil dari helper
            default:
              busOrderVerifications = busOrderVerifications.OrderByDescending(b => b.Orderdate);
              break;
          }
        }
        else
        {
          busOrderVerifications = busOrderVerifications.OrderByDescending(b => b.Orderdate);
        }
      }
      else
      {
        if (!string.IsNullOrEmpty(busOrderVerificationParams.OrderBy))
        {
          switch (busOrderVerificationParams.OrderBy.ToLower())
          {
            case "orderno":
              busOrderVerifications = busOrderVerifications.OrderBy(b => b.OrderNo);
              break;
            case "orderdate":
              busOrderVerifications = busOrderVerifications.OrderBy(b => b.Orderdate);
              break;
//            case "orderstatus":
//              busOrderVerifications = busOrderVerifications.OrderBy(b => b.OrderStatus);
            // TODO - order status adalah string, harus handle isClosed,isLocked -> ambil dari helper
            // sebaiknya isLocked dihilangkan, diganti dgn Open, dicegah waktu save saja jika sudah locked
//              break;
            default:
              busOrderVerifications = busOrderVerifications.OrderBy(b => b.Orderdate);
              break;
          }
        }
        else
        {
          busOrderVerifications = busOrderVerifications.OrderBy(b => b.Orderdate);
        }
      }
      var busOrderVerificationToReturn = busOrderVerifications.Include(b => b.BusOrderVerificationDetails);

      return await PagedList<BusOrderVerification>.CreateAsync(busOrderVerificationToReturn, busOrderVerificationParams.PageNumber, busOrderVerificationParams.PageSize);
    }

    public void Remove(BusOrderVerification busOrderVerification)
    {
      context.Remove(busOrderVerification);
    }
  }
}