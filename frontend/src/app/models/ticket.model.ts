export type TicketStatus = 'Open' | 'InProgress' | 'Resolved' | 'Closed';
export type TicketPriority = 'Low' | 'Medium' | 'High' | 'Critical';
export type TicketCategory = 'Hardware' | 'Software' | 'Network' | 'Account';

export interface Ticket {
  id: number;
  title: string;
  description: string;
  status: TicketStatus;
  priority?: TicketPriority | null;
  category: TicketCategory;
  createdAt: string | Date;
  createdByUserId?: number;
  assignedToUserId?: number | null;
}

