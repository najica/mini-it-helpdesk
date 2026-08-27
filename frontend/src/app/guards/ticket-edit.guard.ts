import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, Router } from '@angular/router';
import { Observable, of } from 'rxjs';
import { map, catchError } from 'rxjs/operators';
import { TicketService } from '../services/ticket.service';

@Injectable({
  providedIn: 'root'
})
export class TicketEditGuard implements CanActivate {

  constructor(
    private ticketService: TicketService,
    private router: Router
  ) { }

  canActivate(route: ActivatedRouteSnapshot): Observable<boolean> | boolean {
    const idParam = route.paramMap.get('id');
    const id = idParam ? Number(idParam) : null;

    if (!id || isNaN(id)) {
      this.router.navigate(['/tickets']);
      return false;
    }

    return this.ticketService.getById(id).pipe(
      map(ticket => {
        if (ticket) {
          return true;
        }
        this.router.navigate(['/tickets']);
        return false;
      }),
      catchError(() => {
        this.router.navigate(['/tickets']);
        return of(false);
      })
    );
  }
}
