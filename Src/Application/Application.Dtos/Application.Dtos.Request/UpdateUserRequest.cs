namespace Application.Dtos.Request
{
    public class UpdateUserRequest
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int Dni { get; set; }
        public string Password { get; set; } = string.Empty;
    }
}
