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
  public class MealOrderRepository : IMealOrderRepository
  {
    private readonly DataContext context;

    public MealOrderRepository(DataContext context)
    {
      this.context = context;
    }
    public void Add(MealOrderEntryHeader mealOrderEntryHeader)
    {
      context.MealOrderEntryHeader.Add(mealOrderEntryHeader);
    }

    public async Task<IEnumerable<MealOrderEntryHeader>> GetAll()
    {
      var mealOrders = await context.MealOrderEntryHeader.Include(m => m.MealOrderDetail).ToListAsync();

      return mealOrders;
    }

    public async Task<MealOrderEntryHeader> GetOne(int id)
    {
      return await context.MealOrderEntryHeader.Include(m => m.MealOrderDetail).FirstOrDefaultAsync(m => m.Id == id);
    }

    public async Task<PagedList<MealOrderEntryHeader>> GetPagedMealOrderEntryHeader(MealOrderParams mealOrderParams)
    {
      var mealOrders = context.MealOrderEntryHeader.Include(m => m.MealOrderDetail).AsQueryable();

      // auth by user id
      if (DateTime.Compare(mealOrderParams.OrderEntryDate, new DateTime(01, 1, 1)) != 0)
      {
        mealOrders = mealOrders.Where(m => m.OrderEntryDate.ToString("yyyy/MM/dd") == mealOrderParams.OrderEntryDate.ToString("yyyy/MM/dd"));
      }

      if (mealOrderParams.DepartmentId > 0)
      {
        mealOrders = mealOrders.Where(m => m.DepartmentId == mealOrderParams.DepartmentId);
      }

      if (mealOrderParams.MealOrderVerificationHeaderId > 0)
      {
        mealOrders = mealOrders.Where(m => m.MealOrderVerificationHeaderId == mealOrderParams.MealOrderVerificationHeaderId);
      }

      // Sort
      if (mealOrderParams.isDescending)
      {
        if (!string.IsNullOrEmpty(mealOrderParams.OrderBy))
        {
          switch (mealOrderParams.OrderBy.ToLower())
          {
            case "orderentrydate":
              mealOrders = mealOrders.OrderByDescending(m => m.OrderEntryDate);
              break;
            case "departmentid":
              mealOrders = mealOrders.OrderByDescending(m => m.DepartmentId);
              break;
            case "mealorderverificationheaderid":
              mealOrders = mealOrders.OrderByDescending(m => m.MealOrderVerificationHeaderId);
              break;
            default:
              mealOrders = mealOrders.OrderByDescending(m => m.OrderEntryDate);
              break;
          }
        }
        else
        {
          mealOrders = mealOrders.OrderByDescending(m => m.OrderEntryDate);
        }
      }
      else
      {
        if (!string.IsNullOrEmpty(mealOrderParams.OrderBy))
        {
          switch (mealOrderParams.OrderBy.ToLower())
          {
            case "orderentrydate":
              mealOrders = mealOrders.OrderBy(m => m.OrderEntryDate);
              break;
            case "departmentid":
              mealOrders = mealOrders.OrderBy(m => m.DepartmentId);
              break;
            case "mealorderverificationheaderid":
              mealOrders = mealOrders.OrderBy(m => m.MealOrderVerificationHeaderId);
              break;
            default:
              mealOrders = mealOrders.OrderBy(m => m.OrderEntryDate);
              break;
          }
        }
        else
        {
          mealOrders = mealOrders.OrderBy(m => m.OrderEntryDate);
        }
      }
      var mealOrderToReturn = mealOrders.Include(m => m.MealOrderDetail);

      return await PagedList<MealOrderEntryHeader>.CreateAsync(mealOrders, mealOrderParams.PageNumber, mealOrderParams.PageSize);

    }

    public void Remove(MealOrderEntryHeader mealOrderEntryHeader)
    {
      context.Remove(mealOrderEntryHeader);
    }
  }
}