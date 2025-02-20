import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { RegisterComponent } from './auth/register/register.component';
import { LoginComponent } from './auth/login/login.component';
import { AuthGuard } from './auth/auth.guard';
import { ProtectedComponent } from './auth/protected/protected.component';
import { CategoriesWithAllEventsComponent } from './features/categoriesWithAllEvents/categoriesWithAllEvents.component';
import { EventDetailComponent } from './features/event-detail/event-detail.component';
import { UserDashboardComponent } from './features/user-dashboard/user-dashboard.component';
import { AddEventComponent } from './features/addevent/addevent.component';
import { AdminDashboardComponent } from './features/admin-dashboard/admin-dashboard.component';
import { UpdateEventComponent } from './features/update-event/update-event.component';

export const routes: Routes = [
  { path: '', redirectTo: "home", pathMatch: "full" }, // Match the empty path exactly
  { path: 'home', title: "Volunteer Connect - Home", component: HomeComponent }, // Home route
  { path: 'register', component: RegisterComponent }, // Register route
  { path: 'login', component: LoginComponent }, // Login route
  { path: 'categorieswithallevents/:categoryId', component: CategoriesWithAllEventsComponent },
  { path: 'detailedevent/:id', component: EventDetailComponent }, // Event detail route
  { path: 'userdashboard/:id', component: UserDashboardComponent }, // Event detail route
  { path: 'admindashboard/:id', component: AdminDashboardComponent }, // Event detail route
  { path: 'addevent',component: AddEventComponent}, // add event route
  { path: 'update-event/:id', component: UpdateEventComponent},
  
  
  { path: 'protected', component: ProtectedComponent, canActivate: [AuthGuard] }, // Protected route

];

