using Application.Exceptions;
using Application.Interfaces;
using Domain.Entity;
using Domain.Interface;

namespace Application.Services
{
    public class ClientService : UserService, IClientService
    {
        public ClientService(IUserRepository repo, IPasswordHasherService hasher, IUserContext userContext)
            : base(repo, hasher, userContext)
        {
        }

        public new async Task Update(Guid id, User updatedUser)
        {
            var user = await _repo.GetById(id);

            if (user == null)
                throw new NotFoundException("User not found");

            user.Name = updatedUser.Name ?? user.Name;
            user.Email = updatedUser.Email ?? user.Email;

            if (user is Client client && updatedUser is Client updatedClient)
            {
                client.Id_Plan = updatedClient.Id_Plan ?? client.Id_Plan;
            }

            await _repo.Update(user);
            await _repo.Save();
        }
    }
}