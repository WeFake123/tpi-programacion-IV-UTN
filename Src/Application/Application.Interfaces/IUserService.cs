
using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Text;
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
        Task<bool> Update(Guid id, User user);
        Task<bool> Delete(Guid id);
    }
}
