import { Component, EventEmitter, Output, Input, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { CreateTicketDto } from '../dtos/create-ticket.dto';
import { TicketCategory, TicketPriority, Ticket } from '../models/ticket.model';
import { TicketService } from '../services/ticket.service';
import { AuthService } from '../services/auth.service';

interface CreateTicketForm {
  title: string;
  description: string;
  priority: TicketPriority | '';
  category: TicketCategory | '';
  createdByUserId: number | null;
}

@Component({
  selector: 'app-create-ticket-form',
  templateUrl: './create-ticket-form.component.html',
  standalone: true,
  styleUrl: './create-ticket-form.component.scss',
  imports: [CommonModule, FormsModule]
})
export class CreateTicketFormComponent implements OnInit {
  @Input() editTicket: Ticket | null = null;

  @Output() close = new EventEmitter<void>();
  @Output() created = new EventEmitter<void>();
  @Output() updated = new EventEmitter<void>();

  readonly priorityOptions: TicketPriority[] = ['Low', 'Medium', 'High', 'Critical'];
  readonly categoryOptions: TicketCategory[] = ['Hardware', 'Software', 'Network', 'Account'];

  form: CreateTicketForm = this.getEmptyForm();
  submitting = false;
  errorMessage = '';
  isEditMode = false;

  constructor(private ticketService: TicketService, private authService: AuthService) { }

  ngOnInit(): void {
    if (this.editTicket) {
      this.isEditMode = true;
      this.form = {
        title: this.editTicket.title,
        description: this.editTicket.description,
        priority: this.editTicket.priority || '',
        category: this.editTicket.category || '',
        createdByUserId: this.editTicket.createdByUserId ?? null
      };
    } else {
      this.form.createdByUserId = this.authService.currentUser?.userId ?? null;
    }
  }

  onCancel(): void {
    this.close.emit();
  }

  onSubmit(): void {
    if (!this.form.category || this.form.createdByUserId == null) {
      return;
    }

    this.submitting = true;
    this.errorMessage = '';

    const payload: Partial<Ticket> = {
      title: this.form.title,
      description: this.form.description,
      category: this.form.category as TicketCategory,
      createdByUserId: this.form.createdByUserId,
      priority: this.form.priority as TicketPriority || undefined
    };

    if (this.isEditMode && this.editTicket) {
      this.ticketService.update(this.editTicket.id, payload).subscribe({
        next: () => {
          this.submitting = false;
          this.updated.emit();
          this.close.emit();
        },
        error: (err) => {
          this.submitting = false;
          this.errorMessage = err.status === 400 ? 'Neispravni podaci. Molimo proverite formu.' : 'Greška pri izmeni tiketa.';
        }
      });
    } else {
      const createPayload: CreateTicketDto = {
        title: this.form.title,
        description: this.form.description,
        ticketCategory: this.form.category as TicketCategory,
        createdByUserId: this.form.createdByUserId,
        priority: this.form.priority as TicketPriority || undefined
      };

      this.ticketService.create(createPayload).subscribe({
        next: () => {
          this.submitting = false;
          this.form = this.getEmptyForm();
          this.created.emit();
          this.close.emit();
        },
        error: (err) => {
          this.submitting = false;
          this.errorMessage = err.status === 400 ? 'Neispravni podaci.' : 'Greška pri kreiranju tiketa.';
        }
      });
    }
  }

  private getEmptyForm(): CreateTicketForm {
    return {
      title: '',
      description: '',
      priority: '',
      category: '',
      createdByUserId: null
    };
  }
}
