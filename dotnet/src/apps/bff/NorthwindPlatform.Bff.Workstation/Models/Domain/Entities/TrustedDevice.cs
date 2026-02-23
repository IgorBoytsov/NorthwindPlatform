using System.ComponentModel.DataAnnotations;

namespace NorthwindPlatform.Bff.Workstation.Models.Domain.Entities
{
    public sealed class TrustedDevice
    {
        [Key]
        public Guid Id { get; set; } = new Guid();

        [Required]
        public Guid UserId { get; set; }

        [Required]
        [StringLength(255)]
        public string DeviceId { get; set; } = string.Empty;

        [Required]
        public byte[] DeviceFingerprintHash { get; set; } = [];

        [Required]
        [StringLength(255)]
        public string AuthServiceSessionId { get; set; } = string.Empty;

        public DateTime FirstSeenAt { get; set; } = DateTime.UtcNow;
        public DateTime LastActivityAt {get; set; } = DateTime.UtcNow;
        public DateTime ExpiresAt { get; set; }

        public bool IsActive { get; set; } = true;
        public DateTime? RevokedAt { get; set; }

        public string? CreatedIp { get; set; }
        public string? LastIp {get; set; }
    }
}