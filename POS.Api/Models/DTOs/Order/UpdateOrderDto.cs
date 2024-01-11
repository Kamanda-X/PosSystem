using System.ComponentModel.DataAnnotations.Schema;

namespace POS.Api.Models.DTOs.Order
{
    public class UpdateOrderDto
    {
        public Guid Id { get; init; }

        [ForeignKey("Employee")]
        public string EmployeeId { get; set; } = null!;

        [ForeignKey("Payment")]
        public Guid? PaymentId { get; set; }

        [ForeignKey("Discount")]
        public Guid? DiscountId { get; set; }

        [ForeignKey("Customer")]
        public Guid? CustomerId { get; set; }

        public OrderStatus Status { get; set; }

        public DateTimeOffset Date { get; set; }
    }
}
