using FastTest.Core.FastTest.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastTest.Infraestructure.Contexts
{
    public class FastTestDbContext :DbContext
    {
        public FastTestDbContext(DbContextOptions<FastTestDbContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(FastTestDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Tarea> Tareas{ get; set; }
    }
}
