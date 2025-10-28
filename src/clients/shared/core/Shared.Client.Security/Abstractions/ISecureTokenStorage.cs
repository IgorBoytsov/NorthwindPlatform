namespace Shared.Client.Security.Abstractions
{
    public interface ISecureTokenStorage
    {
        Task<string?> GetAccessTokenAsync();
        Task<string?> GetRefreshTokenAsync();
        Task StoreTokensAsync(string accessToken, string refreshToken);
        Task ClearTokensAsync();
    }
}