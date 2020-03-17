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
  public class MealVendorRepository : IMealVendorRepository
  {
    private readonly DataContext context;

    public MealVendorRepository(DataContext context)
    {
      this.context = context;
    }
    public void Add(MealVendor mealVendor)
    {
      context.MealVendor.Add(mealVendor);
    }

    public void Update(MealVendor mealVendor)
    {
      context.MealVendor.Attach(mealVendor);
      this.context.Entry(mealVendor).State = EntityState.Modified;
    }        
    

    public async Task<IEnumerable<MealVendor>> GetAll()
    {
      var mealvendors = await context.MealVendor.ToListAsync();

      return mealvendors;
    }

    public async Task<MealVendor> GetOne(int id)
    {
      return await context.MealVendor.FindAsync(id);
    }

    public async Task<PagedList<MealVendor>> GetPagedMealVendor(MealVendorParams mealVendorParams)
    {
      var mealvendors = context.MealVendor.AsQueryable();

      //   filter
      if (!string.IsNullOrEmpty(mealVendorParams.Code))
      {
        mealvendors = mealvendors.Where(m => m.Code.Contains(mealVendorParams.Code, StringComparison.OrdinalIgnoreCase));
      }

      if (!string.IsNullOrEmpty(mealVendorParams.Name))
      {
        mealvendors = mealvendors.Where(m => m.Name.Contains(mealVendorParams.Name, StringComparison.OrdinalIgnoreCase));
      }

      if (!string.IsNullOrEmpty(mealVendorParams.ContactName))
      {
        mealvendors = mealvendors.Where(m => m.ContactName.Contains(mealVendorParams.ContactName, StringComparison.OrdinalIgnoreCase));
      }

      if (!string.IsNullOrEmpty(mealVendorParams.ContactPhone))
      {
        mealvendors = mealvendors.Where(m => m.ContactPhone.Contains(mealVendorParams.ContactPhone, StringComparison.OrdinalIgnoreCase));
      }

      if (!string.IsNullOrEmpty(mealVendorParams.ContactEmail))
      {
        mealvendors = mealvendors.Where(m => m.ContactEmail.Contains(mealVendorParams.ContactEmail, StringComparison.OrdinalIgnoreCase));
      }

      //   sort
      if (mealVendorParams.isDescending)
      {
        if (!string.IsNullOrEmpty(mealVendorParams.OrderBy))
        {
          switch (mealVendorParams.OrderBy.ToLower())
          {
            case "code":
              mealvendors = mealvendors.OrderByDescending(m => m.Code);
              break;
            case "name":
              mealvendors = mealvendors.OrderByDescending(m => m.Name);
              break;
            case "contactname":
              mealvendors = mealvendors.OrderByDescending(m => m.ContactName);
              break;
            case "contactphone":
              mealvendors = mealvendors.OrderByDescending(m => m.ContactPhone);
              break;
            case "contactemail":
              mealvendors = mealvendors.OrderByDescending(m => m.ContactEmail);
              break;
            default:
              mealvendors = mealvendors.OrderByDescending(m => m.Code);
              break;
          }
        }
        else
        {
          mealvendors = mealvendors.OrderByDescending(m => m.Code);

        }
      }
      else
      {
        if (!string.IsNullOrEmpty(mealVendorParams.OrderBy))
        {
          switch (mealVendorParams.OrderBy.ToLower())
          {
            case "code":
              mealvendors = mealvendors.OrderBy(m => m.Code);
              break;
            case "name":
              mealvendors = mealvendors.OrderBy(m => m.Name);
              break;
            case "contactname":
              mealvendors = mealvendors.OrderBy(m => m.ContactName);
              break;
            case "contactphone":
              mealvendors = mealvendors.OrderBy(m => m.ContactPhone);
              break;
            case "contactemail":
              mealvendors = mealvendors.OrderBy(m => m.ContactEmail);
              break;
            default:
              mealvendors = mealvendors.OrderBy(m => m.Code);
              break;
          }
        }
        else
        {
          mealvendors = mealvendors.OrderBy(m => m.Code);

        }
      }

      return await PagedList<MealVendor>.CreateAsync(mealvendors, mealVendorParams.PageNumber, mealVendorParams.PageSize);
    }

    public void Remove(MealVendor mealVendor)
    {
      context.Remove(mealVendor);
    }
  }
}