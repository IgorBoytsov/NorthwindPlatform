namespace Shared.Contracts.Responses.Security
{
    public sealed record AuthenticationResponse(string AccessToken, string RefreshToken);
}