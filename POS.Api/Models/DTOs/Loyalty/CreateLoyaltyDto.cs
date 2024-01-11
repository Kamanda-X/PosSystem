namespace POS.Api.Models.DTOs.Loyalty
{
    public class CreateLoyaltyDto
    {
        public virtual LoyaltyType Type { get; set; }

        public float Discount { get; set; }
    }
}
