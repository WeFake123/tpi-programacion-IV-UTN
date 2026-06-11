using Application.Dtos.Request.Admin;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dtos.Request.Admin
{
    public class CreateClassWithSchedulesRequest
    {
        public CreateClassRequest ClassRequest { get; set; }

        public List<CreteScheduleAdminRequest> ScheduleRequests { get; set; }
    }
}
