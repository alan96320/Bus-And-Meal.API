using System.Collections.Generic;
using System.Threading.Tasks;
using BusMeal.API.Core.Models;
using BusMeal.API.Helpers;

namespace BusMeal.API.Core
{
  public interface IEmployeeRepository
  {
    Task<Employee> GetOne(int id);
    void Add(Employee employee);
    void Remove(Employee employee);
    //  void Update(Employee employee)
    Task<IEnumerable<Employee>> GetAll();
    Task<PagedList<Employee>> GetPagedEmployees(EmployeeParams employeeParams);
  }
}