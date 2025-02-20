import { ChangeDetectorRef, Component } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { CommonModule, NgIf } from '@angular/common';
import { AuthService } from '../../Services/auth.service';
import { FormsModule} from '@angular/forms';

@Component({
  selector: 'app-navbar',
  imports: [NgIf,FormsModule, CommonModule, RouterModule],
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css'],
})
export class NavbarComponent {
  isLoggedIn: boolean = false;
  role: string | null = null;
  userId: string | null = null;
  userName: string | null = null;


  constructor(
    private authService: AuthService,
    private cdr: ChangeDetectorRef
  ) {
  }
  ngOnInit(): void {
    // Subscribe to changes
    this.authService.isLoggedIn$.subscribe((loggedIn) => {
    this.isLoggedIn = this.authService.isAuthenticated();
    this.role = this.authService.getRole();
    this.userId = this.authService.getLoggedUserId();
    this.userName = this.authService.getUserName();
    this.isLoggedIn = loggedIn;
    this.role = this.authService.getRole();
    this.cdr.detectChanges();
    });
  }

  logout() {
    this.authService.logout();
  }
  

 
}
