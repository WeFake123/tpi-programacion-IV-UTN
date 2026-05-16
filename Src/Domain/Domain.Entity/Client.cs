using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entity
{
    public class Client : User
    {

        public string Rol { get; set; } = "Client";

        public int Id_Plan { get; set; }
    }
}
