using Application.Dtos.Responses;
using Application.Interfaces;
using Domain.Entity;

namespace Application.Interfaces
{
    public interface IClientService : IUserService
    {
        Task UpdatePlan(Guid planId, Guid userId);
        Task Update(Guid id, User updatedUser);
        //Task<Client?> SubscribeToPlan(SubscribePlanRequest request);

       // Task CheckExpiredSubscriptions();
    }
}