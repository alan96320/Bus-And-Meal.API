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
  public class MealVerificationRepository : IMealVerificationRepository
  {
    private readonly DataContext context;

    public MealVerificationRepository(DataContext context)
    {
      this.context = context;
    }

    public void Add(MealOrderVerificationHeader mealOrderVerificationHeader)
    {
      context.MealOrderVerificationHeader.Add(mealOrderVerificationHeader);
    }

    public async Task<IEnumerable<MealOrderVerificationHeader>> GetAll()
    {
      var mealVerifications = await context.MealOrderVerificationHeader
      .Include(m => m.MealVerificationTotal)
        .ThenInclude(m => m.MealType)
      .ToListAsync();

      return mealVerifications;
    }

    public async Task<MealOrderVerificationHeader> GetOne(int id)
    {
      return await context.MealOrderVerificationHeader
      .Include(m => m.MealVerificationTotal)
        .ThenInclude(m => m.MealType)
      .FirstOrDefaultAsync(m => m.Id == id);
    }

    public async Task<PagedList<MealOrderVerificationHeader>> GetPagedMealVerification(MealVerificationParams mealVerificationParams)
    {
      var mealVerifications = context.MealOrderVerificationHeader.Include(m => m.MealVerificationTotal).AsQueryable();

      // auth by user id
      if (!string.IsNullOrEmpty(mealVerificationParams.OrderNo))
      {
        mealVerifications = mealVerifications.Where(m => m.OrderNo.Contains(mealVerificationParams.OrderNo, StringComparison.OrdinalIgnoreCase));
      }

      if (DateTime.Compare(mealVerificationParams.OrderDate, new DateTime(01, 1, 1)) != 0)
      {
        mealVerifications = mealVerifications.Where(m => m.OrderDate.Date == mealVerificationParams.OrderDate.Date);
      }

      if (mealVerificationParams.OrderedStatus)
      {
        mealVerifications = mealVerifications.Where(m => m.OrderedStatus == mealVerificationParams.OrderedStatus);
      }

      // Sort
      if (mealVerificationParams.isDescending)
      {
        if (!string.IsNullOrEmpty(mealVerificationParams.OrderBy))
        {
          switch (mealVerificationParams.OrderBy.ToLower())
          {
            case "orderno":
              mealVerifications = mealVerifications.OrderByDescending(m => m.OrderNo);
              break;
            case "orderdate":
              mealVerifications = mealVerifications.OrderByDescending(m => m.OrderDate);
              break;
            case "orderstatus":
              mealVerifications = mealVerifications.OrderByDescending(m => m.OrderedStatus);
              break;
            default:
              mealVerifications = mealVerifications.OrderByDescending(m => m.OrderNo);
              break;
          }
        }
        else
        {
          mealVerifications = mealVerifications.OrderByDescending(m => m.OrderNo);
        }
      }
      else
      {
        if (!string.IsNullOrEmpty(mealVerificationParams.OrderBy))
        {
          switch (mealVerificationParams.OrderBy.ToLower())
          {
            case "orderno":
              mealVerifications = mealVerifications.OrderBy(m => m.OrderNo);
              break;
            case "orderdate":
              mealVerifications = mealVerifications.OrderBy(m => m.OrderDate);
              break;
            case "orderstatus":
              mealVerifications = mealVerifications.OrderBy(m => m.OrderedStatus);
              break;
            default:
              mealVerifications = mealVerifications.OrderBy(m => m.OrderNo);
              break;
          }
        }
        else
        {
          mealVerifications = mealVerifications.OrderBy(m => m.OrderNo);
        }
      }
      var mealVerificationsToReturn = mealVerifications.Include(m => m.MealVerificationTotal);

      return await PagedList<MealOrderVerificationHeader>.CreateAsync(mealVerificationsToReturn, mealVerificationParams.PageNumber, mealVerificationParams.PageSize);

    }
    public void Remove(MealOrderVerificationHeader mealOrderVerificationHeader)
    {
      context.Remove(mealOrderVerificationHeader);
    }
  }
}