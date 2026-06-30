namespace Application.Dtos.Request.Admin
{
    public class CreatePlanAdminRequest
    {
        public string Name { get; set; } = string.Empty;

        public int Max_Users { get; set; }

        public float value { get; set; }

    }
}


