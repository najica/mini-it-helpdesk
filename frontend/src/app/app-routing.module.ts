import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TicketListComponent } from './ticket-list/ticket-list.component';
import { TicketDetailComponent } from './ticket-detail/ticket-detail.component';
import { UserListComponent } from './user-list/user-list.component';
import { TicketFormComponent } from './ticket-form/ticket-form.component';

const routes: Routes = [
  { path: 'tickets', component: TicketListComponent },
  { path: 'tickets/:id', component: TicketDetailComponent },
  { path: 'users', component: UserListComponent },
  { path: '', redirectTo: '/tickets', pathMatch: 'full' },
{ path: 'tickets/new', component: TicketFormComponent },
  { path: 'tickets/edit/:id', component: TicketFormComponent } // :id je ključno za edit mod!
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

