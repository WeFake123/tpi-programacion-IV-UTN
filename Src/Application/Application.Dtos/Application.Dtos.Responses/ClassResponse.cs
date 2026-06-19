namespace Application.Dtos.Responses
{
    public class ClassResponse
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public int Max_Users { get; set; }

        public List<ScheduleResponse> Schedules { get; set; } = new();

    
    }
}