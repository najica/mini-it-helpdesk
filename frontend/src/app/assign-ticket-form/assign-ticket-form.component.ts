import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { User } from '../models/user.model';
import { UserService } from '../services/user.service';
import { TicketService } from '../services/ticket.service';

@Component({
  selector: 'app-assign-ticket-form',
  templateUrl: './assign-ticket-form.component.html',
  standalone: true,
  styleUrl: './assign-ticket-form.component.scss',
  imports: [CommonModule, FormsModule]
})
export class AssignTicketFormComponent implements OnInit {
  @Input() ticketId!: number;
  @Input() assignedToUserId: number | null = null;
  @Output() close = new EventEmitter<void>();
  @Output() assigned = new EventEmitter<void>();

  agents: User[] = [];
  selectedAgentId: number | null = null;
  loadingAgents = true;
  submitting = false;
  errorMessage = '';

  constructor(private userService: UserService, private ticketService: TicketService) { }

  ngOnInit(): void {
    this.selectedAgentId = this.assignedToUserId ?? null;
    this.userService.getAgents().subscribe({
      next: (agents) => {
        this.agents = agents || [];
        this.loadingAgents = false;
      },
      error: () => {
        this.errorMessage = 'Greška pri učitavanju agenata.';
        this.loadingAgents = false;
      }
    });
  }

  onCancel(): void {
    this.close.emit();
  }

  onSubmit(): void {
    if (this.selectedAgentId == null) {
      return;
    }

    this.submitting = true;
    this.errorMessage = '';

    this.ticketService.assignUser(this.ticketId, { assignedToUserId: this.selectedAgentId }).subscribe({
      next: () => {
        this.submitting = false;
        this.assigned.emit();
        this.close.emit();
      },
      error: () => {
        this.submitting = false;
        this.errorMessage = 'Greška pri dodeli tiketa. Pokušajte ponovo.';
      }
    });
  }
}
