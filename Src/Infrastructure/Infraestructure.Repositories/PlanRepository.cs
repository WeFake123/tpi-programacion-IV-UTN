using Domain.Entity;
using Domain.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Trabajop4.Infrastructure;

namespace Infrastructure.Repositories
{
    public class PlanRepository : IPlanRepository
    {
        private readonly ApplicationDbContext _context;

        public PlanRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Plan>> GetAll()
        {
            return await _context.Plans.ToListAsync();
        }

        public async Task<Plan?> GetById(Guid id)
        {
            return await _context.Plans.FindAsync(id);
        }
    }
}
