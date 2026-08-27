import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { CreateTicketDto } from '../dtos/create-ticket.dto';
import { Ticket } from '../models/ticket.model';
import { ChangeStatusDto } from '../dtos/change-status.dto';

export type { Ticket };

export interface TicketSearchFilters {
  status?: string;
  priority?: string;
  category?: string;
  user?: number;
}

@Injectable({ providedIn: 'root' })
export class TicketService {
  private readonly baseUrl = `${environment.apiUrl}/Tickets`;

  constructor(private http: HttpClient) { }

  getAll(): Observable<Ticket[]> {
    return this.http.get<Ticket[]>(this.baseUrl);
  }

  search(filters: TicketSearchFilters): Observable<Ticket[]> {
    const params = this.buildParams(filters);
    return this.http.get<Ticket[]>(this.baseUrl, { params });
  }

  getById(id: number): Observable<Ticket> {
    return this.http.get<Ticket>(`${this.baseUrl}/${id}`);
  }

  create(dto: CreateTicketDto): Observable<Ticket> {
    return this.http.post<Ticket>(this.baseUrl, dto);
  }

  update(id: number, dto: Partial<Ticket>): Observable<Ticket> {
    return this.http.put<Ticket>(`${this.baseUrl}/${id}`, dto);
  }

  delete(id: number): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`);
  }

  changeStatus(id: number, dto: ChangeStatusDto): Observable<Ticket> {
    return this.http.patch<Ticket>(`${this.baseUrl}/${id}/status`, dto);
  }

  assignUser(id: number, dto: Partial<Ticket>): Observable<Ticket> {
    return this.http.patch<Ticket>(`${this.baseUrl}/${id}/assign`, dto);
  }

  private buildParams(filters: TicketSearchFilters): HttpParams {
    let params = new HttpParams();

    Object.entries(filters ?? {}).forEach(([key, value]) => {
      if (value !== undefined && value !== null && value !== '') {
        params = params.set(key, String(value));
      }
    });

    return params;
  }
}
