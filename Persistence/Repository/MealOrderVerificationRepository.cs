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
  public class MealOrderVerificationRepository : IMealOrderVerificationRepository
  {
    private readonly DataContext context;

    public MealOrderVerificationRepository(DataContext context)
    {
      this.context = context;
    }

    public void Add(MealOrderVerification mealOrderVerification)
    {
      context.MealOrderVerification.Add(mealOrderVerification);
    }

    public async Task<IEnumerable<MealOrderVerification>> GetAll()
    {
      var mealOrderVerifications = await context.MealOrderVerification
        .Include(m => m.MealOrders.Where(mo => mo.IsReadyToCollect == true))
        .Include(m => m.MealOrderVerificationDetails)
        .ToListAsync();

      return mealOrderVerifications;
    }

    public async Task<MealOrderVerification> GetOne(int id)
    {
      return await context.MealOrderVerification
        .Include(m => m.MealOrders.Where(mo => mo.IsReadyToCollect == true))
        .Include(m => m.MealOrderVerificationDetails)
        .FirstOrDefaultAsync(m => m.Id == id);
    }

    public async Task<PagedList<MealOrderVerification>> GetPagedMealOrderVerification(MealOrderVerificationParams mealOrderVerificationParams)
    {
      var mealOrderVerifications = context.MealOrderVerification
      .Include(m => m.MealOrders.Where(mo => mo.IsReadyToCollect == true))
      .Include(m => m.MealOrderVerificationDetails)
      .AsQueryable();

      if (!string.IsNullOrEmpty(mealOrderVerificationParams.OrderNo))
      {
        mealOrderVerifications = mealOrderVerifications.Where(m => m.OrderNo.Contains(mealOrderVerificationParams.OrderNo, StringComparison.OrdinalIgnoreCase));
      }

      if (DateTime.Compare(mealOrderVerificationParams.OrderDate, new DateTime(01, 1, 1)) != 0)
      {
        mealOrderVerifications = mealOrderVerifications.Where(m => m.OrderDate.Date == mealOrderVerificationParams.OrderDate.Date);
      }

      // FIXME : OrderStatus = Closed, Open
      // if (mealOrderVerificationParams.OrderStatus)
      // {
      //   mealOrderVerifications = mealOrderVerifications.Where(m => m.OrderedStatus == mealVerificationParams.OrderedStatus);
      // }

      // Sort
      if (mealOrderVerificationParams.isDescending)
      {
        if (!string.IsNullOrEmpty(mealOrderVerificationParams.OrderBy))
        {
          switch (mealOrderVerificationParams.OrderBy.ToLower())
          {
            case "orderno":
              mealOrderVerifications = mealOrderVerifications.OrderByDescending(m => m.OrderNo);
              break;
            case "orderdate":
              mealOrderVerifications = mealOrderVerifications.OrderByDescending(m => m.OrderDate);
              break;
            // FIXME : Orderstatus = Closed, Open
            // case "orderstatus":
            //   mealVerifications = mealVerifications.OrderByDescending(m => m.OrderedStatus);
            //   break;
            default:
              mealOrderVerifications = mealOrderVerifications.OrderByDescending(m => m.OrderDate);
              break;
          }
        }
        else
        {
          mealOrderVerifications = mealOrderVerifications.OrderByDescending(m => m.OrderDate);
        }
      }
      else
      {
        if (!string.IsNullOrEmpty(mealOrderVerificationParams.OrderBy))
        {
          switch (mealOrderVerificationParams.OrderBy.ToLower())
          {
            case "orderno":
              mealOrderVerifications = mealOrderVerifications.OrderBy(m => m.OrderNo);
              break;
            case "orderdate":
              mealOrderVerifications = mealOrderVerifications.OrderBy(m => m.OrderDate);
              break;
            // FIXME : Orderstatus = Closed, Open
            // case "orderstatus":
            //   mealVerifications = mealVerifications.OrderBy(m => m.OrderedStatus);
            //   break;
            default:
              mealOrderVerifications = mealOrderVerifications.OrderBy(m => m.OrderDate);
              break;
          }
        }
        else
        {
          mealOrderVerifications = mealOrderVerifications.OrderBy(m => m.OrderDate);
        }
      }
      var mealOrderVerificationsToReturn = mealOrderVerifications.Include(m => m.MealOrderVerificationDetails);

      return await PagedList<MealOrderVerification>.CreateAsync(mealOrderVerificationsToReturn, mealOrderVerificationParams.PageNumber, mealOrderVerificationParams.PageSize);

    }
    public void Remove(MealOrderVerification mealOrderVerification)
    {
      context.Remove(mealOrderVerification);
    }
  }
}