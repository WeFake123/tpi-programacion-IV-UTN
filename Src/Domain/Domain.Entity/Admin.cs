using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entity
{
    public class Admin: User
    {
        public string Rol { get; set; } = "Admin";
    }
}
