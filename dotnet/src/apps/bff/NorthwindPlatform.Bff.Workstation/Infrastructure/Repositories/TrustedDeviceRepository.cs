using System.Data;
using Dapper;
using NorthwindPlatform.Bff.Workstation.Models.Domain.Entities;

namespace NorthwindPlatform.Bff.Workstation.Infrastructure.Repositories
{
    public class TrustedDeviceRepository : ITrustedDeviceRepository
    {
        private readonly IDbConnection _connection;

        public TrustedDeviceRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<TrustedDevice?> GetByDeviceIdAsync(string deviceId)
        {
            const string sql = @"
                SELECT id, user_id, device_id, device_fingerprint_hash, 
                    auth_service_session_id, first_seen_at, last_activity_at,
                    expires_at, is_active, revoked_at, created_ip, last_ip
                FROM trust_devices 
                WHERE device_id = @deviceId AND is_active = true";

            var row = await _connection.QueryFirstOrDefaultAsync(sql, new { deviceId });
            return row == null ? null : MapRow(row);
        }

        public async Task InsertAsync(TrustedDevice device)
        {
            const string sql = @"
                INSERT INTO trust_devices (
                    id, user_id, device_id, device_fingerprint_hash,
                    auth_service_session_id, first_seen_at, last_activity_at,
                    expires_at, is_active, created_ip, last_ip
                ) VALUES (
                    @Id, @UserId, @DeviceId, @DeviceFingerprintHash,
                    @AuthServiceSessionId, @FirstSeenAt, @LastActivityAt,
                    @ExpiresAt, @IsActive, @CreatedIp, @LastIp
                )";

            await _connection.ExecuteAsync(sql, device);
        }

        public async Task UpdateLastActivityAsync(string deviceId, DateTime now, string? ip)
        {
            const string sql = @"
                UPDATE trust_devices 
                SET last_activity_at = @now, last_ip = @ip
                WHERE device_id = @deviceId AND is_active = true";

            await _connection.ExecuteAsync(sql, new { deviceId, now, ip });
        }

        public async Task RevokeAsync(string deviceId)
        {
            const string sql = @"
                UPDATE trust_devices 
                SET is_active = false, revoked_at = NOW()
                WHERE device_id = @deviceId AND is_active = true";

            await _connection.ExecuteAsync(sql, new { deviceId });
        }

        private static TrustedDevice MapRow(dynamic row) => new()
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