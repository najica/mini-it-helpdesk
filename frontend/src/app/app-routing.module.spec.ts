import { ComponentFixture, TestBed, fakeAsync, tick } from '@angular/core/testing';
import { Router } from '@angular/router';
import { Location } from '@angular/common';
import { provideHttpClient } from '@angular/common/http';
import { provideHttpClientTesting } from '@angular/common/http/testing';
import { of } from 'rxjs';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { TicketListComponent } from './ticket-list/ticket-list.component';
import { TicketDetailComponent } from './ticket-detail/ticket-detail.component';
import { UserListComponent } from './user-list/user-list.component';
import { TicketService } from './services/ticket.service';
import { HealthService } from './health.service';

describe('AppRoutingModule Integration', () => {
  let router: Router;
  let location: Location;
  let fixture: ComponentFixture<AppComponent>;
  let mockTicketService: jasmine.SpyObj<TicketService>;
  let mockHealthService: jasmine.SpyObj<HealthService>;

  beforeEach(async () => {
    mockTicketService = jasmine.createSpyObj('TicketService', ['getAll', 'getById']);
    mockTicketService.getAll.and.returnValue(of([]));
    mockTicketService.getById.and.returnValue(of({
      id: 1,
      title: 'Sample Ticket',
      description: 'Description',
      status: 'Open',
      priority: 'Low',
      category: 'Hardware',
      createdAt: '2026-08-24T10:00:00Z'
    }));

    mockHealthService = jasmine.createSpyObj('HealthService', ['check']);
    mockHealthService.check.and.returnValue(of({ status: 'Healthy', application: 'MiniItHelpdesk' }));

    await TestBed.configureTestingModule({
      declarations: [
        AppComponent,
        TicketListComponent,
        TicketDetailComponent,
        UserListComponent
      ],
      imports: [
        AppRoutingModule
      ],
      providers: [
        provideHttpClient(),
        provideHttpClientTesting(),
        { provide: TicketService, useValue: mockTicketService },
        { provide: HealthService, useValue: mockHealthService }
      ]
    }).compileComponents();

    router = TestBed.inject(Router);
    location = TestBed.inject(Location);
    fixture = TestBed.createComponent(AppComponent);
    fixture.detectChanges();
  });

  it('should redirect from empty path "" to "/tickets"', fakeAsync(() => {
    router.navigate(['']);
    tick();
    expect(location.path()).toBe('/tickets');
  }));

  it('should navigate to "/tickets"', fakeAsync(() => {
    router.navigate(['/tickets']);
    tick();
    expect(location.path()).toBe('/tickets');
  }));

  it('should navigate to "/tickets/1"', fakeAsync(() => {
    router.navigate(['/tickets/1']);
    tick();
    expect(location.path()).toBe('/tickets/1');
  }));

  it('should navigate to "/users"', fakeAsync(() => {
    router.navigate(['/users']);
    tick();
    expect(location.path()).toBe('/users');
  }));
});
