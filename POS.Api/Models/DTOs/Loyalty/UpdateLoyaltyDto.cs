namespace POS.Api.Models.DTOs.Loyalty
{
    public class UpdateLoyaltyDto
    {
        public Guid Id { get; set; }

        public virtual LoyaltyType Type { get; set; }

        public float Discount { get; set; }
    }
}
