using BookStore.ViewModel.BookModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookStore.Services.Messages.Book.Response
{
    public class HomePageResponse : ResponseBase
    {
        public HomePageResponse()
        {
            HomePageViewModel = new HomePageViewModel();
        }

        public HomePageViewModel HomePageViewModel { get; set; }
    }
}