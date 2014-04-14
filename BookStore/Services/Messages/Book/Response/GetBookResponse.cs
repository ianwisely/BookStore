using BookStore.ViewModel.BookModel;

namespace BookStore.Services.Messages.Book.Response
{
    public class GetBookResponse : ResponseBase
    {
        public GetBookResponse()
        {
            CreateViewBookViewModel = new CreateViewBookViewModel();
        }
        public CreateViewBookViewModel CreateViewBookViewModel { get; set; }
    }
}