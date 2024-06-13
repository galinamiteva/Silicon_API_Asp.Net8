

using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
namespace Infrastructure.Contexts;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    public DbSet<CourseEntity> Courses { get; set; } = null!;
    public DbSet<SubscriberEntity> Subscribers { get; set; } = null!;
    public DbSet<CategoryEntity> Category { get; set; }
    public DbSet<UserCourseEntities> UserCourses { get; set; }
    public DbSet<ContactEntity> Contacts { get; set; } = null!;
    public DbSet<UserEntity> Users { get; set; } = null!;


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<SubscriberEntity>()
            .HasIndex(e => e.Email)
            .IsUnique();

        modelBuilder.Entity<ContactEntity>()
            .HasKey(e => e.Id);
    }

}
