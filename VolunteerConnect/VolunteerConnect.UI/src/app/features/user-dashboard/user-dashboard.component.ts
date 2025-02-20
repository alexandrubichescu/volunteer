import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../Services/auth.service';
import { CommonModule, DatePipe, NgFor, NgIf } from '@angular/common';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { ParticipationOrderService } from '../../Services/participationOrder.service';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { EventsService } from '../../Services/events.service';

@Component({
  selector: 'app-user-dashboard',
  standalone: true,
  imports: [DatePipe, NgIf, 
      NgFor,
      ReactiveFormsModule,
      FormsModule,
      CommonModule,
      RouterModule],
  templateUrl: './user-dashboard.component.html',
  styleUrls: ['./user-dashboard.component.css'],
})
export class UserDashboardComponent implements OnInit {
  userName: string | null = null; 
  userRole: string | null = null;
  userId: string | null = null;
  isLoggedIn: boolean = false;

  //totalParticipations: number = 0;
  participationsOrdersList: any[] = [];
  recommendedEvents: any[] = [];

  constructor(
    private route: ActivatedRoute,
    private participationOrderService: ParticipationOrderService,
    private authService: AuthService,
    private eventsService: EventsService,
    private router: Router
  ) {}

  ngOnInit(): void {
    // Load user role and participations
    
      this.userId =  this.authService.getLoggedUserId();
      this.userRole = this.authService.getRole();
      this.userName = this.authService.getUserName();
      if (this.userId) {
        this.getParticipationOrdersForAUser();
      }
    
  }


 getUserName(): void {
     this.userName=this.authService.getUserName();
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
              eventsData.map((event: any) => [
                event.eventId,
                { title: event.title, date: event.date },
              ])
            );
  
            // Step 3: Fetch participation orders for the logged-in user
            this.participationOrderService.getParticipationOrdersByUserId(this.userId).subscribe({
              next: (orders) => {
                console.log('Order EventIds:', orders.map((o: any) => o.eventId));

                // Step 4: Map participation orders with enriched data
                const participationOrders = orders.map((order: any) => {
                  const eventDetails =
                    eventMap.get(String(order.eventId)) || { title: 'Unknown Event', date: 'Unknown Date' }; // Convert order.eventId to string
  
                  return {
                    user: userInfo, // Use logged-in user's details
                    event: (eventDetails as { title: string, date: string }).title, // Map event title
                    eventDate: (eventDetails as { title: string, date: string }).date, // Map event date
                    categoryTitle: order.category?.title || 'Unknown Category', // Map category title
                    eventId: order.eventId, // Include eventId
                    status: order.status, // Include status
                    id: order.id, // Include participation order ID
                  };
                });
  
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
  deleteParticipationOrder(participationOrderId: string): void {
    if (confirm('Are you sure you want to delete this order?')) {
      this.participationOrderService.deleteParticipationOrder(participationOrderId).subscribe({
        next: () => {
          console.log(`Order with ID: ${participationOrderId} deleted successfully`);
          // Refresh the orders list
          this.getParticipationOrdersForAUser();
        },
        error: (err) => console.error('Failed to delete order:', err),
      });
    }
  }

  navigateToEventDetail(eventId: string): void {
    this.router.navigate(['/detailedevent', eventId]);
  }
  
}
