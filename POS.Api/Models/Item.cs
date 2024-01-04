namespace POS.Api.Models
{
    public class Item
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public string Category { get; set; } = null!;

        public float Price { get; set; }

        public virtual ICollection<OrderItem>? OrderItems { get; set; }
    }
}
