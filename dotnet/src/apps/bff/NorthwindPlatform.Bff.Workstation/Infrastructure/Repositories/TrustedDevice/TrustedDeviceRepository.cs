using System.Data;
using Dapper;
using NorthwindPlatform.Bff.Workstation.Infrastructure.Helpers;

using TrustedDeviceModel = NorthwindPlatform.Bff.Workstation.Models.Domain.Entities.TrustedDevice;

namespace NorthwindPlatform.Bff.Workstation.Infrastructure.Repositories.TrustedDevice
{
    public class TrustedDeviceRepository(IDbConnection connection) : ITrustedDeviceRepository
    {
        private readonly IDbConnection _connection = connection;

        private const string repo = "TrustedDevice";

        private readonly string _getByDeviceIdSql = SqlLoader.Load(repo, "GetByDeviceId");
        private readonly string _insertSql = SqlLoader.Load(repo, "Insert");
        private readonly string _updateLastActivitySql = SqlLoader.Load(repo, "UpdateLastActivity");
        private readonly string _revokeSql = SqlLoader.Load(repo, "Revoke");

        public async Task<TrustedDeviceModel?> GetByDeviceIdAsync(string deviceId)
        {
            var row = await _connection.QueryFirstOrDefaultAsync(_getByDeviceIdSql, new { deviceId });

            return row == null ? null : MapRow(row);
        }

        public async Task InsertAsync(TrustedDeviceModel device) 
            =>  await _connection.ExecuteAsync(_insertSql, device);

        public async Task UpdateLastActivityAsync(string deviceId, DateTime now, string? ip)
            => await _connection.ExecuteAsync(_updateLastActivitySql, new { deviceId });

        public async Task RevokeAsync(string deviceId) 
            => await _connection.ExecuteAsync(_revokeSql, new { deviceId });

        private static TrustedDeviceModel MapRow(dynamic row) => new()
        {
            Id = row.id,
            UserId = row.user_id,
            DeviceId = row.device_id,
            DeviceFingerprintHash = row.device_fingerprint_hash,
            AuthServiceSessionId = row.auth_service_session_id,
            FirstSeenAt = row.first_seen_at,
            LastActivityAt = row.last_activity_at,
            ExpiresAt = row.expires_at,
            IsActive = row.is_active,
            RevokedAt = row.revoked_at,
            CreatedIp = row.created_ip,
            LastIp = row.last_ip
        };
    }
}