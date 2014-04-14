using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookStore.ViewModel.OrderModel
{
    public class MyOrdersViewModel
    {
        public MyOrdersViewModel()
        {
            PurchaseOrderViewModel = new List<PurchaseOrderViewModel>();
        }
        public List<PurchaseOrderViewModel> PurchaseOrderViewModel { get; set; }
    }
}