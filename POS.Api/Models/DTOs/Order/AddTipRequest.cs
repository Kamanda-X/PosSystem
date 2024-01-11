namespace POS.Api.Models.DTOs.Order
{
    public class AddTipRequest
    {
        public Guid OrderId { get; set; }

        public float Amount { get; set; }
    }
}
