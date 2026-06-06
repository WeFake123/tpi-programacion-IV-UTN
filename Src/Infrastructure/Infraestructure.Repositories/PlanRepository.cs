using Domain.Entity;
using Domain.Interface;
using Microsoft.EntityFrameworkCore;
using Trabajop4.Infrastructure;

namespace Infrastructure.Repository
{
    public class PlanRepository : IPlanRepository
    {
        private readonly ApplicationDbContext _context;

        public PlanRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Plan> GetAll()
        {
            return _context.Plans.ToList();
        }

        public async Task<Plan?> GetById(Guid id)
        {
            return await _context.Plans
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task Add(Plan plan)
        {
            await _context.Plans.AddAsync(plan);
        }

        public async Task Delete(Plan plan)
        {
            _context.Plans.Remove(plan);

            await Task.CompletedTask;
        }

        public Task Update(Plan plan)
        {
            _context.Plans.Update(plan);

            return Task.CompletedTask;
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}