namespace Shared.Client.Security.Abstractions
{
    public interface IDeviceIdentityService
    {
        Task<DeviceIdentity> GetOrCreateAsync();
    }
}