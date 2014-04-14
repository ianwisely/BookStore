using BookStore.Services.Services;

namespace BookStore.Services
{
    public class ServiceFactory
    {
        public static ServiceBase GetInstance(int service)
        {
            switch (service)
            {
                case 1:
                    return UserService.GetInstance();
                case 2:
                    return BookService.GetInstance();
                case 3:
                    return OrderService.GetInstance();
                default:
                    return null;
            }
        } 
    }
}