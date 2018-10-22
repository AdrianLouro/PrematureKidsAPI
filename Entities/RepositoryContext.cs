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

        public DbSet<Child> Children { get; set; }

        public DbSet<ChildParent> ChildrenParents { get; set; }

        public RepositoryContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ChildParent>().HasKey(cp => new {cp.ChildId, cp.ParentId});

            modelBuilder.Entity<ChildParent>()
                .HasOne<Child>(cp => cp.Child)
                .WithMany(c => c.Parents)
                .HasForeignKey(cp => cp.ChildId);

            modelBuilder.Entity<ChildParent>()
                .HasOne<Parent>(cp => cp.Parent)
                .WithMany(p => p.Children)
                .HasForeignKey(cp => cp.ParentId);
        }
    }
}