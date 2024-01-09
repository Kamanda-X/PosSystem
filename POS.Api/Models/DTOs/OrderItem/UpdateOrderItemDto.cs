namespace POS.Api.Models.DTOs.OrderItem
{
    public class UpdateOrderItemDto
    {
        public Guid OrderId { get; set; }

        public Guid ItemId { get; set; }

        public int Amount { get; set; }
    }
}
