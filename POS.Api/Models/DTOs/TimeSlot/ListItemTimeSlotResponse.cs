namespace POS.Api.Models.DTOs.TimeSlot
{
    public class ListItemTimeSlotResponse
    {
        public Guid Id { get; set; }

        public TimeOnly Start { get; set; }

        public TimeOnly End { get; set; }
    }
}
