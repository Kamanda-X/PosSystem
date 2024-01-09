namespace POS.Api.Models.DTOs.Item
{
    public class CreateItemDto
    {
        public string Name { get; set; }

        public string Category { get; set; }

        public float Price { get; set; }
    }
}
