namespace Application.Dtos.Responses
{
    public class SingInResponse
    {
        public SingInResponse() { }
        public SingInResponse(Guid id, string email   )
        {
            this.id = id;
            this.email = email;
        }

        public Guid id { get; set; } = Guid.Empty;
        public string email { get; set; } = String.Empty;
    }
}
