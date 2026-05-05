using Domain.Entity;
using Domain.Entity.UsersChild;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAll();
        Task<User?> GetById(Guid id);
        Task<User> Create(User user);
        Task<bool> Update(Guid id, User user);
        Task<bool> Delete(Guid id);
    }
}
