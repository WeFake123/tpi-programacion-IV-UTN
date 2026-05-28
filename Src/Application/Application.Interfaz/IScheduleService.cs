using Domain.Entity;

namespace Application.Services
{
    public interface IScheduleService
    {
        Task<List<Schedule>> GetAll();

        Task<Schedule?> GetById(int id);

        Task<Schedule> Create(Schedule schedule);

        Task<bool> Update(int id, Schedule updatedSchedule);

        Task<bool> Delete(int id);
    }
}