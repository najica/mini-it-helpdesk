import { TicketCategory, TicketPriority } from '../models/ticket.model';

export interface CreateTicketDto {
  title: string;
  description: string;
  priority?: TicketPriority;
  ticketCategory: TicketCategory;
  createdByUserId: number;
}
