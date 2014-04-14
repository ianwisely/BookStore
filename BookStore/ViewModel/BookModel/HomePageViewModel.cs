using BookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookStore.ViewModel.BookModel
{
    public class HomePageViewModel
    {
        public int UserId { get; set; }
        public List<Book> Books { get; set; }
    }
}