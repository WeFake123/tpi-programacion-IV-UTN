using Domain.Entity;

namespace Application.Interfaces
{
    public interface IClientService : IUserService
    {
        Task Update(Guid id, User updatedUser);
    }
}

