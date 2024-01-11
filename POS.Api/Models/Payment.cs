using System.ComponentModel.DataAnnotations;

namespace POS.Api.Models
{
    public enum PaymentType
    {
        Cash,
        Card
    }

    public class Payment
    {
        public Guid Id { get; init; }

        public PaymentType Type { get; set; }

        public float Total { get; set; }

        public float Sum { get; set; }

        public float AmountPaid { get; set; }

        public float? Change { get; set; }

        // percent
        public float TaxRate { get; set; }

        // percent
        public float Discount { get; set; }

        public DateTimeOffset Date { get; set; }

        [RegularExpression(@"^\d{8}$")]
        public int? CardNumber { get; set; }

        public virtual Order Order { get; set; } = null!;
    }
}
