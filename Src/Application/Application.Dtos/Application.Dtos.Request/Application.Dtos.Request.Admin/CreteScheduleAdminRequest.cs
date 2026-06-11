using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dtos.Request.Admin
{
 
        public class CreteScheduleAdminRequest
    {
            public int DayOfWeek { get; set; }

            public TimeOnly StartTime { get; set; }

            public TimeOnly EndTime { get; set; }


       }
    
}
