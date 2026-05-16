using Domain.Entity;

namespace Domain.Interface
{
    public interface IClassRepository
    {
        Task<IEnumerable<Class>> GetAll();

        Task<Class?> GetById(Guid id);

        Task Add(Class gymClass);

        Task Update(Class gymClass);

        Task Delete(Class gymClass);

        Task Save();
    }
}