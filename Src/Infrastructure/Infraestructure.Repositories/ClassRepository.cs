using Domain.Entity;
using Domain.Interface;
using Microsoft.EntityFrameworkCore;
using Trabajop4.Infrastructure;

namespace Infrastructure
{
    public class ClassRepository : IClassRepository
    {
        private readonly ApplicationDbContext _context;

        public ClassRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Class>> GetAll()
        {
            return await _context.Classes
                .Include(c => c.Schedules)
                .ToListAsync();
        }

        public async Task<Class?> GetById(Guid id)
        {
            return await _context.Classes
                .Include(c => c.Schedules)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task Add(Class gymClass)
        {
            await _context.Classes.AddAsync(gymClass);
        }

        public async Task Update(Class gymClass)
        {
            _context.Classes.Update(gymClass);
        }

        public async Task Delete(Class gymClass)
        {
            _context.Classes.Remove(gymClass);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}