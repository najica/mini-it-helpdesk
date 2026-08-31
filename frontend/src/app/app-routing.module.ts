import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TicketListComponent } from './ticket-list/ticket-list.component';
import { TicketDetailComponent } from './ticket-detail/ticket-detail.component';
import { UserListComponent } from './user-list/user-list.component';
import { TicketFormComponent } from './ticket-form/ticket-form.component';
import { LoginComponent } from './login/login.component';

import { TicketEditGuard } from './guards/ticket-edit.guard';
import { AuthGuard } from './guards/auth.guard';

const routes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: 'tickets', component: TicketListComponent, canActivate: [AuthGuard] },
  { path: 'tickets/:id', component: TicketDetailComponent, canActivate: [AuthGuard] },
  { path: 'users', component: UserListComponent, canActivate: [AuthGuard] },
  { path: '', redirectTo: '/login', pathMatch: 'full' },
  { path: 'tickets/new', component: TicketFormComponent, canActivate: [AuthGuard] },

  {
    path: 'tickets/edit/:id',
    component: TicketFormComponent,
    canActivate: [AuthGuard, TicketEditGuard]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
