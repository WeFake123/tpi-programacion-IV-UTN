namespace Application.Dtos.Request
{
    public class CreateClassRequest
    {
        public string Name { get; set; } = string.Empty;

        public int Max_Users { get; set; }

        public List<CreateScheduleRequest> Schedules { get; set; } = new();
    }
}