using Domain.Entity;
namespace Domain.Interface
{
    public interface IPlanRepository
    {
        Task<IEnumerable<Plan>> GetAll();

        Task<Plan?> GetById(Guid id);

        Task Add(Plan plan);

        Task Delete(Plan plan);

        Task Update(Plan Plan);

        Task Save();


    }
}

