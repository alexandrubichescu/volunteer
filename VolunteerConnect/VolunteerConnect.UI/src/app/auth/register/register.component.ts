import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { RegisterService } from '../../Services/register.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [ReactiveFormsModule, CommonModule],
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css'],
  providers: [RegisterService],
})
export class RegisterComponent {
  registerForm: FormGroup;
  isSubmitting = false;
  message = '';
  countdownMessage: string | undefined;

  constructor(private fb: FormBuilder, private router: Router,private registerService: RegisterService) {
    this.registerForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6)]],
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
    });
  }

  onSubmit() {
    if (this.registerForm.valid) {
      this.isSubmitting = true;
      this.registerService.register(this.registerForm.value).subscribe({
        next: (response) => {
          if (response.success) {
            this.message = response.message;
            // Initialize the countdown 
            let countdown = 3; // Success message
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
          }, 3000);
        },
        error: (err) => {
          if (err.status === 400 && err.error?.errors) {
            // Handle validation errors returned by the backend
            this.message = err.error.errors.join(', ');
          } 
                   
          else {
            // Generic error message for unexpected cases
            this.message = 'An unexpected error occurred.';
          }
          this.isSubmitting = false;
        },
      });
    }
  }
  
  
}
