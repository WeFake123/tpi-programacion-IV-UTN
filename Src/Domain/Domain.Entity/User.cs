namespace Domain.Entity
{
    public abstract class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int Dni { get; set; }

        public bool IsActive { get; set; }

        public string Password { get; set; } = string.Empty;

        public bool EmailVerified { get; set; } = false;

        public string? VerificationToken { get; set; }
        public DateTime? VerificationTokenExpiration { get; set; }

    }
}
