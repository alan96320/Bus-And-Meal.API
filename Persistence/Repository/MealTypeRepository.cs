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
  public class MealTypeRepository : IMealtypeRepository
  {
    private readonly DataContext context;
    public MealTypeRepository(DataContext context)
    {
      this.context = context;
    }

    public void Add(MealType mealType)
    {
      context.MealType.Add(mealType);
    }

    public async Task<IEnumerable<MealType>> GetAll()
    {
      var mealType = await context.MealType.Include(m => m.mealVendor).ToListAsync();

      return mealType;
    }

    public async Task<MealType> GetOne(int id)
    {
      return await context.MealType.FindAsync(id);
    }

    public void Remove(MealType mealType)
    {
      context.Remove(mealType);
    }

    public async Task<PagedList<MealType>> GetPagedmealType(MealTypeParams mealTypeParams)
    {
      var mealType = context.MealType.Include(m => m.mealVendor).AsQueryable();

      // perlu user id untuk membatasi 
      if (!string.IsNullOrEmpty(mealTypeParams.Code))
      {
        mealType = mealType.Where(e =>
        e.Code.Contains(mealTypeParams.Code, StringComparison.OrdinalIgnoreCase));
      }

      if (!string.IsNullOrEmpty(mealTypeParams.Name))
      {
        mealType = mealType.Where(e =>
        e.Name.Contains(mealTypeParams.Name, StringComparison.OrdinalIgnoreCase));
      }

      if (mealTypeParams.MealVendorId > 0)
      {
        mealType = mealType.Where(u => u.MealVendorId == mealTypeParams.MealVendorId);
      }

      //name,sort
      if (mealTypeParams.isDescending)
      {
        if (!string.IsNullOrEmpty(mealTypeParams.OrderBy))
        {
          switch (mealTypeParams.OrderBy.ToLower())
          {
            case "code":
              mealType = mealType.OrderByDescending(e => e.Code);
              break;
            case "name":
              mealType = mealType.OrderByDescending(e => e.Name);
              break;
            case "lastname":
              mealType = mealType.OrderByDescending(e => e.MealVendorId);
              break;
            default:
              mealType = mealType.OrderByDescending(e => e.Code);
              break;
          }
        }
        else
        {
          mealType = mealType.OrderByDescending(e => e.Code);
        }

      }
      else
      {
        if (!string.IsNullOrEmpty(mealTypeParams.OrderBy))
        {
          switch (mealTypeParams.OrderBy.ToLower())
          {
            case "code":
              mealType = mealType.OrderBy(e => e.Code);
              break;
            case "name":
              mealType = mealType.OrderBy(e => e.Name);
              break;
            case "lastname":
              mealType = mealType.OrderBy(e => e.MealVendorId);
              break;
            default:
              mealType = mealType.OrderBy(e => e.Code);
              break;
          }
        }
        else
        {
          mealType = mealType.OrderBy(e => e.Code);
        }
      }

      var mealTypeToReturn = mealType.Include(m => m.mealVendor);

      return await PagedList<MealType>
          .CreateAsync(mealTypeToReturn, mealTypeParams.PageNumber, mealTypeParams.PageSize);
    }

  }
}


