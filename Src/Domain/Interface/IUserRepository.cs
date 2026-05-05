using Domain.Entity;
using Domain.Entity.UsersChild;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Interface


{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAll();
        Task<User?> GetById(Guid id);
        Task Add(User user);
        Task Update(User user);
        Task Delete(User user);
        Task Save();
    }
}
