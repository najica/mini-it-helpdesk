import { Component, OnInit } from '@angular/core';
import { NavigationEnd, Router } from '@angular/router';
import { filter } from 'rxjs/operators';
import { HealthService } from './health.service';
import { AuthService } from './services/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  standalone: false,
  styleUrl: './app.component.scss'
})
export class AppComponent implements OnInit {
  title = 'frontend';
  backendStatus = 'Provera veze sa backend-om...';
  showNav = true;

  constructor(private healthService: HealthService, private router: Router, private authService: AuthService) {}

  get currentUserName(): string | null {
    return this.authService.currentUser?.name ?? null;
  }

  logout(): void {
    this.authService.logout();
    this.router.navigate(['/login']);
  }

  ngOnInit(): void {
    this.showNav = !this.router.url.startsWith('/login');
    this.router.events.pipe(
      filter((event): event is NavigationEnd => event instanceof NavigationEnd)
    ).subscribe(event => {
      this.showNav = !event.urlAfterRedirects.startsWith('/login');
    });

    this.healthService.check().subscribe({
      next: (result) => {
        this.backendStatus = `Backend OK: ${result.application} (status: ${result.status})`;
      },
      error: () => {
        this.backendStatus = 'Backend nije dostupan. Proveri da li je API pokrenut i da CORS/HTTPS sertifikat rade.';
      }
    });
  }
}
