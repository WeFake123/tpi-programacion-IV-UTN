using Application.Dtos.Responses;
using Application.Dtos.Requests;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces
{
    public interface IAuthService
    {

        Task<AuthResponse?> SingIn(SingInRequest request);
    }
}
