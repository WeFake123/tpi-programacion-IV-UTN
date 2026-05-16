using Domain.Entity;
using Domain.Interface;

namespace Application.Services
{
    public class ClassService : IClassService
    {
        private readonly IClassRepository _repo;

        public ClassService(IClassRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<Class>> GetAll()
        {
            return await _repo.GetAll();
        }

        public async Task<Class?> GetById(Guid id)
        {
            return await _repo.GetById(id);
        }

        public async Task<Class> Create(Class gymClass)
        {
            gymClass.Id = Guid.NewGuid();

            await _repo.Add(gymClass);
            await _repo.Save();

            return gymClass;
        }

        public async Task<bool> Update(Guid id, Class updatedClass)
        {
            var gymClass = await _repo.GetById(id);

            if (gymClass == null)
                return false;

            gymClass.Name = updatedClass.Name ?? gymClass.Name;
            gymClass.Max_Users = updatedClass.Max_Users;

            await _repo.Update(gymClass);
            await _repo.Save();

            return true;
        }

        public async Task<bool> Delete(Guid id)
        {
            var gymClass = await _repo.GetById(id);

            if (gymClass == null)
                return false;

            await _repo.Delete(gymClass);
            await _repo.Save();

            return true;
        }
    }
}