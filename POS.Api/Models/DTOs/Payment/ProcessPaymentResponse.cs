namespace POS.Api.Models.DTOs.Payment
{
    public class ProcessPaymentResponse
    {
        public Guid Id { get; set; }

        public float Sum { get; set; }

        public float AmountPaid { get; set; }

        public float? Change { get; set; }

        public float? Discount { get; set; }

        public float TaxRate { get; set; }

        public float Total { get; set; }

        public DateTimeOffset Date { get; set; }
    }
}
