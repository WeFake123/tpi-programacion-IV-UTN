using Application.Dtos.Responses;
using Application.Dtos.Requests;
using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAll();
        Task<User?> GetById(Guid id);
        Task<User?> GetByEmail(string email);
        Task<User> Create(User user);
        Task<bool> Update(Guid id, User user);
        Task<bool> Delete(Guid id);
    }
}
