namespace POS.Api.Models.DTOs.Reservation
{
    public class UpdateReservationDto
    {
        public Guid Id { get; set; }

        public Guid TableId { get; set; }

        public DateTimeOffset ReservedDate { get; set; }
    }
}
