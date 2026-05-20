using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces
{
    public interface IUserContext
    {
        Guid UserId { get; }
        string Email { get; }
        string Role { get; }
    }   
}

