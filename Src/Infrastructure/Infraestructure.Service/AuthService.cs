
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
        private readonly IEmailService _emailService;

        public AuthService(ApplicationDbContext context, IConfiguration configuration, IPasswordHasherService hasher, IEmailService emailService)
        {
            _context = context;
            _configuration = configuration;
            _hasher = hasher;
            _emailService = emailService;
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
            var verificationToken = Guid.NewGuid().ToString();

            var newUser = new Client
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Email = request.Email,
                Password = hashedPassword,
                Dni = request.Dni,
                EmailVerified = false,
                VerificationToken = verificationToken
            };
            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();
            var verificationLink = $"https://localhost:7001/api/clients/verify-email?token={verificationToken}";
            await _emailService.SendEmailAsync(
                newUser.Email,
                "Verifica tu cuenta",
                $@"
                <h2>Bienvenido al gimnasio</h2>
                <p>Hace click en el siguiente enlace para verificar tu cuenta:</p>
                <a href='{verificationLink}'> Verificar cuenta</a>"
                );

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

        public async Task<bool> VerifyEmail(string token)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.VerificationToken == token);

            if (user == null)
                return false;

            user.EmailVerified = true;
            user.VerificationToken = null;

            await _context.SaveChangesAsync();

            return true;
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
