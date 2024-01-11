using POS.Api.Models.DTOs.Service;

namespace POS.Api.Models.DTOs.TimeSlot
{
    public class TimeSlotResponse
    {
        public Guid Id { get; set; }

        public TimeOnly Start { get; set; }

        public TimeOnly End { get; set; }

        public ListItemServiceResponse Service { get; set; }
    }
}
