using Application.Dtos.Responses;
using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;


namespace Application.Interfaces
{
    public interface IClientService : IUserService
    {
        Task UpdatePlan(Guid planId, Guid userId);

    }
}

