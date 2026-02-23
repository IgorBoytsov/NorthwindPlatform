namespace Shared.Contracts.Requests.Security
{
    public sealed record LoginByTokenRequest(string RefreshToken);
}