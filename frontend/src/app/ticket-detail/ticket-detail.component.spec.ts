import { ComponentFixture, TestBed } from '@angular/core/testing';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { of, throwError } from 'rxjs';
import { TicketDetailComponent } from './ticket-detail.component';
import { TicketService } from '../services/ticket.service';
import { Ticket } from '../models/ticket.model';

describe('TicketDetailComponent', () => {
  let component: TicketDetailComponent;
  let fixture: ComponentFixture<TicketDetailComponent>;
  let mockTicketService: jasmine.SpyObj<TicketService>;

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

  beforeEach(async () => {
    mockTicketService = jasmine.createSpyObj('TicketService', ['getById']);

    await TestBed.configureTestingModule({
      declarations: [TicketDetailComponent],
      imports: [RouterModule.forRoot([])],
      providers: [
        { provide: TicketService, useValue: mockTicketService },
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
});

