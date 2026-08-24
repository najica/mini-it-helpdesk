import { ComponentFixture, TestBed } from '@angular/core/testing';
import { of, throwError } from 'rxjs';
import { RouterModule } from '@angular/router';
import { TicketListComponent } from './ticket-list.component';
import { TicketService } from '../services/ticket.service';
import { Ticket } from '../models/ticket.model';

describe('TicketListComponent', () => {
  let component: TicketListComponent;
  let fixture: ComponentFixture<TicketListComponent>;
  let mockTicketService: jasmine.SpyObj<TicketService>;

  const mockTickets: Ticket[] = [
    {
      id: 1,
      title: 'Problem sa štampačem',
      description: 'Štampač na drugom spratu ne radi.',
      status: 'Open',
      priority: 'High',
      category: 'Hardware',
      createdAt: '2026-08-24T09:00:00Z',
      createdByUserId: 1,
      assignedToUserId: null
    },
    {
      id: 2,
      title: 'VPN konekcija',
      description: 'Ne mogu da se povežem na VPN.',
      status: 'InProgress',
      priority: 'Medium',
      category: 'Network',
      createdAt: '2026-08-24T09:30:00Z',
      createdByUserId: 2,
      assignedToUserId: 3
    }
  ];

  beforeEach(async () => {
    mockTicketService = jasmine.createSpyObj('TicketService', ['getAll']);

    await TestBed.configureTestingModule({
      declarations: [TicketListComponent],
      imports: [RouterModule.forRoot([])],
      providers: [
        { provide: TicketService, useValue: mockTicketService }
      ]
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TicketListComponent);
    component = fixture.componentInstance;
  });

  it('should create the component', () => {
    mockTicketService.getAll.and.returnValue(of([]));
    fixture.detectChanges();
    expect(component).toBeTruthy();
  });

  it('should call TicketService.getAll() on ngOnInit and display tickets in a table', () => {
    mockTicketService.getAll.and.returnValue(of(mockTickets));
    fixture.detectChanges();

    expect(mockTicketService.getAll).toHaveBeenCalledTimes(1);
    expect(component.tickets.length).toBe(2);
    expect(component.loading).toBeFalse();

    const compiled = fixture.nativeElement as HTMLElement;
    const rows = compiled.querySelectorAll('tbody tr');
    expect(rows.length).toBe(2);

    expect(compiled.textContent).toContain('Problem sa štampačem');
    expect(compiled.textContent).toContain('Hardware');
    expect(compiled.textContent).toContain('Open');
    expect(compiled.textContent).toContain('High');

    const detailLinks = compiled.querySelectorAll('a[href="/tickets/1"]');
    expect(detailLinks.length).toBeGreaterThan(0);
  });

  it('should display empty message when ticket list is empty', () => {
    mockTicketService.getAll.and.returnValue(of([]));
    fixture.detectChanges();

    expect(component.tickets.length).toBe(0);
    expect(component.loading).toBeFalse();

    const compiled = fixture.nativeElement as HTMLElement;
    const table = compiled.querySelector('.ticket-table');
    expect(table).toBeNull();

    const emptyState = compiled.querySelector('.empty-state');
    expect(emptyState).toBeTruthy();
    expect(emptyState?.textContent).toContain('Nema dostupnih tiketa.');
  });

  it('should display error message when TicketService.getAll fails', () => {
    mockTicketService.getAll.and.returnValue(throwError(() => new Error('Server error')));
    fixture.detectChanges();

    expect(component.loading).toBeFalse();
    expect(component.errorMessage).toBe('Greška pri učitavanju tiketa.');

    const compiled = fixture.nativeElement as HTMLElement;
    const errorState = compiled.querySelector('.error-state');
    expect(errorState).toBeTruthy();
    expect(errorState?.textContent).toContain('Greška pri učitavanju tiketa.');
  });
});

