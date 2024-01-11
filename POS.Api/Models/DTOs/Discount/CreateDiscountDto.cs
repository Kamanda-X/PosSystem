namespace POS.Api.Models.DTOs.Discount
{
    public class CreateDiscountDto
    {
        public float Amount { get; set; }

        public DateTimeOffset StartDate { get; set; }

        public DateTimeOffset EndDate { get; set; }

        public string Description { get; set; } = null!;
    }
}
