using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Interface
{
    public interface IPlanRepository
    {
        Task<IEnumerable<Plan>> GetAll();
        Task<Plan?> GetById(Guid id);
    }
}
