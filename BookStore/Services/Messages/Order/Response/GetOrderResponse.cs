using BookStore.ViewModel.OrderModel;

namespace BookStore.Services.Messages.Order.Response
{
    public class GetOrderResponse : ResponseBase
    {
        public GetOrderResponse()
        {
            PurchaseOrderViewModel = new PurchaseOrderViewModel();
        }

        public PurchaseOrderViewModel PurchaseOrderViewModel { get; set; }
    }
}