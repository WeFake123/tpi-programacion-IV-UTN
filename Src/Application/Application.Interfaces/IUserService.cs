using Domain.Entity;
using Application.Dtos.Request;

namespace Application.Interfaces
{
    public interface IUserService
    {
        Task<User?> UpdateUser(UpdateUserRequest request);
        Task<IEnumerable<User>> GetAll();
        Task<User?> GetById(Guid id);
        Task<User?> GetByEmail(string email);
        Task<User> Create(User user);
        Task Update(Guid id, User user);
        Task Delete(Guid id);
    }
}
