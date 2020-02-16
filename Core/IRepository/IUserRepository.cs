using System.Collections.Generic;
using System.Threading.Tasks;
using BusMeal.API.Core.Models;
using BusMeal.API.Helpers;

namespace BusMeal.API.Core
{
  public interface IUserRepository
  {
    Task<User> GetOne(int id);
    void Add(User user);
    void Remove(User user);
    Task<IEnumerable<User>> GetAll();
    Task<PagedList<User>> GetPagedUsers(UserParams userParam);
  }
}