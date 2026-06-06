using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Interface
{
    public interface IPlanRepository
    {
        List<Plan> GetAll();

        Task<Plan?> GetById(Guid id);

        Task Add(Plan plan);

        Task Delete(Plan plan);

        Task Update(Plan Plan);

        Task Save();


    }
}

