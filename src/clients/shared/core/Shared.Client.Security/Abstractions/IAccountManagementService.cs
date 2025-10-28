using Common.Core.Results;
using Shared.Contracts.Requests.Security;

namespace Shared.Client.Security.Abstractions
{
    public interface IAccountManagementService
    {
        Task<Result> RegisterAsync(RegisterRequest request, CancellationToken cancellationToken = default);
        Task<Result> ForgotPasswordAsync(ForgotPasswordRequest request, CancellationToken cancellationToken = default);
        Task<Result> ResetPasswordAsync(ResetPasswordRequest request, CancellationToken cancellationToken = default);
    }
}