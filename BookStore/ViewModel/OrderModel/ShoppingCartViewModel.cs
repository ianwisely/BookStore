using BookStore.ViewModel.BookModel;
using System.Collections.Generic;

namespace BookStore.ViewModel.OrderModel
{
    public class ShoppingCartViewModel
    {
        public ShoppingCartViewModel()
        {
            Books = new List<CreateViewBookViewModel>();
        }

        public List<CreateViewBookViewModel> Books { get; set; }
    }
}