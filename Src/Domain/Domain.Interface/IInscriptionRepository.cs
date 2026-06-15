using System;
using System.Collections.Generic;
using System.Text;
using Domain.Entity;
namespace Domain.Interface
{
    public interface IInscriptionRepository
    {
        Task<IEnumerable<Inscription>> GetAll();
        Task<IEnumerable<Inscription>> GetByUserId(Guid userId);
        Task<IEnumerable<Inscription>> GetByClassId(Guid classId);
        Task<Inscription?> GetByUserAndClass(Guid userId, Guid classId);
        Task Add(Inscription inscription);
        Task<bool> ExistsByClassId(Guid classId);
        Task<int> CountActiveByClassId(Guid classId);
        Task Unsubscribe(Inscription inscription);
        Task Save();
    }
}
