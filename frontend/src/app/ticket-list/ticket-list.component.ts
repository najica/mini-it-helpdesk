import { Component, OnInit } from '@angular/core';
import { Ticket, TicketSearchFilters, TicketService } from '../services/ticket.service';

@Component({
  selector: 'app-ticket-list',
  templateUrl: './ticket-list.component.html',
  standalone: false,
  styleUrl: './ticket-list.component.scss'
})
export class TicketListComponent implements OnInit {
  tickets: Ticket[] = [];
  loading = true;
  errorMessage = '';

  readonly statusOptions = ['Open', 'InProgress', 'Resolved', 'Closed'];
  readonly priorityOptions = ['Low', 'Medium', 'High', 'Critical'];
  readonly categoryOptions = ['Hardware', 'Software', 'Network', 'Account'];

  filterStatus: string | null = null;
  filterPriority: string | null = null;
  filterCategory: string | null = null;
  filterUser: number | null = null;

  showCreateModal = false;

  constructor(private ticketService: TicketService) {}

  ngOnInit(): void {
    this.search();
  }

  openCreateModal(): void {
    this.showCreateModal = true;
  }

  closeCreateModal(): void {
    this.showCreateModal = false;
  }

  search(): void {
    this.loading = true;
    this.errorMessage = '';

    const filters: TicketSearchFilters = {
      status: this.filterStatus ?? undefined,
      priority: this.filterPriority ?? undefined,
      category: this.filterCategory ?? undefined,
      user: this.filterUser ?? undefined
    };

    this.ticketService.search(filters).subscribe({
      next: (data) => {
        this.tickets = data || [];
        this.loading = false;
      },
      error: () => {
        this.errorMessage = 'Greška pri učitavanju tiketa.';
        this.loading = false;
      }
    });
  }
}
