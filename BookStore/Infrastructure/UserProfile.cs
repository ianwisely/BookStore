using BookStore.Repository;
using BookStore.Repository.Repositories;

namespace BookStore.Infrastructure
{
    public class UserProfile
    {
        public static bool IsUserAdmin { get; set; }

        public static void GetIsUserAdmin(string email)
        {
            var userRepository = (UserRepository)RepositoryFactory.GetInstance((int)RepositoryTypes.UserRepository);
            IsUserAdmin = userRepository.IsUserAdmin(email);
        }
    }
}