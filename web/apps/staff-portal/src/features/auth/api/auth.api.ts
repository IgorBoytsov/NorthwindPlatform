import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { SrpChallengeRequest, SrpVerifyRequest, SrpChallengeResponse, AuthResponse } from '@northwindplatform/shared/contracts';

@Injectable({
    providedIn: 'root'
})
export class AuthApi {
    private http: HttpClient = inject(HttpClient);
    private baseUrl = 'http://localhost:5121'; // BFF URL

    getCrpChallenge(data: SrpChallengeRequest): Observable<SrpChallengeResponse> {
        return this.http.post<SrpChallengeResponse>(`${this.baseUrl}/srp/challenge`, data);
    }
    
    srpVerifyProof(data: SrpVerifyRequest): Observable<AuthResponse> {
        return this.http.post<AuthResponse>(`${this.baseUrl}/srp/verify`, data, { withCredentials: true });
    }
}