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
  public class UserRepository : IUserRepository
  {
    private readonly DataContext context;

    public UserRepository(DataContext context)
    {
      this.context = context;
    }
    public async Task<User> GetOne(int id)
    {
      return await context.User.FindAsync(id);
    }

    public void Add(User user)
    {
      context.User.Add(user);
    }

    public void Remove(User user)
    {
      context.Remove(user);
    }

    public async Task<IEnumerable<User>> GetAll()
    {
      var users = await context.User.ToListAsync();

      return users;
    }

    public async Task<PagedList<User>> GetPagedUsers(UserParams userParams)
    {
      var users = context.User.AsQueryable();

      // filter
      if (!string.IsNullOrEmpty(userParams.FirstName))
      {
        users = users.Where(u => u.FirstName.Contains(userParams.FirstName, StringComparison.OrdinalIgnoreCase));
      }
      if (!string.IsNullOrEmpty(userParams.LastName))
      {
        users = users.Where(u => u.LastName.Contains(userParams.LastName, StringComparison.OrdinalIgnoreCase));
      }

      // sort
      if (userParams.isDescending)
      {
        if (!string.IsNullOrEmpty(userParams.OrderBy))
        {
          switch (userParams.OrderBy.ToLower())
          {
            case "firstname":
              users = users.OrderByDescending(u => u.FirstName);
              break;
            case "lastname":
              users = users.OrderByDescending(u => u.LastName);
              break;
            default:
              users = users.OrderByDescending(u => u.FirstName);
              break;
          }
        }
        else
        {
          users = users.OrderByDescending(u => u.FirstName);
        }
      }
      else
      {
        if (!string.IsNullOrEmpty(userParams.OrderBy))
        {
          switch (userParams.OrderBy.ToLower())
          {
            case "firstname":
              users = users.OrderBy(u => u.FirstName);
              break;
            case "lastname":
              users = users.OrderBy(u => u.LastName);
              break;
            default:
              users = users.OrderBy(u => u.FirstName);
              break;
          }
        }
        else
        {
          users = users.OrderBy(u => u.FirstName);
        }
      }
      return await PagedList<User>.CreateAsync(users, userParams.PageNumber, userParams.PageSize);
    }
  }
}