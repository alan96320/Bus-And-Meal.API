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

    public async Task<MealType> isVendorDuplicate(int vendorId)
    {
      return await context.MealType.Where(m => m.MealVendorId == vendorId).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<MealType>> GetAll()
    {
      var mealType = await context.MealType.Include(m => m.MealVendor).ToListAsync();

      return mealType;
    }

    public async Task<MealType> GetOne(int id)
    {
      return await context.MealType.Include(m => m.MealVendor).FirstOrDefaultAsync(m => m.Id == id);
    }

    public void Remove(MealType mealType)
    {
      context.Remove(mealType);
    }

    public async Task<PagedList<MealType>> GetPagedmealType(MealTypeParams mealTypeParams)
    {
      var mealTypes = context.MealType.Include(m => m.MealVendor).AsQueryable();

      // perlu user id untuk membatasi 
      if (!string.IsNullOrEmpty(mealTypeParams.Code))
      {
        mealTypes = mealTypes.Where(m =>
        m.Code.Contains(mealTypeParams.Code, StringComparison.OrdinalIgnoreCase));
      }

      if (!string.IsNullOrEmpty(mealTypeParams.Name))
      {
        mealTypes = mealTypes.Where(m =>
        m.Name.Contains(mealTypeParams.Name, StringComparison.OrdinalIgnoreCase));
      }

      if (!string.IsNullOrEmpty(mealTypeParams.vendorName))
      {
        mealTypes = mealTypes.Where(m => m.MealVendor.Name.Contains(mealTypeParams.vendorName, StringComparison.OrdinalIgnoreCase));
      }

      //name,sort
      if (mealTypeParams.isDescending)
      {
        if (!string.IsNullOrEmpty(mealTypeParams.OrderBy))
        {
          switch (mealTypeParams.OrderBy.ToLower())
          {
            case "code":
              mealTypes = mealTypes.OrderByDescending(m => m.Code);
              break;
            case "name":
              mealTypes = mealTypes.OrderByDescending(m => m.Name);
              break;
            case "vendorname":
              mealTypes = mealTypes.OrderByDescending(m => m.MealVendor.Name);
              break;
            default:
              mealTypes = mealTypes.OrderByDescending(m => m.Code);
              break;
          }
        }
        else
        {
          mealTypes = mealTypes.OrderByDescending(m => m.Code);
        }

      }
      else
      {
        if (!string.IsNullOrEmpty(mealTypeParams.OrderBy))
        {
          switch (mealTypeParams.OrderBy.ToLower())
          {
            case "code":
              mealTypes = mealTypes.OrderBy(m => m.Code);
              break;
            case "name":
              mealTypes = mealTypes.OrderBy(m => m.Name);
              break;
            case "vendorname":
              mealTypes = mealTypes.OrderBy(m => m.MealVendor.Name);
              break;
            default:
              mealTypes = mealTypes.OrderBy(m => m.Code);
              break;
          }
        }
        else
        {
          mealTypes = mealTypes.OrderBy(m => m.Code);
        }
      }
      return await PagedList<MealType>
          .CreateAsync(mealTypes, mealTypeParams.PageNumber, mealTypeParams.PageSize);
    }

  }
}


