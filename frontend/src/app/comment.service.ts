import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../environments/environment';

export interface Comment {
  id: number;
  ticketId: number;
  userId: number;
  text: string;
  createdAt: string | Date;
}

@Injectable({
  providedIn: 'root'
})
export class CommentService {
  private readonly apiUrl = `${environment.apiUrl}/Tickets`;

  constructor(private http: HttpClient) {}

  getByTicketId(ticketId: number): Observable<Comment[]> {
    return this.http.get<Comment[]>(`${this.apiUrl}/${ticketId}/comments`);
  }
}
