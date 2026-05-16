using Application.Interfaces;
using Domain.Entity;
using Domain.Interface;
using Application.Dtos.Requests;
using Application.Dtos.Responses;

public class UserService : IUserService
{
    private readonly IUserRepository _repo;

    private readonly IPasswordHasherService _hasher;

    public UserService(IUserRepository repo, IPasswordHasherService hasher)
    {
        _repo = repo;
        _hasher = hasher;
    }




    
    public async Task<User?> GetByEmail(string email)
    {
        var user = await _repo.GetAll();

        return user.FirstOrDefault(u => u.Email == email);
    }

    public async Task<IEnumerable<User>> GetAll()
    {
        return await _repo.GetAll();
    }

    public async Task<User?> GetById(Guid id)
    {
        return await _repo.GetById(id);
    }

    public async Task<User> Create(User user)
    {
        user.Id = Guid.NewGuid();
        user.Password = _hasher.Hash(user.Password);

        await _repo.Add(user);
        await _repo.Save();

        return user;
    }

    public async Task<bool> Update(Guid id, User updatedUser)
    {
        var user = await _repo.GetById(id);
        if (user == null) return false;

        user.Name = updatedUser.Name ?? user.Name;
        user.Email = updatedUser.Email ?? user.Email;   
        await _repo.Update(user);
        await _repo.Save();

        return true;
    }

    public async Task<bool> Delete(Guid id)
    {
        var user = await _repo.GetById(id);
        if (user == null) return false;

        await _repo.Delete(user);
        await _repo.Save();

        return true;
    }
}