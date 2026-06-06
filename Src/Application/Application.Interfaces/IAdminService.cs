using Application.Dtos.Request;
using Application.Dtos.Request.Admin;
using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces
{
    public interface IAdminService : IUserService
    {
        Task<Plan?> UpdatePlan(Guid id, CreatePlanAdminRequest request);

        Task<Plan?> CreatePlan(CreatePlanAdminRequest request);

        Task<Class?> CreteClass(CreateClassRequest request, List<CreteScheduleAdminRequest> scheduleRequests);

        Task<Schedule?> CreteSchedule(CreteScheduleAdminRequest request);

        Task<IEnumerable<Class>> GetClass();

        Task<IEnumerable<Class?>> UpdateClass(Guid id, CreateClassRequest request, List<CreteScheduleAdminRequest> scheduleRequests);


        Task<IEnumerable<Schedule?>> UpdateSchedule(Guid id, List<CreteScheduleAdminRequest> scheduleRequests);



    }
}
