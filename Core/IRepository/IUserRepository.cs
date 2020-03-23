using System.Collections.Generic;
using System.Threading.Tasks;
using BusMeal.API.Core.Models;
using BusMeal.API.Helpers;
using BusMeal.API.Helpers.Params;

namespace BusMeal.API.Core.IRepository
{
  public interface IUserRepository
  {

    Task<User> GetOne(int id);
    Task<User> GetOneByUserName(string userName);
    void Add(User user, string password);
    Task<User> Login(string username, string password);
    void Remove(User user);
    Task<IEnumerable<User>> GetAll();
    Task<PagedList<User>> GetPagedUsers(UserParams userParam);
  }
}