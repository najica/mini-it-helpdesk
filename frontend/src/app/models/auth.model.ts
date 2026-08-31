export enum Role {
  Employee = 'Employee',
  ITAgent = 'ITAgent',
  Admin = 'Admin',
}

export interface LoginRequest {
  email: string;
  password: string;
}

export interface AuthResponse {
  token: string;
  userId: number;
  name: string;
  role: Role;
  expiresAt: string;
}