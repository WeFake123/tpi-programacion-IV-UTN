using Application.Dtos.Request;
using Application.Dtos.Request.Admin;
using Application.Exceptions;
using Application.Interfaces;
using Domain.Entity;
using Domain.Interface;

namespace Application.Services
{

    public class AdminService : UserService, IAdminService
    {
        private readonly IPlanRepository _planRepo;
        private readonly IClassRepository _repo;
        private readonly IScheduleRepository _scheduleRepo;
        private readonly IInscriptionRepository _inscriptionRepo;
        public AdminService(IUserRepository repo, IPasswordHasherService hasher, IUserContext userContext, IClassRepository classRepo, IScheduleRepository scheduleRepo, IPlanRepository planRepo)
            : base(repo, hasher, userContext)
        {
            _repo = classRepo;
            _scheduleRepo = scheduleRepo;
            _planRepo = planRepo;   
        }



        // -------------------------CRUD for Plan---------------------

        public async Task<Plan?> UpdatePlan(Guid id, CreatePlanAdminRequest request)
        {
            var plan_id = await _planRepo.GetById(id);
            var plan = await _planRepo.GetById(id);

            if (plan == null)
                throw new NotFoundException("Plan not found");

            if (string.IsNullOrWhiteSpace(request.Name))
                throw new ValidationException("Plan name is required");

            if (request.Max_Users <= 0)
                throw new ValidationException("Max users must be greater than zero");

            if (request.value <= 0)
                throw new ValidationException("Plan value must be greater than zero");

            plan_id.Name = request.Name;
            plan_id.Max_Class = request.Max_Users;
            plan_id.Value = request.value;

            await _planRepo.Update(plan_id);
            await _planRepo.Save();

            return plan_id;

        }

        public async Task<Plan?> CreatePlan(CreatePlanAdminRequest request)
        {
            if (request == null)
                throw new BadRequestException("Request cannot be null");

            if (string.IsNullOrWhiteSpace(request.Name))
                throw new ValidationException("Plan name is required");

            if (request.Max_Users <= 0)
                throw new ValidationException("Max users must be greater than zero");

            if (request.value <= 0)
                throw new ValidationException("Plan value must be greater than zero");

            var plan = new Plan
            {
                Name = request.Name,
                Max_Class = request.Max_Users,
                Value = request.value
            };


            await _planRepo.Add(plan);
            await _planRepo.Save();

            return plan;
        }

        public async Task<Plan?> DeletePlan(Guid id)
        {

            var plan = await _planRepo.GetById(id);
            if (plan == null)
                throw new NotFoundException("Plan not found");
            throw new ConflictException(
    "Plan cannot be deleted because it is currently assigned to users");
            await _planRepo.Delete(plan);
            await _planRepo.Save();
            return plan;
        }

        public async Task<IEnumerable<Plan>> GetPlan()
        {
            return await _planRepo.GetAll();
        }



        // ---------------CRUD for Class -------------------

        public async Task<IEnumerable<Class?>> UpdateClass(Guid id, CreateClassRequest request, List<CreteScheduleAdminRequest> scheduleRequests)
        {

            var gymClass = await _repo.GetById(id);

            if (gymClass == null)
                return null;

            gymClass.Name = request.Name ?? gymClass.Name;
            if (request.Max_Users != 0) gymClass.Max_Users = request.Max_Users;

            if (scheduleRequests != null)
            {
                gymClass.Schedules = (List<Schedule>)await UpdateSchedule(id, scheduleRequests);

            }

            await _repo.Save();

            return await _repo.GetAll(); ;
        }

        public async Task<IEnumerable<Class?>> DeleteClass(Guid id)
        {
            var gymClass = await _repo.GetById(id);
            if (gymClass == null) return null;
            await _repo.Delete(gymClass);
            await _repo.Save();
            return await _repo.GetAll();
        }

        public async Task<Class?> CreteClass(CreateClassRequest request, List<CreteScheduleAdminRequest> scheduleRequests)
        {
            if (request == null)
                throw new BadRequestException("Request cannot be null");

            if (string.IsNullOrWhiteSpace(request.Name))
                throw new ValidationException("Class name is required");

            if (request.Max_Users <= 0)
                throw new ValidationException("Max users must be greater than zero");

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

        public async Task<IEnumerable<Class>> GetClass()
        {
            return await _repo.GetAll();
        }


        // -------------------------CRUD for Schedule---------------------

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

        public async Task<Schedule?> DeleteSchedule(Guid id)
        {
            var schedule = await _scheduleRepo.GetById(id);
            if (schedule == null) return null;
            await _scheduleRepo.Delete(id);
            await _scheduleRepo.Save();
            return schedule;
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