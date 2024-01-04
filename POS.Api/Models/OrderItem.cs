using System.ComponentModel.DataAnnotations.Schema;
using Azure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace POS.Api.Models
{
    [PrimaryKey(nameof(OrderId), nameof(ItemId))]
    public class OrderItem
    {
        public Guid OrderId { get; set; }

        public virtual Order Order { get; set; } = null!;

        public Guid ItemId { get; set; }

        public virtual Item Item { get; set; } = null!;

        public int Amount { get; set; }
    }
}
