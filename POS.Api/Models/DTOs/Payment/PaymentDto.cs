using System.ComponentModel.DataAnnotations;

namespace POS.Api.Models.DTOs.Payment
{
    public class PaymentDto
    {
        public Guid Id { get; set; }

        public Guid OrderId { get; init; }

        public PaymentType Type { get; set; }

        public float Amount { get; set; }

        public float TaxRate { get; set; }

        public float TipAmount { get; set; }

        public DateTimeOffset Date { get; set; }

        [RegularExpression(@"^\d{8}$")]
        public int? CardNumber { get; set; }
    }
}
