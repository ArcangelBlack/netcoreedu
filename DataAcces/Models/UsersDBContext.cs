using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DataAccesLayer.Models
{
    public partial class UsersDBContext : DbContext
    {
        public UsersDBContext()
        {
        }

        public UsersDBContext(DbContextOptions<UsersDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<UsersRoles> UsersRoles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=UsersDB;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Roles>(entity =>
            {
                entity.Property(e => e.RoleName)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.Property(e => e.UserPassword).HasMaxLength(50);

                entity.Property(e => e.Username).HasMaxLength(20);
            });

            modelBuilder.Entity<UsersRoles>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.UsersRoles)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK__UsersRole__RoleI__5629CD9C");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UsersRoles)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__UsersRole__UserI__5535A963");
            });
        }
    }
}
