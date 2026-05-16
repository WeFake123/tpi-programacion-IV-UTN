using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dtos.Requests
{
    public class SingUpRequest
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int Dni { get; set; }

        public string Password { get; set; } = string.Empty;
    }
}
