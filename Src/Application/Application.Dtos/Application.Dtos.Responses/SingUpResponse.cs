namespace Application.Dtos.Responses
{
    public class SingUpResponse
    {
        public SingUpResponse() { }
        public SingUpResponse(Guid id, string email)
        {
            this.id = id;
            this.email = email;
        }

        public Guid id { get; set; } = Guid.Empty;
        public string email { get; set; } = String.Empty;
    }
}
