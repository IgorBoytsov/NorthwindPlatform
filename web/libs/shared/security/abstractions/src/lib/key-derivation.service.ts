export interface KeyDerivationService {
    deriveKeysFromPassword(password: string, salt: Uint8Array) : Promise<{ kek: Uint8Array, authHash: string }>;
}