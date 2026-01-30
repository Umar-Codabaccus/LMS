using LMS.Api.Domain.Entities;

namespace LMS.Api.Infrastructure.Interfaces
{
    public interface IUserRepository
    {
        public User Add(User user);
        public User CheckExistingAccount(string email);
        public bool Delete(User user);
        public User Get(Guid id);
        public List<User> GetAll();
        public bool Update(User user);
        public List<User> GetUsersBySearch(string search);
    }
}
