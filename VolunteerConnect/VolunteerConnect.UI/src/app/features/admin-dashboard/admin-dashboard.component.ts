import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { AuthService } from '../../Services/auth.service';
import { EventsService } from '../../Services/events.service';
import { CategoryService } from '../../Services/category.service';
import { CommonModule, DatePipe, NgFor, NgIf } from '@angular/common';
import { Router, RouterModule } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ParticipationOrderService } from '../../Services/participationOrder.service';

@Component({
  selector: 'app-admin-dashboard',
  standalone: true,
  imports: [
    NgIf,
    NgFor,
    ReactiveFormsModule,
    FormsModule,
    CommonModule,
    RouterModule,
  ],
  templateUrl: './admin-dashboard.component.html',
  styleUrls: ['./admin-dashboard.component.css'],
})
export class AdminDashboardComponent implements OnInit {
  userName: string | null = null;
  userRole: string | null = null;
  userId: string | null = null;
  isLoggedIn: boolean = false;

  participationsOrdersList: any[] = [];
  allParticipationsOrdersList: any[] = [];
  eventsList: any[] = [];

  // Modal-related fields
  isAddCategoryModalVisible = false;
  newCategoryTitle = '';
  newDescription = '';
  newImageUrl = '';
  errorMessage: string | null = null;

  constructor(
    private authService: AuthService,
    private eventsService: EventsService,
    private categoryService: CategoryService,
    private participationOrderService: ParticipationOrderService,
    private router: Router,
    private cdr: ChangeDetectorRef
  ) {}

  ngOnInit(): void {
    this.isLoggedIn = this.authService.isAuthenticated();
    this.userId = this.authService.getLoggedUserId();
    this.userRole = this.authService.getRole();
    this.userName = this.authService.getUserName();

    if (this.userRole === 'Admin') {
      this.getAllEvents(); // Load all events for admin
      this.getParticipationOrdersForAUser();
      this.getAllParticipationOrders(); // Load all participation orders
    }
    this.cdr.detectChanges();
  }

  getAllEvents(): void {
    this.eventsService.getAllEvents().subscribe({
      next: (data) => {
        this.eventsList = data;
        this.eventsList = data;
        console.log('Events:', this.eventsList);
      },
      error: (err: any) => console.error('Failed to load events:', err),
    });
  }
  getParticipationOrdersForAUser(): void {
    // Step 1: Fetch user details for the logged-in user
    this.authService.getUserById(this.userId).subscribe({
      next: (user: any) => {
        const userInfo = { id: user.id, firstName: user.firstName, lastName: user.lastName ,  username:user.username};
  
        // Step 2: Fetch all events and cache by eventId
        this.eventsService.getAllEvents().subscribe({
          next: (eventsData) => {
            const eventMap = new Map(
              eventsData.map((event: any) => [event.eventId, event.title])
            );
  
            // Step 3: Fetch participation orders for the logged-in user
            this.participationOrderService.getParticipationOrdersByUserId(this.userId).subscribe({
              next: (orders) => {
                // Step 4: Map participation orders with enriched data
                const participationOrders = orders.map((order: any) => ({
                  user: userInfo, // Use logged-in user's details
                  event: eventMap.get(order.eventId) || 'Unknown Event', // Map event title
                  categoryTitle: order.category?.title || 'Unknown Category', // Map category title
                  eventId: order.eventId, // Include eventId
                  status: order.status, // Include status
                  id: order.id, // Include participation order ID
                }));
  
                // Step 5: Sort participation orders by status
                this.participationsOrdersList = participationOrders.sort((a, b) => a.status - b.status);
  
                console.log(
                  'Sorted Participation Orders with Event Titles and User Details:',
                  this.participationsOrdersList
                );
              },
              error: (err) =>
                console.error('Failed to fetch participation orders:', err),
            });
          },
          error: (err) => console.error('Failed to load events:', err),
        });
      },
      error: (err) => console.error('Failed to fetch user details:', err),
    });
  }
  
  
  getAllParticipationOrders(): void {
    // Fetch categories with events
    this.eventsService.getAllCategoriesWithEvents().subscribe({
      next: (categoriesData) => {
        // Create a map for eventId -> categoryTitle
        const eventToCategoryMap = new Map();
        const eventTitleMap = new Map();
  
        categoriesData.forEach((category: any) => {
          category.categoryEventsList.forEach((event: any) => {
            eventToCategoryMap.set(event.eventId, category.title);
            eventTitleMap.set(event.eventId, event.title);
          });
        });
  
        // Fetch users
        this.authService.getAllUsers().subscribe({
          next: (users) => {
            // Create a map for userId -> user
            const userMap = new Map(users.map((user: any) => [user.id, user]));
  
            // Fetch participation orders
            this.participationOrderService.getAllParticipationOrders().subscribe({
              next: (participationOrders) => {
                // Map participation orders with users, events, and categories
                this.allParticipationsOrdersList = participationOrders.map((order: any) => ({
                  user: userMap.get(order.userId) || { firstName: 'Unknown', lastName: 'User' },
                  event: eventTitleMap.get(order.eventId) || 'Unknown Event',
                  categoryTitle: eventToCategoryMap.get(order.eventId) || 'Unknown Category',
                  eventId: order.eventId, // Include eventId in the list
                  status: order.status,
                  id: order.id, // Participation order ID
                }));
  
                // Sort by status (pending first)
                  this.allParticipationsOrdersList.sort((a, b) => a.status - b.status);
                console.log('Mapped Participation Orders:', this.allParticipationsOrdersList);
              },
              error: (err) => console.error('Failed to fetch participation orders:', err),
            });
          },
          error: (err) => console.error('Failed to fetch users:', err),
        });
      },
      error: (err) => console.error('Failed to fetch categories with events:', err),
    });
  }
  

