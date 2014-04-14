using BookStore.Models;
using System.Linq;

namespace BookStore.Repository.Repositories
{
    public class UserRepository : RepositoryBase
    {
        private static BookStoreContext context;
        private static UserRepository _userRepository = new UserRepository();

        private UserRepository() {}

        public new static RepositoryBase GetInstance()
        {
            context = new BookStoreContext();
            return _userRepository;
        }

        public void Dispose()
        {
            context.Dispose();
        }

        public bool IsUserAdmin(string email)
        {
            return context.Users.SingleOrDefault(u => u.Email == email).IsAdmin == true;
        }

        public User GetUser(string email)
        {
            return context.Users.SingleOrDefault(u => u.Email == email);
        }

        public User GetUser(int id)
        {
            return context.Users.SingleOrDefault(u => u.Id == id);
        }

        public void InsertUser(User user)
        {
            context.Users.Add(user);
            context.SaveChanges();
        }

        public void UpdateUser(User user)
        {
            context.Users.Attach(user);
            context.Entry(user).State = System.Data.EntityState.Modified;
            context.SaveChanges();
        }
    }
}