using Domain.Common;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class BloggerContext : DbContext
    {
        public BloggerContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Post> Posts { get; set; }

        public override int SaveChanges()
        {
            var entities = ChangeTracker
                .Entries()
                .Where(e => e.Entity is AuditableEntity && (e.State == EntityState.Added || e.State == EntityState.Modified));
            foreach (var entity in entities)
            {
                ((AuditableEntity)entity.Entity).LastModified = DateTime.UtcNow;
                ((AuditableEntity)entity.Entity).LastModifiedBy = "Default";

                if (entity.State == EntityState.Added)
                {
                    ((AuditableEntity)entity.Entity).Created = DateTime.UtcNow;
                    ((AuditableEntity)entity.Entity).CreatedBy = "Default";
                }
            }
            return base.SaveChanges();
        }
    }
}
