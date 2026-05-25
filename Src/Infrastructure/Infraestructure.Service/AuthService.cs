using Application.Dtos.Requests;
using Application.Dtos.Responses;
using Application.Interfaces;
using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using Trabajop4.Infrastructure;

namespace Infraestructure.Service
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

        //Agregar validaciones de registro
        public async Task<AuthResponse?> SingUp(SingUpRequest request)
        {

            string patron = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            if (!Regex.IsMatch(request.Email, patron))
            { return null; }
            ;
            var existingUser = await _context.Users
                .FirstOrDefaultAsync(c => c.Email == request.Email);
            if (existingUser != null)
                return null;


            var hashedPassword = _hasher.Hash(request.Password);
            var newUser = new Client
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Email = request.Email,
                Password = hashedPassword,
                Dni = request.Dni
            };
            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();
            return new AuthResponse
            {
                Token = GenerarToken(
                    newUser.Id,
                    newUser.Email,
                    newUser.GetType().Name),
                Rol = newUser.GetType().Name,
                Id = newUser.Id,
                Email = newUser.Email
            };
        }

        public async Task<AuthResponse?> SingIn(SingInRequest request)
        {
            var cliente = await _context.Users
                .FirstOrDefaultAsync(c => c.Email == request.Email);
            Console.WriteLine(cliente);

            if (cliente == null)
                return null;

            if (!_hasher.Verify(request.Password, cliente.Password))
                return null;

            return new AuthResponse
            {
                Token = GenerarToken(
                    cliente.Id,
                    cliente.Email,
                    cliente.GetType().Name),

                Rol = cliente.GetType().Name,
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
                new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                new Claim(ClaimTypes.Email, email),
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
