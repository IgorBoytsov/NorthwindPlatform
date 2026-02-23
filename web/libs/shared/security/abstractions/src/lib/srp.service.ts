export interface SrpService {
  generateSrpVerifier(authHash: string): Promise<string>;
  generateSrpProof(password: string, saltBase64: string, B_base64: string ): Promise<{ A: string; M1: string; S: string }>;
  verifyServerM2(A: string, M1: string, S: string, serverM2: string): Promise<boolean>;
}