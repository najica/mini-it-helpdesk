import { Component, EventEmitter, Output } from '@angular/core';
import { CreateTicketDto } from '../dtos/create-ticket.dto';
import { TicketCategory, TicketPriority } from '../models/ticket.model';
import { TicketService } from '../services/ticket.service';

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
  standalone: false,
  styleUrl: './create-ticket-form.component.scss'
})
export class CreateTicketFormComponent {
  @Output() close = new EventEmitter<void>();
  @Output() created = new EventEmitter<void>();

  readonly priorityOptions: TicketPriority[] = ['Low', 'Medium', 'High', 'Critical'];
  readonly categoryOptions: TicketCategory[] = ['Hardware', 'Software', 'Network', 'Account'];

  form: CreateTicketForm = this.getEmptyForm();
  submitting = false;
  errorMessage = '';

  constructor(private ticketService: TicketService) {}

  onCancel(): void {
    this.close.emit();
  }

  onSubmit(): void {
    if (!this.form.category || this.form.createdByUserId == null) {
      return;
    }

    this.submitting = true;
    this.errorMessage = '';

    const payload: CreateTicketDto = {
      title: this.form.title,
      description: this.form.description,
      ticketCategory: this.form.category,
      createdByUserId: this.form.createdByUserId,
      priority: this.form.priority || undefined
    };

    this.ticketService.create(payload).subscribe({
      next: () => {
        this.submitting = false;
        this.form = this.getEmptyForm();
        this.created.emit();
        this.close.emit();
      },
      error: () => {
        this.submitting = false;
        this.errorMessage = 'Greška pri kreiranju tiketa. Pokušajte ponovo.';
      }
    });
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
