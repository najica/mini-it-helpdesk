import { TestBed } from '@angular/core/testing';
import { CanActivateFn } from '@angular/router';

import { ticketEditGuard } from './ticket-edit.guard';

describe('ticketEditGuard', () => {
  const executeGuard: CanActivateFn = (...guardParameters) => 
      TestBed.runInInjectionContext(() => ticketEditGuard(...guardParameters));

  beforeEach(() => {
    TestBed.configureTestingModule({});
  });

  it('should be created', () => {
    expect(executeGuard).toBeTruthy();
  });
});
