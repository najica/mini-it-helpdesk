import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';

import { AuthService } from '../services/auth.service';

interface LoginForm {
  email: string;
  password: string;
}

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  standalone: true,
  styleUrl: './login.component.scss',
  imports: [CommonModule, FormsModule]
})
export class LoginComponent implements OnInit {
  form: LoginForm = { email: '', password: '' };
  submitting = false;
  showErrorToast = false;

  constructor(private authService: AuthService, private router: Router) { }

  ngOnInit(): void {
    if (this.authService.isLoggedIn()) {
      this.router.navigate(['/tickets']);
    }
  }

  onSubmit(): void {
    this.submitting = true;
    this.showErrorToast = false;

    this.authService.login(this.form).subscribe({
      next: () => {
        this.submitting = false;
        this.router.navigate(['/tickets']);
      },
      error: () => {
        this.submitting = false;
        this.showErrorToast = true;
      }
    });
  }
}
