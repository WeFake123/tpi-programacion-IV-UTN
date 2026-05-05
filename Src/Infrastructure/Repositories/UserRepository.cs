using Domain.Entity;
using Domain.Interface;
using Microsoft.EntityFrameworkCore;
using Trabajop4.Infrastructure;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;

    public UserRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<User>> GetAll()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task<User?> GetById(Guid id)
    {
        return await _context.Users.FindAsync(id);
    }

    public async Task Add(User user)
    {
        await _context.Users.AddAsync(user);
    }

    public async Task Update(User user)
    {
        _context.Users.Update(user);
    }

    public async Task Delete(User user)
    {
        _context.Users.Remove(user);
    }

    public async Task Save()
    {
        await _context.SaveChangesAsync();
    }
}