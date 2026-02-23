INSERT INTO trust_devices (
    id, user_id, device_id, device_fingerprint_hash,
    auth_service_session_id, first_seen_at, last_activity_at,
    expires_at, is_active, created_ip, last_ip
) VALUES (
    @Id, @UserId, @DeviceId, @DeviceFingerprintHash,
    @AuthServiceSessionId, @FirstSeenAt, @LastActivityAt,
    @ExpiresAt, @IsActive, @CreatedIp, @LastIp
);