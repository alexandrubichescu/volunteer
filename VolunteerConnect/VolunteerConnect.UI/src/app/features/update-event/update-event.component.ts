import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { EventsService } from '../../Services/events.service';
import { CategoryService } from '../../Services/category.service';
import { CommonModule } from '@angular/common';
import { AuthService } from '../../Services/auth.service';

@Component({
  selector: 'app-update-event',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
  ],
  templateUrl: './update-event.component.html',
  styleUrls: ['./update-event.component.css'],
})
export class UpdateEventComponent implements OnInit {
  eventForm!: FormGroup; // FormGroup for the event
  eventId: string | null = null; // Event ID from route
  categories: any[] = []; // To populate the category dropdown
  message: string = ''; // Success message
  errorMessage: string = ''; // Error message
  isSubmitting = false; // Form submission state
  countdownMessage: string=''; // Countdown message
  userId: string | null = null; 

  constructor(
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private eventsService: EventsService,
    private categoryService: CategoryService,
    private authService: AuthService,
    private router: Router
  ) {}

  ngOnInit(): void {
    // Get Event ID from URL
    this.eventId = this.route.snapshot.paramMap.get('id');
    this.userId =  this.authService.getLoggedUserId();

    if (this.eventId) {
      this.loadEventDetails(this.eventId); // Load event data
    }

    // Initialize the form
    this.eventForm = this.fb.group({
      title: ['', [Validators.required, Validators.maxLength(50)]],
      companyHolder: [''],
      location: ['', [Validators.required, Validators.maxLength(50)]],
      date: ['', Validators.required],
      maxParticipants: [0],
      description: [''],
      imageUrl: [''],
      categoryId: ['', Validators.required],
    });

    // Load categories for dropdown
    this.loadCategories();
  }

  loadEventDetails(eventId: string): void {
    this.eventsService.getEventById(eventId).subscribe({
      next: (event) => {
        // Populate the form with the event data
        this.eventForm.patchValue({
          title: event.title,
          companyHolder: event.companyHolder,
          location: event.location,
          date: event.date,
          maxParticipants: event.maxParticipants,
          description: event.description,
          imageUrl: event.imageUrl,
          categoryId: event.categoryId,
        });
      },
      error: (err) => {
        console.error('Failed to load event details:', err);
        this.errorMessage = 'Failed to load event details.';
      },
    });
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
    if (this.eventForm.valid && this.eventId) {
      const updatedEventData = {
        ...this.eventForm.value,
        eventId: this.eventId, // Include the event ID in the update
      };
      //const updatedEventData = Object.assign({}, this.eventForm.value, { eventId: this.eventId });


      this.isSubmitting = true;
      this.eventsService.updateEvent(this.eventId, updatedEventData).subscribe({
        next: () => {
            this.message = 'Event updated successfully.';
            // Initialize the countdown 
            let countdown = 3; // Success message
            this.countdownMessage = `Redirecting to dashboard page in ${countdown} seconds...`;
            // Update the countdown every second
            const interval = setInterval(() => {
            countdown--;
            this.countdownMessage = `Redirecting to dashboard page in ${countdown} seconds...`;
            }, 1000);
            // Delay the navigation by 2 seconds
            setTimeout(() => {
              console.log('Navigating to:', '/admindashboard');
              this.goBack();  // Navigate back to the admin dashboard
            }, 3000);
        },
        error: (err) => {
          console.error('Failed to update event:', err);
          this.errorMessage = 'Failed to update event. Please try again.';
        },
        complete: () => {
          this.isSubmitting = true;
        },
      });
    } else {
      this.errorMessage = 'Please complete all required fields.';
    }
  }
  goBack(): void {
    this.router.navigate(['/admindashboard', this.userId]);// Navigate back to the admin dashboard
  }

  
  
}
