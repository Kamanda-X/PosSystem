using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Azure;

namespace POS.Api.Models
{
    public enum OrderStatus
    {
        Ongoing,
        Paid,
        Finished,
        Cancelled
    }

    public class Order
    {
        public Order()
        {
            OrderItems = new List<OrderItem>();
        }


        public Guid Id { get; init; }

        [ForeignKey("Employee")]
        public string EmployeeId { get; set; } = null!;
        public virtual Employee Employee { get; set; } = null!;

        [ForeignKey("Payment")]
        public Guid? PaymentId { get; set; }

        public virtual Payment? Payment { get; set; }

        [ForeignKey("Discount")]
        public Guid? DiscountId { get; set; }

        public virtual Discount? Discount { get; set; }

        [ForeignKey("Customer")]
        public Guid? CustomerId { get; set; }

        public virtual Customer? Customer { get; set; }

        public OrderStatus Status { get; set; }

        public DateTimeOffset Date { get; set; }

        public virtual ICollection<OrderItem> OrderItems { get; set; }

        public float? Amount { get; set; }

        public float TipAmount { get; set; }

        public Guid? ReservationId { get; set; }

        public virtual Reservation? Reservation { get; set; }
    }
}
