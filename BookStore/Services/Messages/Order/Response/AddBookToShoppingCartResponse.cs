using BookStore.ViewModel.BookModel;

namespace BookStore.Services.Messages.Order.Response
{
    public class AddBookToShoppingCartResponse : ResponseBase
    {
        public AddBookToShoppingCartResponse()
        {
            CreateViewBookViewModel = new CreateViewBookViewModel();
        }

        public CreateViewBookViewModel CreateViewBookViewModel { get; set; }
        public decimal TotalPrice { get; set; }
    }
}