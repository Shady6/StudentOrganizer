using System;
using Microsoft.EntityFrameworkCore;
using StudentOrganizer.Core.Models;

namespace StudentOrganizer.Infrastructure.Contexts
{
	public class EfCoreDbContext : DbContext
	{
		public EfCoreDbContext(DbContextOptions<EfCoreDbContext> options)
			: base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{			
			modelBuilder.Entity<ScheduledCourse>().Property(e => e.Id).ValueGeneratedNever();
			modelBuilder.Entity<Course>().Property(e => e.Id).ValueGeneratedNever();
			modelBuilder.Entity<Group>().Property(e => e.Id).ValueGeneratedNever();			
			modelBuilder.Entity<Schedule>().Property(e => e.Id).ValueGeneratedNever();
			modelBuilder.Entity<Assignment>().Property(e => e.Id).ValueGeneratedNever();
			modelBuilder.Entity<Team>().Property(e => e.Id).ValueGeneratedNever();

			modelBuilder.Entity<ScheduledCourse>().Property(c => c.StartTime).HasConversion(
				t => new DateTime(2000, 1, 1, t.Hour, t.Minute, 0),
				d => new NodaTime.LocalTime(d.Hour, d.Minute));

			modelBuilder.Entity<ScheduledCourse>().Property(c => c.EndTime).HasConversion(
				t => new DateTime(2000, 1, 1, t.Hour, t.Minute, 0),
				d => new NodaTime.LocalTime(d.Hour, d.Minute));			

			modelBuilder.Entity<Group>()
				.HasMany(g => g.Administrators)
				.WithMany(u => u.AdministratedGroups);

			modelBuilder.Entity<Group>()
				.HasMany(g => g.Moderators)
				.WithMany(u => u.ModeratedGroups);

			modelBuilder.Entity<Group>()
				.HasMany(g => g.Students)
				.WithMany(u => u.Groups);

			modelBuilder.Entity<Group>()
				.HasMany(g => g.Schedules)
				.WithOne()
				.HasForeignKey("GroupId")
				.OnDelete(DeleteBehavior.Cascade);

			modelBuilder.Entity<Team>()
				.HasMany(t => t.Schedules)
				.WithOne()
				.HasForeignKey("TeamId")
				.OnDelete(DeleteBehavior.Cascade);

			modelBuilder.Entity<Team>()
				.HasMany(t => t.Students)
				.WithMany("Teams");			

			modelBuilder.Entity<Course>()
				.OwnsOne(c => c.Location)
				.OwnsOne(l => l.Address);

			base.OnModelCreating(modelBuilder);
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information);
			optionsBuilder.EnableDetailedErrors();
			optionsBuilder.EnableSensitiveDataLogging();
			base.OnConfiguring(optionsBuilder);
		}

		public DbSet<User> Users { get; set; }
		public DbSet<Group> Group { get; set; }
	}
}