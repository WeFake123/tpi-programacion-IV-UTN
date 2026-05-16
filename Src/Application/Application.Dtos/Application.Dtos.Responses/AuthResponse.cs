using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dtos.Responses
{
    public class AuthResponse
    {
        public string Token { get; set; } = string.Empty;
        public string Rol { get; set; } = string.Empty;

        public Guid Id { get; set; } = Guid.Empty;
        public string Email  { get; set; } = string.Empty;
    }
}
