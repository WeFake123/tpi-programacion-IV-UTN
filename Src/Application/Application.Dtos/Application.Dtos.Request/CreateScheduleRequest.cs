namespace Application.Dtos.Request
{
    public class CreateScheduleRequest
    {

        public int DayOfWeek { get; set; }

        public TimeOnly StartTime { get; set; }

        public TimeOnly EndTime { get; set; }


    }
}