using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BusMeal.API.Core;
using BusMeal.API.Core.Models;
using BusMeal.API.Helpers;

namespace BusMeal.API.Persistence
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
      return await context.Departments.FindAsync(id);
    }

    public void Add(Department department)
    {
      context.Departments.Add(department);
    }

    public void Update(Department department)
    {
      // context.Departments.Add(department);
      context.Departments.Entry(department).State = EntityState.Modified;
      // this.context.Entry(department).State = EntityState.Modified;
    }

    public void Remove(Department department)
    {
      context.Remove(department);
    }

    public async Task<IEnumerable<Department>> GetAll()
    {
      var departments = await context.Departments.ToListAsync();

      return departments;
    }

    public async Task<PagedList<Department>> GetPagedDepartments(DepartmentParams departmentParams)
    {

      var departments = context.Departments.AsQueryable();

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
      if (!string.IsNullOrEmpty(departmentParams.OrderBy))
      {
        switch (departmentParams.OrderBy)
        {
          case "name":
            switch (departmentParams.OrderDir)
            {
              case "desc":
                departments.OrderByDescending(d => d.Name);
                break;

              default:
                departments.OrderBy(d => d.Name);
                break;
            }

            break;
          default:
            switch (departmentParams.OrderDir)
            {
              case "desc":
                departments.OrderByDescending(d => d.Code);
                break;

              default:
                departments.OrderBy(d => d.Code);
                break;
            }
            break;
        }
      }

      return await PagedList<Department>
        .CreateAsync(departments, departmentParams.PageNumber, departmentParams.PageSize);
    }
  }
}
