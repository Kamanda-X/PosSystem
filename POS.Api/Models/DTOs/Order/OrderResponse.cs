using System.ComponentModel.DataAnnotations.Schema;

namespace POS.Api.Models.DTOs.Order
{
    public class OrderResponse
    {
        public Guid Id { get; init; }

    
        public string EmployeeId { get; set; } = null!;

        public Guid? PaymentId { get; set; }


        public Guid? DiscountId { get; set; }


        public Guid? CustomerId { get; set; }


        public OrderStatus Status { get; set; }

        public DateTimeOffset Date { get; set; }

        public float? Amount { get; set; }

        public float TipAmount { get; set; }

        public Guid? ReservationId { get; set; }

    }
}
