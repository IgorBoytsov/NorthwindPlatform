namespace Shared.Client.Security.Abstractions
{
    public class DeviceIdentity
    {
        public string DeviceId { get; set; } = Guid.NewGuid().ToString();
        public byte[] FingerprintHash { get; set; } = [];
    }
}