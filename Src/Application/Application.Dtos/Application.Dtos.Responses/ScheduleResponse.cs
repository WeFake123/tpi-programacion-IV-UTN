namespace Application.Dtos.Responses
{
    public class ScheduleResponse
    {
        public int Id { get; set; }

        public int DayOfWeek { get; set; }

        public TimeOnly StartTime { get; set; }

        public TimeOnly EndTime { get; set; }

        public bool IsActive { get; set; }
    }
}