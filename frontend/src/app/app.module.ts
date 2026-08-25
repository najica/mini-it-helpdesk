import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { provideHttpClient } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { TicketListComponent } from './ticket-list/ticket-list.component';
import { TicketDetailComponent } from './ticket-detail/ticket-detail.component';
import { UserListComponent } from './user-list/user-list.component';

// 1. OBAVEZNO IMPORTUJ FORMU OVDE:
import { CreateTicketFormComponent } from './create-ticket-form/create-ticket-form.component';

@NgModule({
  declarations: [
    AppComponent,
    TicketDetailComponent,
    UserListComponent
    // Uveri se da CreateTicketFormComponent NIJE ovde u declarations!
  ],
  imports: [
    BrowserModule,
    FormsModule,
    AppRoutingModule,
    TicketListComponent,
    // 2. DODAJ FORMU OVDE U IMPORTS NIZ:
    CreateTicketFormComponent
  ],
  providers: [
    provideHttpClient()
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
