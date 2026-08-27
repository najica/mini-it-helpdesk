import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { TicketService } from '../services/ticket.service';

@Component({
  selector: 'app-ticket-form',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterModule],
  templateUrl: './ticket-form.component.html',
  styleUrl: './ticket-form.component.scss'
})
export class TicketFormComponent implements OnInit {
  ticketForm: FormGroup;
  isEditMode = false;
  ticketId: number | null = null;
  errorMessage = '';
  loading = false;

  readonly statusOptions = ['Open', 'InProgress', 'Resolved', 'Closed'];
  readonly priorityOptions = ['Low', 'Medium', 'High', 'Critical'];
  readonly categoryOptions = ['Hardware', 'Software', 'Network', 'Account'];

  constructor(
    private fb: FormBuilder,
    private ticketService: TicketService,
    private route: ActivatedRoute,
    private router: Router
  ) {
    this.ticketForm = this.fb.group({
      title: ['', Validators.required],
      description: ['', Validators.required],
      status: ['Open'],
      priority: ['Medium'],
      category: ['Software']
    });
  }

  ngOnInit(): void {
    const idParam = this.route.snapshot.paramMap.get('id');
    if (idParam) {
      this.isEditMode = true;
      this.ticketId = Number(idParam);
      this.loadTicketData(this.ticketId);
    }
  }

  loadTicketData(id: number): void {
    this.loading = true;

    this.ticketService.getById(id).subscribe({
      next: (ticket) => {
        this.ticketForm.patchValue({
          title: ticket.title,
          description: ticket.description,
          status: ticket.status,
          priority: ticket.priority,
          category: ticket.category
        });
        this.loading = false;
      },
      error: () => {
        this.errorMessage = 'Greška pri učitavanju tiketa.';
        this.loading = false;
      }
    });
  }

  onSubmit(): void {
    if (this.ticketForm.invalid) {
      this.ticketForm.markAllAsTouched();
      return;
    }

    this.loading = true;
    this.errorMessage = '';
    const formData = this.ticketForm.value;

    if (this.isEditMode && this.ticketId) {
      this.ticketService.update(this.ticketId, formData).subscribe({
        next: () => this.router.navigate(['/tickets', this.ticketId]),
        error: (err) => this.handleError(err)
      });
    } else {
      this.ticketService.create(formData).subscribe({
        next: () => this.router.navigate(['/tickets']),
        error: (err) => this.handleError(err)
      });
    }
  }

  private handleError(error: any): void {
    this.loading = false;
    if (error.status === 400) {
      this.errorMessage = 'Neispravni podaci. Molimo proverite formu.';
    } else {
      this.errorMessage = 'Došlo je do greške na serveru. Pokušajte ponovo.';
    }
  }
}
