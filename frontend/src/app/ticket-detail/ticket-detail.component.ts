import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Ticket } from '../models/ticket.model';
import { TicketService } from '../services/ticket.service';
import { Comment, CommentService } from '../comment.service';
import { HttpErrorResponse } from '@angular/common/http';
import { User } from '../models/user.model';
import { UserService } from '../services/user.service';

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

  comments: Comment[] = [];
  commentsLoading = false;
  commentsErrorMessage = '';

  users: User[] = [];

  newCommentText = '';
  newCommentUserId: number | null = null;
  submittingComment = false;
  newCommentErrorMessage = '';

  // VRAĆENA PROMENLJIVA ZA MODAL:
  showEditModal = false;

  constructor(
    private route: ActivatedRoute,
    private ticketService: TicketService,
    private commentService: CommentService,
    private userService: UserService
  ) { }

  ngOnInit(): void {
    const idParam = this.route.snapshot.paramMap.get('id');
    const ticketId = idParam ? Number(idParam) : null;

    if (ticketId) {
      this.loadTicket(ticketId);
      this.loadComments(ticketId);
      this.loadUsers();
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
      error: (err: HttpErrorResponse) => {
        this.errorMessage = err.status === 404
          ? 'Tiket nije pronađen.'
          : 'Greška pri učitavanju detalja tiketa.';
        this.loading = false;
      }
    });
  }

  loadComments(ticketId: number): void {
    this.commentsLoading = true;
    this.commentsErrorMessage = '';
    this.commentService.getByTicketId(ticketId).subscribe({
      next: (comments) => {
        this.comments = comments ?? [];
        this.commentsLoading = false;
      },
      error: () => {
        this.comments = [];
        this.commentsErrorMessage = 'Greška pri učitavanju komentara.';
        this.commentsLoading = false;
      }
    });
  }

  loadUsers(): void {
    this.userService.getAll().subscribe({
      next: (users) => {
        this.users = users ?? [];
      },
      error: () => {
        this.users = [];
      }
    });
  }

  submitComment(): void {
    this.newCommentErrorMessage = '';

    const text = this.newCommentText.trim();
    if (!text) {
      this.newCommentErrorMessage = 'Tekst komentara je obavezan.';
      return;
    }

    if (!this.newCommentUserId) {
      this.newCommentErrorMessage = 'Izaberite korisnika koji ostavlja komentar.';
      return;
    }

    if (!this.ticket) {
      return;
    }

    this.submittingComment = true;
    this.commentService.create(this.ticket.id, { text, userId: this.newCommentUserId }).subscribe({
      next: (comment) => {
        this.comments.push(comment);
        this.newCommentText = '';
        this.newCommentUserId = null;
        this.submittingComment = false;
      },
      error: (err: HttpErrorResponse) => {
        this.submittingComment = false;
        if (err.status === 404) {
          this.newCommentErrorMessage = 'Tiket nije pronađen.';
        } else if (err.status === 400) {
          this.newCommentErrorMessage = 'Neispravan unos. Proverite polja i pokušajte ponovo.';
        } else {
          this.newCommentErrorMessage = 'Greška pri slanju komentara.';
        }
      }
    });
  }

  // VRAĆENE METODE ZA OTVARANJE MODALA I OSVEŽAVANJE:
  openEditModal(): void {
    this.showEditModal = true;
  }

  closeEditModal(): void {
    this.showEditModal = false;
  }

  onTicketUpdated(): void {
    if (this.ticket) {
      this.loadTicket(this.ticket.id);
    }
  }
}
