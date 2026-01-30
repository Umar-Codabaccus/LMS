using LMS.Api.Domain.Entities;
using LMS.Api.Infrastructure.Context;
using LMS.Api.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LMS.Api.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;
        private readonly DbSet<User> _db;

        public UserRepository(AppDbContext context)
        {
            _context = context;
            _db = context.Set<User>();
        }

        public User Add(User user)
        {
            try
            {
                _db.Add(user);
            }
            catch (Exception e)
            {
                return null;
            }

            _context.SaveChanges();

            return _db.FirstOrDefault(u => u.Email == user.Email);
        }

        public bool Delete(User user)
        {
            try
            {
                _db.Remove(user);
                _context.SaveChanges();
                return true;
            } 
            catch (Exception e)
            {
                return false;
            }
        }

        public User Get(Guid id)
        {
            var user = _db.Find(id);
            return user;
        }

        public List<User> GetAll()
        {
            var users = _db.ToList();
            return users;
        }

        public bool Update(User user)
        {
            try
            {
                _db.Update(user);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Update error: {e.Message}");
                return false;
            }
        }

        public User CheckExistingAccount(string email)
        {
            var user = _db.FirstOrDefault(user => user.Email == email);
            return user;
        }

        public List<User> GetUsersBySearch(string search)
        {
            var users = _db
                        .Where(user => (user.Firstname + " " + user.Lastname).ToLower().Contains(search.ToLower()) 
                                || user.Firstname.ToLower().Contains(search.ToLower()) 
                                || user.Lastname.ToLower().Contains(search.ToLower()))
                        .ToList();

            return users;
        }
    }
}
