namespace Shared.Contracts.Responses.AuthenticationService
{
    public sealed record SrpChallengeResponse(string Salt, string B);
}
 