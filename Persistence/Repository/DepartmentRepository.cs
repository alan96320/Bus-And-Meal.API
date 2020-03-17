using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BusMeal.API.Core.Models;
using BusMeal.API.Helpers;
using BusMeal.API.Core.IRepository;
using BusMeal.API.Helpers.Params;

namespace BusMeal.API.Persistence.Repository
{
  public class DepartmentRepository : IDepartmentRepository
  {
    private readonly DataContext context;

    public DepartmentRepository(DataContext context)
    {
      this.context = context;
    }

    public async Task<Department> GetOne(int id)
    {
      return await context.Department.FindAsync(id);
    }

    public void Add(Department department)
    {
      context.Department.Add(department);
    }

    public void Update(Department department)
    {
      context.Department.Attach(department);
      this.context.Entry(department).State = EntityState.Modified;
    }

    public void Remove(Department department)
    {
      context.Remove(department);
    }

    public async Task<IEnumerable<Department>> GetAll()
    {
      var departments = await context.Department.ToListAsync();

      return departments;
    }

    public async Task<PagedList<Department>> GetPagedDepartments(DepartmentParams departmentParams)
    {
      var departments = context.Department.AsQueryable();

      // perlu user id untuk membatasi 

      if (!string.IsNullOrEmpty(departmentParams.Name))
      {
        departments = departments.Where(d =>
          d.Name.Contains(departmentParams.Name, StringComparison.OrdinalIgnoreCase));
      }

      if (!string.IsNullOrEmpty(departmentParams.Code))
      {
        departments = departments.Where(d =>
          d.Code.Contains(departmentParams.Code, StringComparison.OrdinalIgnoreCase));
      }

      //name,sort
      if (departmentParams.isDescending)
      {
        if (!string.IsNullOrEmpty(departmentParams.OrderBy))
        {
          switch (departmentParams.OrderBy.ToLower())
          {
            case "code":
              departments = departments.OrderByDescending(d => d.Code);
              break;
            case "name":
              departments = departments.OrderByDescending(d => d.Name);
              break;
            default:
              departments = departments.OrderByDescending(d => d.Code);
              break;
          }
        }
        else
        {
          departments = departments.OrderByDescending(d => d.Code);
        }
      }
      else
      {
        if (!string.IsNullOrEmpty(departmentParams.OrderBy))
        {
          switch (departmentParams.OrderBy.ToLower())
          {
            case "code":
              departments = departments.OrderBy(d => d.Code);
              break;
            case "name":
              departments = departments.OrderBy(d => d.Name);
              break;
            default:
              departments = departments.OrderBy(d => d.Code);
              break;
          }
        }
        else
        {
          departments = departments.OrderBy(d => d.Code);
        }
      }

      return await PagedList<Department>
        .CreateAsync(departments, departmentParams.PageNumber, departmentParams.PageSize);
    }

  }
}