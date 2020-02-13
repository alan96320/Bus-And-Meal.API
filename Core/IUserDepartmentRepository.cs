using System.Collections.Generic;
using System.Threading.Tasks;
using BusMeal.API.Core.Models;
using BusMeal.API.Helpers;

namespace BusMeal.API.Core
{
  public interface IUserDepartmentRepository
  {
    Task<UserDepartment> GetOne(int id);
    void Add(UserDepartment userDepartment);
    void Remove(UserDepartment userDepartment);
    Task<IEnumerable<UserDepartment>> GetAll();
    Task<PagedList<UserDepartment>> GetPagedUserDepartment(UserDepartmentParams userDepartmentParams);
  }
}