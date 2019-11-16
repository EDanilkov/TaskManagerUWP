using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ServerAPI.Data.Models;
using System.IO;

namespace ServerAPI.Data
{
    public partial class TaskManagerContext : DbContext
    {
        public TaskManagerContext()
        {
            Database.EnsureCreated();
        }

        public TaskManagerContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }

        
        public virtual DbSet<Permission> Permission { get; set; }
        public virtual DbSet<Project> Project { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<Task> Task { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<Status> Status { get; set; }
        public virtual DbSet<Comment> Comment { get; set; }
        public virtual DbSet<UserProject> UserProject { get; set; }
        public virtual DbSet<RolePermission> RolePermission { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Comment>()
            .HasKey(pt => new { pt.Id});

            modelBuilder.Entity<Comment>()
                .HasOne(pt => pt.User);

            modelBuilder.Entity<Comment>()
                .HasOne(pt => pt.Task);

            modelBuilder.Entity<RolePermission>()
                .HasKey(pt => new { pt.RoleId, pt.PermissionId });

            modelBuilder.Entity<RolePermission>()
                .HasOne(pt => pt.Role);

            modelBuilder.Entity<RolePermission>()
                .HasOne(pt => pt.Permission);

            modelBuilder.Entity<UserProject>()
                .HasKey(pt => new { pt.UserId, pt.ProjectId, pt.RoleId });

            modelBuilder.Entity<UserProject>()
                .HasOne(pt => pt.User);

            modelBuilder.Entity<UserProject>()
                .HasOne(pt => pt.Project);
            
            modelBuilder.Entity<Task>()
                .HasOne(pt => pt.Status);

            modelBuilder.Entity<Task>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Login)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Password)
                .IsUnicode(false);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json")
                   .Build();
                var connectionString = configuration.GetConnectionString("DefaultConnection");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }
    }
}