using Microsoft.EntityFrameworkCore;
using NorthwindPlatform.Bff.Workstation.Models.Domain.Entities;

namespace NorthwindPlatform.Bff.Workstation.Infrastructure.Persistence.Contexts
{
    public class WorkstationDbContext(DbContextOptions<WorkstationDbContext> options) : DbContext(options)
    {
        public DbSet<TrustedDevice> TrustedDevices { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TrustedDevice>(entity =>
            {
                entity.ToTable("trust_devices");
                entity.HasKey(t => t.Id);
                entity.Property(t => t.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(t => t.UserId)
                    .HasColumnName("user_id")
                    .HasColumnType("uuid");
                
                entity.Property(t => t.DeviceId)
                    .HasColumnName("device_id")
                    .HasColumnType("TEXT");

                entity.Property(t => t.DeviceFingerprintHash)
                    .HasColumnName("device_fingerprint_hash")
                    .HasColumnType("bytea");

                entity.Property(t => t.AuthServiceSessionId)
                    .HasColumnName("auth_service_session_id")
                    .HasColumnType("TEXT");

                entity.Property(t => t.FirstSeenAt)
                    .HasColumnName("first_seen_at")
                    .HasColumnType("timestamp with time zone");

                entity.Property(t => t.LastActivityAt)
                    .HasColumnName("last_activity_at")
                    .HasColumnType("timestamp with time zone");

                entity.Property(t => t.ExpiresAt)
                    .HasColumnName("expires_at")
                    .HasColumnType("timestamp with time zone");

                entity.Property(t => t.IsActive)
                    .HasColumnName("is_active");

                entity.Property(t => t.RevokedAt)
                    .HasColumnName("revoked_at");

                entity.Property(t => t.CreatedIp)
                    .HasColumnName("created_ip")
                    .HasColumnType("TEXT");

                entity.Property(t => t.LastIp)
                    .HasColumnName("last_ip")
                    .HasColumnType("TEXT");

                entity.HasIndex(t => new {t.UserId, t.DeviceId}).IsUnique();
                entity.HasIndex(t => t.AuthServiceSessionId);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}