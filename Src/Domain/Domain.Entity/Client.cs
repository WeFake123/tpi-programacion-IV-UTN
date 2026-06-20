using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entity
{
    public class Client : User
    {
        public Guid? Id_Plan { get; set; }

    }
}
