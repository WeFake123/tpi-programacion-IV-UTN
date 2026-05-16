using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entity
{
    public class SysAdmin : User
    {
        public string Rol { get; set; } = "SysAdmin";

    }
}
