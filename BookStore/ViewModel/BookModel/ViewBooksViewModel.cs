using BookStore.ViewModel.OrderModel;
using System.Collections.Generic;

namespace BookStore.ViewModel.BookModel
{
    public class ViewBooksViewModel 
    {
        public ViewBooksViewModel()
        {
            CreateViewBookViewModel = new CreateViewBookViewModel();
            CreateViewBookViewModels = new List<CreateViewBookViewModel>();
            ShoppingCartViewModel = new ShoppingCartViewModel();
        }

        public List<CreateViewBookViewModel> CreateViewBookViewModels { get; set; }
        public CreateViewBookViewModel CreateViewBookViewModel { get; set; }
        public ShoppingCartViewModel ShoppingCartViewModel { get; set; }
        
        public int Id { get; set; }
        public string Title { get; set; }
        public string Name { get; set; }
        public string AuthorFirstName { get; set; }
        public string AuthorLastName { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }
        public byte[] CoverImage { get; set; }
        public string ISBN { get; set; }
        public int AmountInStock { get; set; }
        public int UserId { get; set; }
    }
}