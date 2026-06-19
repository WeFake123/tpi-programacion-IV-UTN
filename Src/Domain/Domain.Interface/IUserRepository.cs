using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Interface


{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAll();
        Task<User?> GetById(Guid id);
        Task<IEnumerable<Client>> GetClientsByPlanId(Guid planId);
        Task<User?> GetByEmail(string email);
        Task Add(User user);
        Task Update(User user);
        Task Delete(User user);
        Task Save();
    }
}
