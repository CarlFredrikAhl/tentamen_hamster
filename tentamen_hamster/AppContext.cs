﻿using Microsoft.EntityFrameworkCore;
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
                builder.UseSqlServer("Server=LAPTOP-D5UUETOL\\SQLEXPRESS;Database=advCarlFredrikAhl;Trusted_Connection=True;");
                builder.UseLazyLoadingProxies();
            }
        }
    }
}