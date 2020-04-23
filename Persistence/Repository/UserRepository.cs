using System.Collections.Immutable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusMeal.API.Core.IRepository;
using BusMeal.API.Core.Models;
using BusMeal.API.Helpers;
using BusMeal.API.Helpers.Params;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.DirectoryServices.ActiveDirectory;

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
            var user = await context.User
                        .Include(u => u.UserDepartments)
                        .Include(u => u.UserModuleRights)
                          .ThenInclude(u => u.ModuleRights)
                        .FirstOrDefaultAsync(u => u.Id == id);
            return user;
        }

        public async Task<User> GetOneByUserName(string username)
        {
            var user = await context.User.FirstOrDefaultAsync(u => u.Username == username);
            return user;
        }

        public async Task<User> Login(string username, string password)
        {
            var user = await context.User.FirstOrDefaultAsync(u => u.Username == username);

            if (user != null)
                if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                    user = null; // set user to null if password not verify.

            return user;
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i]) return false;
                }
                return true;
            }
        }

        public void Add(User user, string password)
        {
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            context.User.Add(user);
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public void Remove(User user)
        {
            context.Remove(user);
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            var users = await context.User
                        .Include(u => u.UserDepartments)
                        .Include(u => u.UserModuleRights)
                          .ThenInclude(u => u.ModuleRights)
                        .ToListAsync();

            return users;
        }

        public async Task<PagedList<User>> GetPagedUsers(UserParams userParams)
        {
            var users = context.User
                        .Include(u => u.UserDepartments)
                        .Include(u => u.UserModuleRights)
                          .ThenInclude(u => u.ModuleRights)
                        .AsQueryable();

            // filter
            if (!string.IsNullOrEmpty(userParams.GddbId))
            {
                users = users.Where(u => u.GddbId.Contains(userParams.GddbId, StringComparison.OrdinalIgnoreCase));
            }
            if (!string.IsNullOrEmpty(userParams.FirstName))
            {
                users = users.Where(u => u.FirstName.Contains(userParams.FirstName, StringComparison.OrdinalIgnoreCase));
            }
            if (!string.IsNullOrEmpty(userParams.LastName))
            {
                users = users.Where(u => u.LastName.Contains(userParams.LastName, StringComparison.OrdinalIgnoreCase));
            }
            if (!string.IsNullOrEmpty(userParams.FullName))
            {
                users = users.Where(u => u.FullName.Contains(userParams.FullName, StringComparison.OrdinalIgnoreCase));
            }
            if (userParams.isActive == true)
            {
                users = users.Where(u => u.isActive == true);
            }

            // sort
            if (userParams.isDescending)
            {
                if (!string.IsNullOrEmpty(userParams.OrderBy))
                {
                    switch (userParams.OrderBy.ToLower())
                    {
                        case "gddbid":
                            users = users.OrderByDescending(u => u.GddbId);
                            break;
                        case "firstname":
                            users = users.OrderByDescending(u => u.FirstName);
                            break;
                        case "lastname":
                            users = users.OrderByDescending(u => u.LastName);
                            break;
                        case "fullname":
                            users = users.OrderByDescending(u => u.FullName);
                            break;
                        default:
                            users = users.OrderByDescending(u => u.GddbId);
                            break;
                    }
                }
                else
                {
                    users = users.OrderByDescending(u => u.GddbId);
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(userParams.OrderBy))
                {
                    switch (userParams.OrderBy.ToLower())
                    {
                        case "gddbid":
                            users = users.OrderBy(u => u.GddbId);
                            break;
                        case "firstname":
                            users = users.OrderBy(u => u.FirstName);
                            break;
                        case "lastname":
                            users = users.OrderBy(u => u.LastName);
                            break;
                        case "fullname":
                            users = users.OrderBy(u => u.FullName);
                            break;
                        default:
                            users = users.OrderBy(u => u.GddbId);
                            break;
                    }
                }
                else
                {
                    users = users.OrderBy(u => u.GddbId);
                }
            }
            return await PagedList<User>.CreateAsync(users, userParams.PageNumber, userParams.PageSize);
        }
    }
}