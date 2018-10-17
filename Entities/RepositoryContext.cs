using System;
using System.Collections.Generic;
using System.Text;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Entities
{
    public class RepositoryContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Parent> Parents { get; set; }

        public DbSet<Doctor> Doctors { get; set; }

        public RepositoryContext(DbContextOptions options) : base(options)
        {
        }
    }
}