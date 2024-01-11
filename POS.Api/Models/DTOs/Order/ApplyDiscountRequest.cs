namespace POS.Api.Models.DTOs.Order
{
    public class ApplyDiscountRequest
    {
        public Guid OrderId { get; set; }

        public Guid DiscountId { get; set; }
    }
}
