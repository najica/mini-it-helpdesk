import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../environments/environment';

export interface HealthStatus {
  status: string;
  application: string;
}

@Injectable({ providedIn: 'root' })
export class HealthService {
  constructor(private http: HttpClient) {}

  check(): Observable<HealthStatus> {
    return this.http.get<HealthStatus>(`${environment.apiUrl}/health`);
  }
}
