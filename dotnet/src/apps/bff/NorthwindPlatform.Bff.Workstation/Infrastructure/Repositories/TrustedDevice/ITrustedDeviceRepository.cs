using TrustedDeviceModel = NorthwindPlatform.Bff.Workstation.Models.Domain.Entities.TrustedDevice;

namespace NorthwindPlatform.Bff.Workstation.Infrastructure.Repositories.TrustedDevice
{
    public interface ITrustedDeviceRepository
    {
        Task<TrustedDeviceModel?> GetByDeviceIdAsync(string deviceId);
        Task InsertAsync(TrustedDeviceModel device);
        Task UpdateLastActivityAsync(string deviceId, DateTime now, string? ip);
        Task RevokeAsync(string deviceId);
    }
}