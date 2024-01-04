using POS.Api.Models;

namespace POS.Api.Data
{
    public class DbInitializer
    {
        public static void Initialize(PosDbContext context)
        {
            InitializeLoyalty(context);
            context.SaveChanges();
        }

        public static void InitializeLoyalty(PosDbContext context)
        {
            if (context.Loyalty.Any())
            {
                return;
            }

            var users = new Loyalty[]
            {
                new() { Type = LoyaltyType.Standard, Discount = 0 },
                new() { Type = LoyaltyType.Plus, Discount = 0.2f },
                new() { Type = LoyaltyType.Premium, Discount = 0.4f }
            };

            context.Loyalty.AddRange(users);
        }
    }
}
