namespace Shared.Contracts.Requests.Workstation
{
    public sealed record WorkstationSrpVerifyRequest(string Login, string A, string M1, string DeviceId, string DeviceFingerprintHash);
}