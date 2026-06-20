using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dtos.Request
{ 
    public class SingInRequest
    {
        public string Email { get; set; } = String.Empty;
        public string Password { get; set; } = String.Empty;
        
    }
}
