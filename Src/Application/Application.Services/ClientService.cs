using Application.Interfaz;
using Application.Interfaces;
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
    }
}
 