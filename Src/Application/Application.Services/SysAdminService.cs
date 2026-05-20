using Application.Interfaces;
using Domain.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services
{
    public class SysAdminService : UserService, ISysAdminService
    {
        public SysAdminService(IUserRepository repo, IPasswordHasherService hasher, IUserContext userContext)
            : base(repo, hasher, userContext)
        {
        }
    }
}




