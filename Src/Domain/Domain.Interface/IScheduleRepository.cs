using Domain.Entity;
namespace Domain.Interface
{
    public interface IScheduleRepository
    {

        Task<List<Schedule>> GetAll();
        Task<Schedule?> GetById(int id);
        Task<Schedule> Create(Schedule schedule);
        Task<bool> Update(int id, Schedule schedule);
        Task<bool> Delete(int id);

    }
}
