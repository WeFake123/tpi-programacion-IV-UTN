
using Application.Dtos.Request.Admin;
using Application.Dtos.Responses;
using Application.Interfaces;
using Domain.Entity;
using Domain.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services
{
    public class ClientService : UserService, IClientService
    {

        public ClientService(IUserRepository repo, IPasswordHasherService hasher, IUserContext userContext)
            : base(repo, hasher, userContext) 
        {
 
        }

        public async Task UpdatePlan(Guid planId, Guid userId)
        {
            var client = (Client) await _repo.GetById(userId);
            client.Id_Plan = planId;
            client.IsActive = true;
            await _repo.Update(client);
        }

    }
}
 