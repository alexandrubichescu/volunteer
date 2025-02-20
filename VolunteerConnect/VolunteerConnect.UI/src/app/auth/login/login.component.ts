import { Component } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../../Services/auth.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [ReactiveFormsModule, CommonModule],
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent {
  loginForm: FormGroup;
  message = '';
  isSubmitting = false;
  countdownMessage: string | undefined;

  constructor(
    private fb: FormBuilder,
    private loginService: AuthService,
    private router: Router
  ) {
    this.loginForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', Validators.required],
    });
  }
  
  onSubmit() {
    if (this.loginForm.valid) {
      this.isSubmitting = true;
      this.loginService.login(this.loginForm.value).subscribe({
        next: (response) => {
          //this.loginService.setToken(response.token); // Save the token
          this.loginService.setTokenAndRoleAndId(response.token, response.role, response.userId, response.userName); // Save token and role

          if (response.success) {
            this.message = response.message; // Success message
            // Initialize the countdown
            let countdown = 3;
            this.countdownMessage = `Redirecting to home page in ${countdown} seconds...`;
            // Update the countdown every second
            const interval = setInterval(() => {
              countdown--;
              this.countdownMessage = `Redirecting to home page in ${countdown} seconds...`;
            }, 1000);
          } else {
            this.message = response.errors.join(', '); // Error messages
          }
          // Delay the navigation by 2 seconds
          setTimeout(() => {
            this.router.navigate(['/']); // Redirect to home page
          }, 3200);
        },
        error: (err) => {
          if (err.status === 401 && err.error?.errors) {
            // Handle validation errors returned by the backend
            this.message = err.error.errors.join(', ');
          } else {
            // Generic error message for unexpected cases
            this.message = 'An unexpected error occurred.';
          }
          this.isSubmitting = false;
        },
      });
    }
  }
}
