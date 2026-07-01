namespace Application.Dtos.Request
{
    public class SingUpRequest
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int Dni { get; set; }

        public string Password { get; set; } = string.Empty;
    }
}
