using BookStore.Services.Enums;
using BookStore.Services.Interfaces;
using BookStore.Services.SubClasses.Orders;

namespace BookStore.Services.Factories
{
    public class OrderDiscountFactory
    {
        public static IOrderDiscount GetDiscountAndRemainingLoyaltyPoints(int discountType)
        {
            switch (discountType)
            {
                case (int)DiscountType.NoDiscount:
                    return new NoOrderDiscount();
                case (int)DiscountType.FreeBook:
                    return new FreeBookDiscount();
                case (int)DiscountType.PriceDiscount:
                    return new PriceDeductionDiscount();
                default:
                    return new NoOrderDiscount();
            }
        }
    }
}