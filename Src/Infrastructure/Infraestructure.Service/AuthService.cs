using Application.Dtos.Requests;
using Application.Dtos.Responses;
using Application.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Trabajop4.Infrastructure;

namespace Infrastructure.Infraestructure.Service
{
    public class AuthService : IAuthService
    {   


        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly IPasswordHasherService _hasher;

        public AuthService(ApplicationDbContext context, IConfiguration configuration, IPasswordHasherService hasher)
        {
            _context = context;
            _configuration = configuration;
            _hasher = hasher;
        }


        public AuthResponse? SignIn(SingInRequest request)
        {


            var cliente = _context.Users.FirstOrDefault(c => c.Email == request.Email);
            if (cliente != null)
            {
                if (_hasher.Verify(cliente.Password, request.Password))
                    return null;
            }
            else return null;

            return new AuthResponse
            {
                Token = GenerarToken(cliente.Id, cliente.Email, cliente.UserType),
                Rol = cliente.UserType,
                Id = cliente.Id,
                Email = cliente.Email
            };
        }
        private string GenerarToken(Guid userId, string email, string rol)
        {
            string key = _configuration["Jwt:Key"]!;
            string issuer = _configuration["Jwt:Issuer"]!;
            string audience = _configuration["Jwt:Audience"]!;
            int expirationMinutes = int.Parse(_configuration["Jwt:ExpirationMinutes"]!);

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, email),
                new Claim(ClaimTypes.Role, rol),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat,
                    DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(),
                    ClaimValueTypes.Integer64)
            };

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(expirationMinutes),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
