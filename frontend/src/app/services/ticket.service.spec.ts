import { TestBed } from '@angular/core/testing';
import { provideHttpClient } from '@angular/common/http';
import { HttpTestingController, provideHttpClientTesting } from '@angular/common/http/testing';
import { TicketService } from './ticket.service';
import { Ticket } from '../models/ticket.model';
import { environment } from '../../environments/environment';

describe('TicketService', () => {
  let service: TicketService;
  let httpTestingController: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [
        TicketService,
        provideHttpClient(),
        provideHttpClientTesting()
      ]
    });
    service = TestBed.inject(TicketService);
    httpTestingController = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpTestingController.verify();
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should fetch all tickets via GET', () => {
    const mockTickets: Ticket[] = [
      {
        id: 1,
        title: 'Test Ticket',
        description: 'Test Description',
        status: 'Open',
        priority: 'Low',
        category: 'Software',
        createdAt: '2026-08-24T10:00:00Z'
      }
    ];

    service.getAll().subscribe((tickets) => {
      expect(tickets.length).toBe(1);
      expect(tickets).toEqual(mockTickets);
    });

    const req = httpTestingController.expectOne(`${environment.apiUrl}/tickets`);
    expect(req.request.method).toBe('GET');
    req.flush(mockTickets);
  });

  it('should fetch a single ticket by id via GET', () => {
    const mockTicket: Ticket = {
      id: 1,
      title: 'Test Ticket',
      description: 'Test Description',
      status: 'Open',
      priority: 'Low',
      category: 'Software',
      createdAt: '2026-08-24T10:00:00Z'
    };

    service.getById(1).subscribe((ticket) => {
      expect(ticket).toEqual(mockTicket);
    });

    const req = httpTestingController.expectOne(`${environment.apiUrl}/tickets/1`);
    expect(req.request.method).toBe('GET');
    req.flush(mockTicket);
  });
});

