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
    public void Add(MealOrder mealOrder)
    {
      context.MealOrder.Add(mealOrder);
    }

    public async Task<IEnumerable<MealOrder>> GetAll(int? userId = null)
    {
      var mealOrders = context.MealOrder.Include(m => m.MealOrderDetails).AsQueryable();

      if (userId != null)
      {
        var user = context.User.FirstOrDefault(u => u.Id == userId);
        if (user.AdminStatus != true)
        {
          mealOrders = mealOrders.Where(bo => bo.UserId == userId);
        }
      }
      return await mealOrders.ToListAsync();
    }

    public async Task<MealOrder> GetOne(int id, int? userId = null)
    {
      if (userId != null)
      {
        return await context.MealOrder.Include(b => b.MealOrderDetails)
                                      .FirstOrDefaultAsync(b => b.Id == id);
      }
      else
      {
        return await context.MealOrder.Include(b => b.MealOrderDetails)
                                     .FirstOrDefaultAsync(b => b.Id == id);
      }
    }

    public async Task<PagedList<MealOrder>> GetPagedMealOrder(MealOrderParams mealOrderParams, int? userId = null)
    {
      var mealOrders = context.MealOrder.Include(m => m.MealOrderDetails).AsQueryable();

      if (userId != null)
      {
        var user = context.User.FirstOrDefault(u => u.Id == userId);
        if (user.AdminStatus != true)
        {
          mealOrders = mealOrders.Where(bo => bo.UserId == userId);
        }
      }

      if (DateTime.Compare(mealOrderParams.StartDate, new DateTime(01, 1, 1)) != 0 && DateTime.Compare(mealOrderParams.EndDate, new DateTime(01, 1, 1)) != 0)
      {
        mealOrders = mealOrders.Where(m => m.OrderEntryDate.Date >= mealOrderParams.StartDate.Date && m.OrderEntryDate.Date <= mealOrderParams.EndDate.Date);
      }

      if (DateTime.Compare(mealOrderParams.OrderEntryDate, new DateTime(01, 1, 1)) != 0)
      {
        mealOrders = mealOrders.Where(m => m.OrderEntryDate.Date == mealOrderParams.OrderEntryDate.Date);
      }

      if (mealOrderParams.isReadyToCollect == true)
      {
        mealOrders = mealOrders.Where(m => m.IsReadyToCollect == true);
      }

      // FIXME : harusnya department Name, bukan Id untuk filter. kecuali di frontend pakai comboxbox
      if (mealOrderParams.DepartmentId > 0)
      {
        mealOrders = mealOrders.Where(m => m.DepartmentId == mealOrderParams.DepartmentId);
      }
      // TODO : implmentasikan Filter pada Status 

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
            // TODO : implementasikan Sort pada Status
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

            // TODO : implementasikan Sort pada Status              

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
      var mealOrderToReturn = mealOrders.Include(m => m.MealOrderDetails);

      return await PagedList<MealOrder>.CreateAsync(mealOrders, mealOrderParams.PageNumber, mealOrderParams.PageSize);

    }

    public void Remove(MealOrder mealOrder)
    {
      context.Remove(mealOrder);
    }
  }
}