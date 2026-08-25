import { Component, EventEmitter, Output } from '@angular/core';

interface CreateTicketForm {
  title: string;
  description: string;
  priority: string;
  category: string;
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

  readonly priorityOptions = ['Low', 'Medium', 'High', 'Critical'];
  readonly categoryOptions = ['Hardware', 'Software', 'Network', 'Account'];

  form: CreateTicketForm = this.getEmptyForm();

  onCancel(): void {
    this.close.emit();
  }

  onSubmit(): void {
    // TODO: povezati sa TicketService kada bude spreman endpoint za kreiranje tiketa.
    console.log('Novi tiket (nije poslat na server):', this.form);
    this.form = this.getEmptyForm();
    this.close.emit();
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
