using System;
using System.Collections.Generic;
using System.Text;
using Application.Interfaces;
using Microsoft.AspNetCore.Identity;

using BCrypt.Net;


namespace Infrastructure.Service
            
{
    public class PasswordHasherService : IPasswordHasherService
    {
        public string Hash(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public bool Verify(string password, string hash)
        {
            return BCrypt.Net.BCrypt.Verify(password, hash);
        }
    }

}
