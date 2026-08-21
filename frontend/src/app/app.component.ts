import { Component, OnInit } from '@angular/core';
import { HealthService } from './health.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  standalone: false,
  styleUrl: './app.component.scss'
})
export class AppComponent implements OnInit {
  title = 'frontend';
  backendStatus = 'Provera veze sa backend-om...';

  constructor(private healthService: HealthService) {}

  ngOnInit(): void {
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
