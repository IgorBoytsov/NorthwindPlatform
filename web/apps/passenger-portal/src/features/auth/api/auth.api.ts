import { useCallback } from "react";
import axios from 'axios';
import { SrpChallengeRequest, SrpVerifyRequest, SrpChallengeResponse, AuthResponse } from '@northwindplatform/shared/contracts';

const api = axios.create({
  baseURL: 'http://localhost:5193',
  withCredentials: true, 
});

export const useAuthApi = () => {
  const getSrpChallenge = useCallback(
    async (data: SrpChallengeRequest): Promise<SrpChallengeResponse> => {
        const response = await api.post<SrpChallengeResponse>('/srp/challenge', data);
        return response.data
    },
    []
  );

  const srpVerifyProof = useCallback(
    async (data: SrpVerifyRequest): Promise<AuthResponse> => {
        const response = await api.post<AuthResponse>('/srp/verify', data);
        return response.data;
    },
    []
  );

  return { getSrpChallenge, srpVerifyProof };
};