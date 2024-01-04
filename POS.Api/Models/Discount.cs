namespace POS.Api.Models
{
    public enum DiscountStatus
    {
        Unused,
        Used,
        Special
    }

    public class Discount
    {
        public Guid Id { get; set; }

        //percent
        public float Amount { get; set; }

        public DateTimeOffset StartDate { get; set; }

        public DateTimeOffset EndDate { get; set; }

        public string Description { get; set; } = null!;

        public DiscountStatus Status { get; set; }

        public virtual ICollection<Order>? Orders { get; set; }
    }
}
