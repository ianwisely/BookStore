using BookStore.ViewModel.OrderModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookStore.Services.Messages.Order.Response
{
    public class CancelOrderResponse : ResponseBase
    {
        public CancelOrderResponse()
        {
            MyOrdersViewModel = new MyOrdersViewModel();
        }
        public MyOrdersViewModel MyOrdersViewModel { get; set; }
    }
}