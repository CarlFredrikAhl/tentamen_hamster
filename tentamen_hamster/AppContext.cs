using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Text;

namespace tentamen_hamster
{
    class AppContext : DbContext
    {
        public DbSet<Hamster> Hamsters { get; set; }
        public DbSet<ExerciseSpace> ExerciseSpace { get; set; }
        public DbSet<Cage> Cages { get; set; }
        public DbSet<ActivityLog> ActivityLog { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            if (!builder.IsConfigured) 
            {
                builder.UseSqlServer("Server=LAPTOP-D5UUETOL\\SQLEXPRESS;Database=advCarlFredrikAhl4;Trusted_Connection=True; MultipleActiveResultSets=true;");
                builder.UseLazyLoadingProxies();
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var converter = new ValueConverter<Gender, string>(
                v => v.ToString(),
                v => (Gender)Enum.Parse(typeof(Gender), v));

            modelBuilder
                .Entity<Hamster>()
                .Property(e => e.Gender)
                .HasConversion(converter);
        }
    }
}
