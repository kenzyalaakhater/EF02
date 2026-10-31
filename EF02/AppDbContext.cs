using EF02.Models;
using EF02.Models;
using Microsoft.EntityFrameworkCore;

namespace EF02.Data;

public class AppDbContext : DbContext
{
    public DbSet<Organizer> Organizers { get; set; }
    public DbSet<OrganizerProfile> OrganizerProfiles { get; set; }
    public DbSet<Event> Events { get; set; }
    public DbSet<Attendee> Attendees { get; set; }
    public DbSet<Badge> Badges { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            "Server=(localdb)\\MSSQLLocalDB;Database=EventHubDb;Trusted_Connection=True;TrustServerCertificate=True;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Way 1: Fluent API directly in OnModelCreating (Event)
        modelBuilder.Entity<Event>(e =>
        {
            e.ToTable("Events");
            e.HasKey(x => x.Id);

            e.Property(x => x.Title)
             .IsRequired()
             .HasMaxLength(200);

            e.Property(x => x.Description).IsRequired();
            e.Property(x => x.StartDate).IsRequired();
            e.Property(x => x.EndDate);
            e.Property(x => x.MaxAttendees).IsRequired();

            // One-to-Many: Organizer -> Events
            e.HasOne(x => x.Organizer)
             .WithMany(o => o.Events)
             .HasForeignKey(x => x.OrganizerId)
             .IsRequired()
             .OnDelete(DeleteBehavior.Cascade);
        });

        // Way 2: separate configuration classes
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}