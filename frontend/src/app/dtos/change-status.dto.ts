import { TicketStatus } from '../models/ticket.model';

export interface ChangeStatusDto {
  newStatus: TicketStatus;
}
