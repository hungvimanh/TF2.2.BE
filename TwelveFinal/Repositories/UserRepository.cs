using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwelveFinal.Entities;
using TwelveFinal.Repositories.Models;

namespace TwelveFinal.Repositories
{
    public interface IUserRepository
    {
        Task<bool> Create(User user);
        Task<bool> BulkInsert(List<User> users);
        Task<User> Get(UserFilter userFilter);
        Task<bool> Delete(Guid Id);
        Task<bool> ChangePassword(User user);
    }
    public class UserRepository : IUserRepository
    {
        private readonly TFContext tFContext;
        public UserRepository(TFContext _tFContext)
        {
            tFContext = _tFContext;
        }

        private IQueryable<UserDAO> DynamicFilter(IQueryable<UserDAO> query, UserFilter userFilter)
        {
            if (!string.IsNullOrEmpty(userFilter.Username))
            {
                query = query.Where(u => u.Username.Equals(userFilter.Username));
            }
            if (userFilter.IsAdmin.HasValue)
            {
                query = query.Where(u => u.IsAdmin.Equals(userFilter.IsAdmin.Value));
            }
            return query;
        }

        public async Task<bool> Create(User user)
        {
            UserDAO userDAO = new UserDAO
            {
                Id = user.Id,
                IsAdmin = user.IsAdmin,
                Username = user.Username,
                Password = user.Password,
                StudentId = user.StudentId,
                Email = user.Email
            };

            tFContext.User.Add(userDAO);
            await tFContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> BulkInsert(List<User> users)
        {
            try
            {
                List<UserDAO> userDAOs = users.Select(user => new UserDAO()
                {
                    Id = user.Id,
                    IsAdmin = user.IsAdmin,
                    Username = user.Username,
                    Password = user.Password,
                    StudentId = user.StudentId,
                    Email = user.Email
                }).ToList();

                tFContext.User.AddRange(userDAOs);
                await tFContext.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<bool> Delete(Guid Id)
        {
            await tFContext.User.Where(g => g.Id.Equals(Id)).DeleteFromQueryAsync();
            return true;
        }

        public async Task<User> Get(UserFilter userFilter)
        {
            IQueryable<UserDAO> users = tFContext.User;
            UserDAO userDAO = await DynamicFilter(users, userFilter).FirstOrDefaultAsync();
            User user = null;
            if (userDAO == null) return user;
            else
            {
                user = new User()
                {
                    Id = userDAO.Id,
                    Username = userDAO.Username,
                    Password = userDAO.Password,
                    Salt = userDAO.Salt,
                    IsAdmin = userDAO.IsAdmin,
                    StudentId = userDAO.StudentId,
                    Email = userDAO.Email
                };
            }

            return user;
        }

        public async Task<bool> ChangePassword(User user)
        {
            await tFContext.User.Where(u => u.Id.Equals(user.Id)).UpdateFromQueryAsync(u => new UserDAO
            {
                Password = user.Password,
                Salt = user.Salt
            });

            return true;
        }
    }
}
