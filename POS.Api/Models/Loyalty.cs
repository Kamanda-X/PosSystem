namespace POS.Api.Models
{
    public enum LoyaltyType
    {
        Standard,
        Plus,
        Premium
    }

    public class Loyalty
    {
        public Guid Id { get; set; }

        public virtual LoyaltyType Type { get; set; }

        public float Discount { get; set; }

        public virtual ICollection<Customer>? Customers { get; set; }
    }
}
