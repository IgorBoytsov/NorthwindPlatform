namespace Shared.Contracts.Requests.AuthenticationService
{
    public sealed record SrpVerifyRequest(string Login, string A, string M1);
}