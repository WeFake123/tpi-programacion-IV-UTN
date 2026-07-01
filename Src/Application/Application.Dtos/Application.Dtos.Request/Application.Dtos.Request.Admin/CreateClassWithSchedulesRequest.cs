namespace Application.Dtos.Request.Admin
{
    public class CreateClassWithSchedulesRequest
    {
        public CreateClassRequest ClassRequest { get; set; }

        public List<CreteScheduleAdminRequest> ScheduleRequests { get; set; }
    }
}
