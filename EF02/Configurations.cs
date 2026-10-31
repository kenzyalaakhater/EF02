using EF02.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EF02.Configurations;

public class OrganizerProfileConfiguration : IEntityTypeConfiguration<OrganizerProfile>
{
    public void Configure(EntityTypeBuilder<OrganizerProfile> builder)
    {
        builder.ToTable("OrganizerProfiles");
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Biography).HasMaxLength(1000);
        builder.Property(p => p.WebsiteUrl).HasMaxLength(300);
        builder.Property(p => p.LogoUrl).HasMaxLength(300);

        // One-to-Onee
        builder.HasOne(p => p.Organizer)
               .WithOne(o => o.Profile)
               .HasForeignKey<OrganizerProfile>(p => p.OrganizerId)
               .IsRequired()
               .OnDelete(DeleteBehavior.Cascade);
    }
}
public class BadgeConfiguration : IEntityTypeConfiguration<Badge>
{
    public void Configure(EntityTypeBuilder<Badge> builder)
    {
        builder.ToTable("Badges");
        builder.HasKey(b => b.Id);

        builder.Property(b => b.BadgeNumber)
               .IsRequired()
               .HasMaxLength(50);

        builder.HasIndex(b => b.BadgeNumber).IsUnique();

        builder.Property(b => b.IssuedDate).IsRequired();

        builder.Property(b => b.Tier)
               .IsRequired()
               .HasConversion<string>()
               .HasMaxLength(20);

        // One-to-One
        builder.HasOne(b => b.Attendee)
               .WithOne(a => a.Badge)
               .HasForeignKey<Badge>(b => b.AttendeeId)
               .IsRequired()
               .OnDelete(DeleteBehavior.Cascade);
    }
}