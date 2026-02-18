export interface CryptoService {
    encryptData<T>(data: T, key: Uint8Array ): Promise<string>;
    decryptedData<T>(encryptedBase64: string, key: Uint8Array): Promise<T | null>;
    generateRandomBytes(length?: number): Uint8Array;
}