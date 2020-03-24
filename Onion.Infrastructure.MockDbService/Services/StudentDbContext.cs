using Microsoft.EntityFrameworkCore;
using Onion.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Onion.Infrastructure.MockDbService.Services
{
    public class StudentDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=db-mssql;Initial Catalog=s16782;Integrated Security=True");
            base.OnConfiguring(optionsBuilder);
        }
        public DbSet<Student> Student { get; set; }
    }
}
