using NorthwindPlatform.Bff.Workstation.Models.Domain.Entities;

namespace NorthwindPlatform.Bff.Workstation.Infrastructure.Repositories
{
    public interface ITrustedDeviceRepository
    {
        Task<TrustedDevice?> GetByDeviceIdAsync(string deviceId);
        Task InsertAsync(TrustedDevice device);
        Task UpdateLastActivityAsync(string deviceId, DateTime now, string? ip);
        Task RevokeAsync(string deviceId);
    }
}