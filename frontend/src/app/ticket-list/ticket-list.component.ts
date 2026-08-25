import { Component, OnInit } from '@angular/core';
// Add these imports at the top
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { Ticket, TicketSearchFilters, TicketService } from '../services/ticket.service';
// DODATO: Import za tvoju novu formu
import { CreateTicketFormComponent } from '../create-ticket-form/create-ticket-form.component';
import { AssignTicketFormComponent } from '../assign-ticket-form/assign-ticket-form.component';

@Component({
  selector: 'app-ticket-list',
  templateUrl: './ticket-list.component.html',
  standalone: true,
  styleUrl: './ticket-list.component.scss',
  // ADD THIS IMPORTS ARRAY:
  imports: [
    CommonModule,
    FormsModule,
    RouterModule,
    // DODATO: Komponenta registrovana ovde
    CreateTicketFormComponent,
    AssignTicketFormComponent
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
  assignTicketId: number | null = null;
  assignTicketAssignedToUserId: number | null = null;

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

  openAssignModal(ticket: Ticket): void {
    this.assignTicketId = ticket.id;
    this.assignTicketAssignedToUserId = ticket.assignedToUserId ?? null;
  }

  closeAssignModal(): void {
    this.assignTicketId = null;
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
