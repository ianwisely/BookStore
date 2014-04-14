using BookStore.ViewModel.UserModel;

namespace BookStore.Services.Messages.User.Response
{
    public class LoginResponse : ResponseBase
    {
        public LoginResponse()
        {
            LoginViewModel = new LoginViewModel();
        }

        public LoginViewModel LoginViewModel { get; set; }
        public int UserId { get; set; }
    }
}