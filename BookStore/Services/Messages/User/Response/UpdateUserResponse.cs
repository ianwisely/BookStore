using BookStore.ViewModel.UserModel;

namespace BookStore.Services.Messages.User.Response
{
    public class UpdateUserResponse : ResponseBase
    {
        public UpdateUserResponse()
        {
            UpdateUserViewModel = new UpdateUserViewModel();
        }

        public UpdateUserViewModel UpdateUserViewModel { get; set; }
    }
}