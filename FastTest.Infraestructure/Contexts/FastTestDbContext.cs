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


        public DbSet<TaskItem> TaskItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.ApplyConfigurationsFromAssembly(typeof(FastTestDbContext).Assembly);
            //base.OnModelCreating(modelBuilder);



            // Mapeo a la entidad para tabla Task y definicion de restrincciones
            // campos obligatorios longitudes  y conversión ¿ Status 
            modelBuilder.Entity<TaskItem>(entity =>
            {
                entity.ToTable("TaskItems");
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Title)
                    .IsRequired()
                    .HasMaxLength(200);
                entity.Property(x => x.Description)
                    .HasMaxLength(500);
                entity.Property(x => x.Status)
                    .HasConversion<string>()
                    .HasMaxLength(50);
                entity.Property(x => x.CreatedAt).IsRequired();
                entity.Property(x => x.UpdatedAt).IsRequired();
            });
        }


    }
}
