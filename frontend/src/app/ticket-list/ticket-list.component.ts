import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { Ticket, TicketSearchFilters, TicketService } from '../services/ticket.service';
import { CreateTicketFormComponent } from '../create-ticket-form/create-ticket-form.component';

@Component({
  selector: 'app-ticket-list',
  templateUrl: './ticket-list.component.html',
  standalone: true,
  styleUrl: './ticket-list.component.scss',
  imports: [
    CommonModule,
    FormsModule,
    RouterModule,
    CreateTicketFormComponent
  ]
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

  constructor(private ticketService: TicketService) { }

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
        const fetchedTickets = data || [];
        this.tickets = this.sortTickets(fetchedTickets);
        this.loading = false;
      },
      error: () => {
        this.errorMessage = 'Greška pri učitavanju tiketa.';
        this.loading = false;
      }
    });
  }

  private sortTickets(tickets: Ticket[]): Ticket[] {
    const statusWeight: { [key: string]: number } = {
      'Open': 1,
      'InProgress': 2,
      'Resolved': 3,
      'Closed': 4
    };

    const priorityWeight: { [key: string]: number } = {
      'Critical': 1,
      'High': 2,
      'Medium': 3,
      'Low': 4
    };

    return tickets.sort((a, b) => {
      const statusA = a.status ? (statusWeight[a.status] || 99) : 99;
      const statusB = b.status ? (statusWeight[b.status] || 99) : 99;
      if (statusA !== statusB) {
        return statusA - statusB;
      }

      const priorityA = a.priority ? (priorityWeight[a.priority] || 99) : 99;
      const priorityB = b.priority ? (priorityWeight[b.priority] || 99) : 99;
      if (priorityA !== priorityB) {
        return priorityA - priorityB;
      }

      return a.id - b.id;
    });
  }
}
