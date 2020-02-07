using System.Collections.Generic;
using System.Threading.Tasks;
using BusMeal.API.Core.Models;
using BusMeal.API.Helpers;

namespace BusMeal.API.Core
{
  public interface IDepartmentRepository
  {

    Task<Department> GetOne(int id);
    void Add(Department department);
    void Remove(Department department);
    void Update(Department department);
    Task<IEnumerable<Department>> GetAll();
    Task<PagedList<Department>> GetPagedDepartments(DepartmentParams departmentParams);

  }
}