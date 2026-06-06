using Application.Dtos.Request;
using Application.Dtos.Request.Admin;
using Application.Interfaces;
using Domain.Entity;
using Domain.Interface;

namespace Application.Services
{

    public class AdminService : UserService, IAdminService
    {
        private readonly IClassRepository _repo;
        private readonly IScheduleRepository _scheduleRepo;
        public AdminService(IUserRepository repo, IPasswordHasherService hasher, IUserContext userContext, IClassRepository classRepo, IScheduleRepository scheduleRepo)
            : base(repo, hasher, userContext)
        {
            _repo = classRepo;
            _scheduleRepo = scheduleRepo;
        }

        public async Task<Class?> CreteClass(CreateClassAdminRequest request, List<CreteScheduleAdminRequest> scheduleRequests)
        {
            if (request == null) { return null; }
            ;
            if (request.Max_Users < 1) { return null; }
            ;

            var schedules = new List<Schedule>();

            foreach (var scheduleRequest in scheduleRequests)
            {
                var schedule = await CreteSchedule(scheduleRequest);

                if (schedule == null)
                    return null;

                schedules.Add(schedule);
            }

            var clase = new Class
            {
                Name = request.Name,
                Max_Users = request.Max_Users,
                Schedules = schedules
            };

            await _repo.Add(clase);
            await _repo.Save();

            return clase;
        }

        public async Task<Schedule?> CreteSchedule(CreteScheduleAdminRequest request)
        {
            if (request == null) { return null; }
            ;

            if (request.StartTime >= request.EndTime) { return null; }
            var schedule = new Schedule
            {
                DayOfWeek = (Day)request.DayOfWeek,
                StartTime = request.StartTime,
                EndTime = request.EndTime,

            };
            return schedule;
        }

        public async Task<IEnumerable<Class>> GetClass()
        {
            return await _repo.GetAll();
        }





        public async Task<IEnumerable<Class?>> UpdateClass(Guid id, CreateClassAdminRequest request, List<CreteScheduleAdminRequest> scheduleRequests)
        {

            var gymClass = await _repo.GetById(id);

            if (gymClass == null)
                return null;

            gymClass.Name = request.Name ?? gymClass.Name;
            gymClass.Max_Users = request.Max_Users;

            gymClass.Schedules = (List<Schedule>)await UpdateSchedule(id, scheduleRequests);

            await _repo.Save();

            return await _repo.GetAll(); ;
        }

        public async Task<IEnumerable<Schedule?>> UpdateSchedule(Guid id, List<CreteScheduleAdminRequest> scheduleRequests)
        {
            var gymClass = await _repo.GetById(id);

            if (gymClass == null)
                return null;

            var schedules = new List<Schedule>();


            foreach (var schedule in scheduleRequests)
            {
                var pushOrigin = false;

                foreach (var gymClassSchedule in gymClass.Schedules)
                {
                    if (schedule.DayOfWeek == (int)gymClassSchedule.DayOfWeek && schedule.StartTime == gymClassSchedule.StartTime && schedule.EndTime == gymClassSchedule.EndTime)
                    {
                        pushOrigin = true;
                        schedules.Add(gymClassSchedule);
                        break;
                    }
                }
                if (!pushOrigin)
                {
                    schedules.Add(await CreteSchedule(schedule));
                }
            }
            return schedules;
        }
    }
}


