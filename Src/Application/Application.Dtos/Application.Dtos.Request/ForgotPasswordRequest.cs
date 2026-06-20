using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dtos.Request
{
    public class ForgotPasswordRequest
    {
        public string Email { get; set; } = string.Empty;
    }
}
