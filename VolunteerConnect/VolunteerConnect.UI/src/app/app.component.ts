import { Component, ViewEncapsulation } from '@angular/core';
import { RouterModule, RouterOutlet } from '@angular/router';
import { NavbarComponent } from './home/navbar/navbar.component';
import { AuthService } from './Services/auth.service';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, RouterModule, NavbarComponent],
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  constructor(private authService: AuthService) {
    if (this.authService.isAuthenticated()) {
      this.authService.startInactivityTimer(); // Start inactivity timer on app initialization
    }
  }
  
  title = 'VolunteerConnect.UI';
}
