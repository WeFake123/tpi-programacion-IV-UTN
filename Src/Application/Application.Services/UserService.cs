using Application.Interfaces;
using Domain.Entity;
using Domain.Interface;
using Application.Dtos.Requests;
using Application.Dtos.Responses;
using System.ComponentModel.DataAnnotations;
using Application.Dtos.Request;

public class UserService : IUserService
{
    private readonly IUserRepository _repo;
    private readonly IPasswordHasherService _hasher;
    private readonly IUserContext _userContext;

    public UserService(IUserRepository repo, IPasswordHasherService hasher, IUserContext userContext)
    {
        _repo = repo;
        _hasher = hasher;
        _userContext = userContext;
    }

    
    public async Task<User?> UpdateUser(UpdateUserRequest request)
    {
        var user = await _repo.GetById(_userContext.UserId);

        if (user == null) return null;


        user.Name = request.Name ?? user.Name;
        user.Email = request.Email ?? user.Email;
        user.Password = request.Password != null ? _hasher.Hash(request.Password) : user.Password;
        await _repo.Update(user);
        await _repo.Save();
        return user;
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