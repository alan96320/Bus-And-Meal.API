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
  public class BusOrderRepository : IBusOrderRepository
  {
    private readonly DataContext context;

    public BusOrderRepository(DataContext context)
    {
      this.context = context;
    }

    public void Add(BusOrder busOrder)
    {
      context.BusOrder.Add(busOrder);
    }

    public async Task<IEnumerable<BusOrder>> GetAll(int? userId = null)
    {
      // verifikasi dgn userId atau Admin
      var busOrders = context.BusOrder.Include(b => b.BusOrderDetails).AsQueryable();
      if (userId != null)
      {
        var user = context.User.FirstOrDefault(u => u.Id == userId);
        if (user.AdminStatus != true)
        {
          busOrders = busOrders.Where(bo => bo.UserId == userId);
        }
      }
      return await busOrders.ToListAsync();
    }

    public async Task<BusOrder> GetOne(int id, int? userId = null)
    {
      // FIXME : cari alternative lain
      if (userId != null)
      {
        return await context.BusOrder.Include(b => b.BusOrderDetails)
                                    .FirstOrDefaultAsync(b => b.Id == id);
      }
      else
      {
        return await context.BusOrder.Include(b => b.BusOrderDetails)
                                     .FirstOrDefaultAsync(b => b.Id == id);
      }
    }

    public async Task<PagedList<BusOrder>> GetPagedBusOrder(BusOrderParams busOrderParams, int? userId = null)
    {
      var busOrders = context.BusOrder.Include(b => b.BusOrderDetails).AsQueryable();

      if (userId != null)
      {
        var user = context.User.FirstOrDefault(u => u.Id == userId);
        if (user.AdminStatus != true)
        {
          busOrders = busOrders.Where(bo => bo.UserId == userId);
        }
      }

      if (DateTime.Compare(busOrderParams.StartDate, new DateTime(01, 1, 1)) != 0 && DateTime.Compare(busOrderParams.EndDate, new DateTime(01, 1, 1)) != 0)
      {
        busOrders = busOrders.Where(m => m.OrderEntryDate.Date >= busOrderParams.StartDate.Date && m.OrderEntryDate.Date <= busOrderParams.EndDate.Date);
      }

      if (DateTime.Compare(busOrderParams.OrderEntryDate, new DateTime(01, 1, 1)) != 0)
      {
        busOrders = busOrders.Where(b => b.OrderEntryDate.Date == busOrderParams.OrderEntryDate.Date);
      }

      if (busOrderParams.isReadyToCollect == true)
      {
        busOrders = busOrders.Where(b => b.IsReadyToCollect == true);
      }

      // FIXME : seharusnya bukan departmentId tetapi departementcode atau departmentName

      if (busOrderParams.DepartmentId > 0)
      {
        busOrders = busOrders.Where(b => b.DepartmentId == busOrderParams.DepartmentId);
      }

      if (busOrderParams.DormitoryBlockId > 0)
      {
        busOrders = busOrders.Where(b => b.DormitoryBlockId == busOrderParams.DormitoryBlockId);
      }

      // TODO  : Filter by Status ? Locked, Closed --> sebaiknya Locked dihilangkan, dan diganti dgn Open

      // Sort
      if (busOrderParams.isDescending)
      {
        if (!string.IsNullOrEmpty(busOrderParams.OrderBy))
        {
          switch (busOrderParams.OrderBy.ToLower())
          {
            case "orderentrydate":
              busOrders = busOrders.OrderByDescending(b => b.OrderEntryDate);
              break;

            // FIXME : seharusnya bukan departmentId tetapi departementcode atau departmentName , kecuali di FE pakai combobox                           
            case "departmentid":
              busOrders = busOrders.OrderByDescending(b => b.DepartmentId);
              break;
            // FIXME : seharusnya bukan BlockId tetapi dormitoryBlockcname, kecuali di FE pakai combobox                           

            case "dormitoryblockid":
              busOrders = busOrders.OrderByDescending(b => b.DormitoryBlockId);
              break;
            default:
              busOrders = busOrders.OrderByDescending(b => b.OrderEntryDate);
              break;
              // TODO  : Filter by Status ? Locked, Closed --> sebaiknya Locked dihilangkan, dan diganti dgn Open              
          }
        }
        else
        {
          busOrders = busOrders.OrderByDescending(b => b.OrderEntryDate);
        }
      }
      else
      {
        if (!string.IsNullOrEmpty(busOrderParams.OrderBy))
        {
          switch (busOrderParams.OrderBy.ToLower())
          {
            case "orderentrydate":
              busOrders = busOrders.OrderBy(b => b.OrderEntryDate);
              break;
            // FIXME : seharusnya bukan departmentId tetapi departementcode atau departmentName , kecuali di FE pakai combobox                           
            case "departmentid":
              busOrders = busOrders.OrderBy(b => b.DepartmentId);
              break;
            // FIXME : seharusnya bukan BlockId tetapi dormitoryBlockcname, kecuali di FE pakai combobox                           

            case "dormitoryblockid":
              busOrders = busOrders.OrderBy(b => b.DormitoryBlockId);
              break;
            default:
              busOrders = busOrders.OrderBy(b => b.OrderEntryDate);
              break;
              // TODO  : Filter by Status ? Locked, Closed --> sebaiknya Locked dihilangkan, dan diganti dgn Open                            
          }
        }
        else
        {
          busOrders = busOrders.OrderBy(b => b.OrderEntryDate);
        }
      }

      var busOrderToReturn = busOrders.Include(b => b.BusOrderDetails);

      return await PagedList<BusOrder>.CreateAsync(busOrderToReturn, busOrderParams.PageNumber, busOrderParams.PageSize);

    }

    public void Remove(BusOrder busOrder)
    {
      context.Remove(busOrder);
    }

  }
}