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
  public class EmployeeRepository : IEmployeeRepository
  {
    private readonly DataContext context;
    public EmployeeRepository(DataContext context)
    {
      this.context = context;
    }
    public async Task<Employee> GetOne(int id)
    {
      return await context.Employee
            .Include(e => e.Department)
            .FirstOrDefaultAsync(e => e.Id == id);
    }
    public void Add(Employee employee)
    {
      context.Employee.Add(employee);
    }

    public void Update(Employee employee)
    {
      context.Employee.Attach(employee);
      this.context.Entry(employee).State = EntityState.Modified;
    }

    public void Remove(Employee employee)
    {
      context.Remove(employee);
    }

    public async Task<IEnumerable<Employee>> GetAll()
    {
      var employees = await context.Employee
                      .Include( e => e.Department)
                      .ToListAsync();

      return employees;
    }

    public async Task<PagedList<Employee>> GetPagedEmployees(EmployeeParams employeeParams)
    {
      var employees = context.Employee
                        .Include( e => e.Department)
                        .AsQueryable();

      // perlu user id untuk membatasi 
      if (!string.IsNullOrEmpty(employeeParams.HrCoreNo))
      {
        employees = employees.Where(e =>
          e.HrCoreNo.Contains(employeeParams.HrCoreNo, StringComparison.OrdinalIgnoreCase));
      }

      if (!string.IsNullOrEmpty(employeeParams.Firstname))
      {
        employees = employees.Where(e =>
          e.Firstname.Contains(employeeParams.Firstname, StringComparison.OrdinalIgnoreCase));
      }

      if (!string.IsNullOrEmpty(employeeParams.Lastname))
      {
        employees = employees.Where(e =>
          e.Lastname.Contains(employeeParams.Lastname, StringComparison.OrdinalIgnoreCase));
      }

      if (!string.IsNullOrEmpty(employeeParams.DepartmentCode))
      {
        employees = employees.Where(e =>
          e.Department.Code.Contains(employeeParams.DepartmentCode, StringComparison.OrdinalIgnoreCase));
      }

      if (!string.IsNullOrEmpty(employeeParams.DepartmentName))
      {
        employees = employees.Where(e =>
          e.Department.Name.Contains(employeeParams.DepartmentName, StringComparison.OrdinalIgnoreCase));
      }


      //name,sort
      if (employeeParams.isDescending)
      {
        if (!string.IsNullOrEmpty(employeeParams.OrderBy))
        {
          switch (employeeParams.OrderBy.ToLower())
          {
            case "hrcoreno":
              employees = employees.OrderByDescending(e => e.HrCoreNo);
              break;
            case "firstname":
              employees = employees.OrderByDescending(e => e.Firstname);
              break;
            case "lastname":
              employees = employees.OrderByDescending(e => e.Lastname);
              break;
            case "departmentcode":
              employees = employees.OrderByDescending(e => e.Department.Code);
              break;
            case "departmentname":            
              employees = employees.OrderByDescending(e => e.Department.Name);
              break;
            default:
              employees = employees.OrderByDescending(e => e.HrCoreNo);
              break;
          }
        }
        else
        {
          employees = employees.OrderByDescending(e => e.HrCoreNo);
        }

      }
      else
      {
        if (!string.IsNullOrEmpty(employeeParams.OrderBy))
        {
          switch (employeeParams.OrderBy.ToLower())
          {
            case "hrcoreno":
              employees = employees.OrderBy(e => e.HrCoreNo);
              break;
            case "firstname":
              employees = employees.OrderBy(e => e.Firstname);
              break;
            case "lastname":
              employees = employees.OrderBy(e => e.Lastname);
              break;
            case "departmentcode":
              employees = employees.OrderByDescending(e => e.Department.Code);
              break;
            case "departmentname":            
              employees = employees.OrderByDescending(e => e.Department.Name);
              break;
            default:
              employees = employees.OrderBy(e => e.HrCoreNo);
              break;
              
          }
        }
        else
        {
          employees = employees.OrderBy(e => e.HrCoreNo);
        }
      }
      return await PagedList<Employee>
        .CreateAsync(employees, employeeParams.PageNumber, employeeParams.PageSize);
    }
  }
}