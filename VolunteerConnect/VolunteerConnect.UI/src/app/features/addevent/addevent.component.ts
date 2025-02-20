import { Component, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { EventsService } from '../../Services/events.service';
import { CategoryService } from '../../Services/category.service';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { AuthService } from '../../Services/auth.service';

@Component({
  selector: 'app-addevent',
  standalone: true,
  imports: [ReactiveFormsModule, CommonModule],
  templateUrl: './addevent.component.html',
  styleUrls: ['./addevent.component.css'],
})
export class AddEventComponent implements OnInit {
  eventForm: FormGroup;
  categories: any[] = [];
  isSubmitting = false;
  message = '';
  errorMessage = '';
  countdownMessage: string | undefined;
  userId: string | null = null;

  constructor(
    private fb: FormBuilder,
    private eventsService: EventsService,
    private categoryService: CategoryService,
    private authService: AuthService,

    private router: Router
  ) {
    this.userId = this.authService.getLoggedUserId();
    this.eventForm = this.fb.group({
      title: ['', [Validators.required, Validators.maxLength(50)]],
      companyHolder: ['', Validators.required],
      location: ['', [Validators.required, Validators.maxLength(50)]],
      date: ['', Validators.required],
      maxParticipants: [0, Validators.required],
      description: ['', Validators.required],
      imageUrl: ['', Validators.required],
      categoryId: ['', Validators.required],
    });
  }

  ngOnInit(): void {
    this.loadCategories();
  }

  loadCategories(): void {
    this.categoryService.getAllCategories().subscribe({
      next: (data) => {
        this.categories = data;
      },
      error: (err) => {
        console.error('Failed to load categories:', err);
      },
    });
  }

  onSubmit(): void {
    if (this.eventForm.valid) {
      this.isSubmitting = true;

      this.eventsService.createEvent(this.eventForm.value).subscribe({
        next: (response) => {
          // Handle success
          this.message = 'Event created successfully!';
          console.log('Event created successfully!');

          // Redirect after success
          let countdown = 3;
          this.countdownMessage = `Redirecting to dashboard page in ${countdown} seconds...`;
          const interval = setInterval(() => {
            countdown--;
            this.countdownMessage = `Redirecting to dashboard page in ${countdown} seconds...`;
          }, 1000);

          setTimeout(() => {
            this.router.navigate(['/admindashboard', this.userId]);// Navigate back to the admin dashboard
          }, 3000);
        },
        error: (err) => {
          this.isSubmitting = false;

          if (err.status === 400 && err.error) {
            // Handle FluentValidation errors
            this.errorMessage = err.error.join(', ');
          } else {
            // Handle other errors
            this.errorMessage = 'Failed to create event. Please try again.';
          }

          console.error('Failed to create event:', err);
        },
      });
    } else {
      this.errorMessage = 'Please complete all required fields.';
    }
  }
}
