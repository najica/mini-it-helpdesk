import { ComponentFixture, TestBed } from '@angular/core/testing';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { HttpErrorResponse } from '@angular/common/http';
import { of, throwError } from 'rxjs';
import { TicketDetailComponent } from './ticket-detail.component';
import { TicketService } from '../services/ticket.service';
import { Ticket } from '../models/ticket.model';
import { Comment, CommentService } from '../comment.service';
import { User } from '../models/user.model';
import { UserService } from '../services/user.service';

describe('TicketDetailComponent', () => {
  let component: TicketDetailComponent;
  let fixture: ComponentFixture<TicketDetailComponent>;
  let mockTicketService: jasmine.SpyObj<TicketService>;
  let mockCommentService: jasmine.SpyObj<CommentService>;
  let mockUserService: jasmine.SpyObj<UserService>;

  const mockTicket: Ticket = {
    id: 1,
    title: 'Test Ticket Detail',
    description: 'Detailed description for test ticket.',
    status: 'Open',
    priority: 'High',
    category: 'Hardware',
    createdAt: '2026-08-24T10:00:00Z',
    createdByUserId: 1,
    assignedToUserId: null
  };

  const mockComments: Comment[] = [
    {
      id: 10,
      ticketId: 1,
      userId: 2,
      text: 'Prvi komentar na tiketu.',
      createdAt: '2026-08-24T11:00:00Z'
    },
    {
      id: 11,
      ticketId: 1,
      userId: 3,
      text: 'Drugi komentar na tiketu.',
      createdAt: '2026-08-24T12:30:00Z'
    }
  ];

  const mockUsers: User[] = [
    { id: 2, name: 'Marko Marković', email: 'marko@example.com', role: 'Employee' },
    { id: 3, name: 'Ana Anić', email: 'ana@example.com', role: 'ITAgent' }
  ];

  beforeEach(async () => {
    mockTicketService = jasmine.createSpyObj('TicketService', ['getById']);
    mockCommentService = jasmine.createSpyObj('CommentService', ['getByTicketId', 'create']);
    mockUserService = jasmine.createSpyObj('UserService', ['getAll']);

    await TestBed.configureTestingModule({
      declarations: [TicketDetailComponent],
      imports: [RouterModule.forRoot([]), FormsModule],
      providers: [
        { provide: TicketService, useValue: mockTicketService },
        { provide: CommentService, useValue: mockCommentService },
        { provide: UserService, useValue: mockUserService },
        {
          provide: ActivatedRoute,
          useValue: {
            snapshot: {
              paramMap: {
                get: (key: string) => (key === 'id' ? '1' : null)
              }
            }
          }
        }
      ]
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TicketDetailComponent);
    component = fixture.componentInstance;
    mockCommentService.getByTicketId.and.returnValue(of([]));
    mockUserService.getAll.and.returnValue(of(mockUsers));
  });

  it('should create the component', () => {
    mockTicketService.getById.and.returnValue(of(mockTicket));
    fixture.detectChanges();
    expect(component).toBeTruthy();
  });

  it('should load ticket details on init', () => {
    mockTicketService.getById.and.returnValue(of(mockTicket));
    fixture.detectChanges();

    expect(mockTicketService.getById).toHaveBeenCalledWith(1);
    expect(component.ticket).toEqual(mockTicket);
    expect(component.loading).toBeFalse();

    const compiled = fixture.nativeElement as HTMLElement;
    expect(compiled.textContent).toContain('Test Ticket Detail');
    expect(compiled.textContent).toContain('Detailed description for test ticket.');
    expect(compiled.textContent).toContain('Hardware');
    expect(compiled.textContent).toContain('High');
  });

  it('should handle error when ticket service fails', () => {
    mockTicketService.getById.and.returnValue(throwError(() => new Error('Not found')));
    fixture.detectChanges();

    expect(component.loading).toBeFalse();
    expect(component.errorMessage).toBe('Greška pri učitavanju detalja tiketa.');

    const compiled = fixture.nativeElement as HTMLElement;
    expect(compiled.textContent).toContain('Greška pri učitavanju detalja tiketa.');
  });

  it('should load and render comments for the ticket', () => {
    mockTicketService.getById.and.returnValue(of(mockTicket));
    mockCommentService.getByTicketId.and.returnValue(of(mockComments));
    fixture.detectChanges();

    expect(mockCommentService.getByTicketId).toHaveBeenCalledWith(1);
    expect(component.comments.length).toBe(2);
    expect(component.commentsLoading).toBeFalse();

    const compiled = fixture.nativeElement as HTMLElement;
    expect(compiled.querySelectorAll('.comment-item').length).toBe(2);
    expect(compiled.textContent).toContain('Prvi komentar na tiketu.');
    expect(compiled.textContent).toContain('Korisnik #3');
  });

  it('should show empty message when there are no comments', () => {
    mockTicketService.getById.and.returnValue(of(mockTicket));
    mockCommentService.getByTicketId.and.returnValue(of([]));
    fixture.detectChanges();

    const compiled = fixture.nativeElement as HTMLElement;
    expect(compiled.querySelectorAll('.comment-item').length).toBe(0);
    expect(compiled.textContent).toContain('Nema komentara');
  });

  it('should handle error when comment service fails', () => {
    mockTicketService.getById.and.returnValue(of(mockTicket));
    mockCommentService.getByTicketId.and.returnValue(throwError(() => new Error('boom')));
    fixture.detectChanges();

    expect(component.comments).toEqual([]);
    expect(component.commentsErrorMessage).toBe('Greška pri učitavanju komentara.');

    const compiled = fixture.nativeElement as HTMLElement;
    expect(compiled.textContent).toContain('Greška pri učitavanju komentara.');
  });

  it('should load users for the comment author dropdown', () => {
    mockTicketService.getById.and.returnValue(of(mockTicket));
    fixture.detectChanges();

    expect(mockUserService.getAll).toHaveBeenCalled();
    expect(component.users).toEqual(mockUsers);
  });

  it('should not submit a comment with empty text and should show a validation message', () => {
    mockTicketService.getById.and.returnValue(of(mockTicket));
    fixture.detectChanges();

    component.newCommentText = '   ';
    component.newCommentUserId = 2;
    component.submitComment();

    expect(mockCommentService.create).not.toHaveBeenCalled();
    expect(component.newCommentErrorMessage).toBe('Tekst komentara je obavezan.');
  });

  it('should not submit a comment without a selected user', () => {
    mockTicketService.getById.and.returnValue(of(mockTicket));
    fixture.detectChanges();

    component.newCommentText = 'Neki tekst';
    component.newCommentUserId = null;
    component.submitComment();

    expect(mockCommentService.create).not.toHaveBeenCalled();
    expect(component.newCommentErrorMessage).toBe('Izaberite korisnika koji ostavlja komentar.');
  });

  it('should create a comment and show it in the list immediately on success', () => {
    mockTicketService.getById.and.returnValue(of(mockTicket));
    fixture.detectChanges();

    const created: Comment = {
      id: 20,
      ticketId: 1,
      userId: 2,
      text: 'Novi komentar',
      createdAt: '2026-08-25T09:00:00Z'
    };
    mockCommentService.create.and.returnValue(of(created));

    component.newCommentText = 'Novi komentar';
    component.newCommentUserId = 2;
    component.submitComment();

    expect(mockCommentService.create).toHaveBeenCalledWith(1, { text: 'Novi komentar', userId: 2 });
    expect(component.comments).toContain(created);
    expect(component.newCommentText).toBe('');
    expect(component.newCommentUserId).toBeNull();
    expect(component.submittingComment).toBeFalse();
  });

  it('should show "Tiket nije pronađen" when comment creation fails with 404', () => {
    mockTicketService.getById.and.returnValue(of(mockTicket));
    fixture.detectChanges();

    mockCommentService.create.and.returnValue(
      throwError(() => new HttpErrorResponse({ status: 404 }))
    );

    component.newCommentText = 'Neki tekst';
    component.newCommentUserId = 2;
    component.submitComment();

    expect(component.newCommentErrorMessage).toBe('Tiket nije pronađen.');
    expect(component.submittingComment).toBeFalse();
  });

  it('should show a validation error message when comment creation fails with 400', () => {
    mockTicketService.getById.and.returnValue(of(mockTicket));
    fixture.detectChanges();

    mockCommentService.create.and.returnValue(
      throwError(() => new HttpErrorResponse({ status: 400 }))
    );

    component.newCommentText = 'Neki tekst';
    component.newCommentUserId = 2;
    component.submitComment();

    expect(component.newCommentErrorMessage).toBe('Neispravan unos. Proverite polja i pokušajte ponovo.');
    expect(component.submittingComment).toBeFalse();
  });
});

