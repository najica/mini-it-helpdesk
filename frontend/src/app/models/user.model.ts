export type UserRole = 'Employee' | 'ITAgent' | 'Admin';

export interface User {
  id: number;
  name: string;
  email: string;
  role: UserRole;
}
