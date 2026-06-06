using System;
using System.Collections.Generic;
using System.Text;
using Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Trabajop4.Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Class> Classes { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Plan> Plans { get; set; }
        public DbSet<Inscription> Inscriptions { get; set; }

        // Esta es la clave: el DbSet apunta a la clase base. 
        // EF creará una tabla 'Users' que incluye a Clients, Admins y SysAdmins.
        public DbSet<User> Users { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<SysAdmin> SysAdmins { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Esto le dice a EF que use una sola tabla para toda la jerarquía
            modelBuilder.Entity<User>()
                .HasDiscriminator<string>("UserType")
                .HasValue<Client>("Client")
                .HasValue<Admin>("Admin")
                .HasValue<SysAdmin>("SysAdmin");

            base.OnModelCreating(modelBuilder);
        }
    }
}