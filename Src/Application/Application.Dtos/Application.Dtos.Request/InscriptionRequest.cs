using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dtos.Requests
{
    public class InscriptionRequest
    {
        public Guid UserId { get; set; }
        public Guid ClassId { get; set; }
    }
}