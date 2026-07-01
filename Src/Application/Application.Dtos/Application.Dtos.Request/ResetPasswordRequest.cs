namespace Application.Dtos.Request
{
    public class ResetPasswordRequest
    {
        public string Token { get; set; } = string.Empty;

        public string NewPassword { get; set; } = string.Empty;
    }
}
