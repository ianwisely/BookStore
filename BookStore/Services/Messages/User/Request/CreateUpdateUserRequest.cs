
using BookStore.ViewModel.UserModel;
using System;
namespace BookStore.Services.Messages.User.Request
{
    public class CreateUpdateUserRequest
    {
        public CreateUpdateUserRequest()
        {
            UpdateUserViewModel = new UpdateUserViewModel();
        }

        public UpdateUserViewModel UpdateUserViewModel { get; set; }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public int LoyaltyCardPoints { get; set; }
        public int CreditCardNumber { get; set; }
        public DateTime CCExpiryDate { get; set; }
    }
}