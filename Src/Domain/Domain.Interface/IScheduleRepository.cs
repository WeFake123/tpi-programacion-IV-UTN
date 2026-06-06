using Domain.Entity;
namespace Domain.Interface
{
    public interface IScheduleRepository
    {

        Task<List<Schedule>> GetAll();
        Task<Schedule?> GetById(Guid id);
        Task<Schedule> Create(Schedule schedule);
        Task<bool> Update(Guid id, Schedule schedule);
        Task<bool> Delete(Guid id);

        Task Save();


    }
}
