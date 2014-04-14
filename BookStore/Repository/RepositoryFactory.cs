using BookStore.Repository.Repositories;

namespace BookStore.Repository
{
    public class RepositoryFactory
    {
        public static RepositoryBase GetInstance(int service)
        {
            switch (service)
            {
                case (int)RepositoryTypes.UserRepository:
                    return UserRepository.GetInstance();
                case (int)RepositoryTypes.BookRepository:
                    return BookRepository.GetInstance();
                default:
                    return null;
            }
        } 
    }
}