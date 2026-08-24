import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Ticket } from '../models/ticket.model';
import { TicketService } from '../services/ticket.service';

@Component({
  selector: 'app-ticket-detail',
  templateUrl: './ticket-detail.component.html',
  standalone: false,
  styleUrl: './ticket-detail.component.scss'
})
export class TicketDetailComponent implements OnInit {
  ticket: Ticket | null = null;
  loading = true;
  errorMessage = '';

  constructor(
    private route: ActivatedRoute,
    private ticketService: TicketService
  ) {}

  ngOnInit(): void {
    const idParam = this.route.snapshot.paramMap.get('id');
    const ticketId = idParam ? Number(idParam) : null;

    if (ticketId) {
      this.loadTicket(ticketId);
    } else {
      this.loading = false;
      this.errorMessage = 'Nevažeći ID tiketa.';
    }
  }

  loadTicket(id: number): void {
    this.loading = true;
    this.errorMessage = '';
    this.ticketService.getById(id).subscribe({
      next: (ticket) => {
        this.ticket = ticket;
        this.loading = false;
      },
      error: () => {
        this.errorMessage = 'Greška pri učitavanju detalja tiketa.';
        this.loading = false;
      }
    });
  }
}

