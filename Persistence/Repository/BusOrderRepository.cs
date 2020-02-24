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

    public void Add(BusOrderEntryHeader busOrderEntryHeader)
    {
      context.BusOrderEntryHeader.Add(busOrderEntryHeader);
    }

    public async Task<IEnumerable<BusOrderEntryHeader>> GetAll()
    {
      var busOrders = await context.BusOrderEntryHeader.Include(b => b.BusOrderDetail).ToListAsync();

      return busOrders;
    }

    public async Task<BusOrderEntryHeader> GetOne(int id)
    {
      return await context.BusOrderEntryHeader.Include(b => b.BusOrderDetail).FirstOrDefaultAsync(b => b.Id == id);
    }

    public async Task<PagedList<BusOrderEntryHeader>> GetPagedBusOrder(BusOrderParams busOrderParams)
    {
      var busOrders = context.BusOrderEntryHeader.Include(b => b.BusOrderDetail).AsQueryable();

      // auth by user id
      if (DateTime.Compare(busOrderParams.OrderEntryDate, new DateTime(01, 1, 1)) != 0)
      {
        busOrders = busOrders.Where(b => b.OrderEntryDate.Date == busOrderParams.OrderEntryDate.Date);
      }

      if (busOrderParams.DepartmentId > 0)
      {
        busOrders = busOrders.Where(b => b.DepartmentId == busOrderParams.DepartmentId);
      }

      if (busOrderParams.DormitoryBlockId > 0)
      {
        busOrders = busOrders.Where(b => b.DormitoryBlockId == busOrderParams.DormitoryBlockId);
      }

      if (busOrderParams.BusOrderVerificationHeaderId > 0)
      {
        busOrders = busOrders.Where(b => b.BusOrderVerificationHeaderId == busOrderParams.BusOrderVerificationHeaderId);
      }

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
            case "departmentid":
              busOrders = busOrders.OrderByDescending(b => b.DepartmentId);
              break;
            case "dormitoryblockid":
              busOrders = busOrders.OrderByDescending(b => b.DormitoryBlockId);
              break;
            case "busorderverificationheaderid":
              busOrders = busOrders.OrderByDescending(b => b.BusOrderVerificationHeaderId);
              break;
            default:
              busOrders = busOrders.OrderByDescending(b => b.OrderEntryDate);
              break;
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
            case "departmentid":
              busOrders = busOrders.OrderBy(b => b.DepartmentId);
              break;
            case "dormitoryblockid":
              busOrders = busOrders.OrderBy(b => b.DormitoryBlockId);
              break;
            case "busorderverificationheaderid":
              busOrders = busOrders.OrderBy(b => b.BusOrderVerificationHeaderId);
              break;
            default:
              busOrders = busOrders.OrderBy(b => b.OrderEntryDate);
              break;
          }
        }
        else
        {
          busOrders = busOrders.OrderBy(b => b.OrderEntryDate);
        }
      }
      var busOrderToReturn = busOrders.Include(b => b.BusOrderDetail);

      return await PagedList<BusOrderEntryHeader>.CreateAsync(busOrderToReturn, busOrderParams.PageNumber, busOrderParams.PageSize);

    }

    public void Remove(BusOrderEntryHeader busOrderEntryHeader)
    {
      context.Remove(busOrderEntryHeader);
    }

  }
}