namespace Shared.Contracts.Responses.AuthenticationService
{
    public sealed record AuthResponse(string AccessToken, string RefreshToken, string? M2 = null);
}