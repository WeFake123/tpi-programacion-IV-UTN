using Domain.Entity;
using Domain.Interface;

namespace Application.Services
{
    public class ScheduleService : IScheduleService
    {
        private readonly IScheduleRepository _repository;

        public ScheduleService(IScheduleRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Schedule>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<Schedule?> GetById(int id)
        {
            return await _repository.GetById(id);
        }

        public async Task<Schedule> Create(Schedule schedule)
        {
            return await _repository.Create(schedule);
        }

        public async Task<bool> Update(int id, Schedule updatedSchedule)
        {
            return await _repository.Update(id, updatedSchedule);
        }

        public async Task<bool> Delete(int id)
        {
            return await _repository.Delete(id);
        }
    }
}