  getStatusLabel(status: number): string {
    switch (status) {
      case 0:
        return 'Pending';
      case 1:
        return 'Approved';
      case 2:
        return 'Declined';
      default:
        return 'Unknown';
    }
  }

  updateParticipationStatus(orderId: string, userId: string, eventId: string, status: number): void {
    console.log(`Updating order ${orderId} to status: ${status}`);
    this.participationOrderService.updateParticipationOrder({
      id: orderId,
      userId,
      eventId,
      status,
    }).subscribe({
      next: () => {
        console.log('Participation status updated successfully.');
        this.getParticipationOrdersForAUser();
      this.getAllParticipationOrders();
      },
      error: (err) => console.error('Failed to update participation status:', err),
    });
  }

  addEvent(): void {
    console.log('Redirecting to add new event page...');
    this.router.navigate(['/addevent']);
    // Redirect to Add Event component
  }

  updateEvent(eventId: string): void {
    this.router.navigate(['/update-event', eventId]);
  }

  deleteEvent(eventId: string): void {
    if (confirm('Are you sure you want to delete this event?')) {
      this.eventsService.deleteEvent(eventId).subscribe({
        next: () => {
          console.log(`Event with ID: ${eventId} deleted successfully`);
          this.getAllEvents(); // Refresh the events list
          this.getParticipationOrdersForAUser();
          this.getAllParticipationOrders();
        },
        error: (err) => console.error('Failed to delete event:', err),
      });
    }
  }
  deleteParticipationOrder(participationOrderId: string): void {
    if (confirm('Are you sure you want to delete this order?')) {
      this.participationOrderService.deleteParticipationOrder(participationOrderId).subscribe({
        next: () => {
          console.log(`Order with ID: ${participationOrderId} deleted successfully`);
          // Refresh the orders list
          this.getParticipationOrdersForAUser();
          this.getAllParticipationOrders();
        },
        error: (err) => console.error('Failed to delete order:', err),
      });
    }
  }


  showAddCategoryModal(): void {
    this.isAddCategoryModalVisible = true;
  }

  closeAddCategoryModal(): void {
    this.isAddCategoryModalVisible = false;
  }

  addNewCategory(): void {
    if (
      !this.newCategoryTitle.trim() ||
      !this.newDescription.trim() ||
      !this.newImageUrl.trim()
    ) {
      this.errorMessage = 'All fields are required.';
      return;
    }

    this.categoryService
      .createCategory(
        this.newCategoryTitle,
        this.newDescription,
        this.newImageUrl
      )
      .subscribe({
        next: (response) => {
          if (response.success) {
            console.log('Category added successfully.');
            this.closeAddCategoryModal();
            this.newCategoryTitle = '';
            this.newDescription = '';
            this.newImageUrl = '';
          } else {
            this.errorMessage = response.validationErrors.join(', ');
          }
        },
        error: (err) => {
          console.error('Failed to add category:', err);
          this.errorMessage = 'An error occurred while adding the category.';
        },
      });
  }
}
