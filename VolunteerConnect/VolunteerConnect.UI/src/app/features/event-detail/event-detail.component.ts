import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { EventsService } from '../../Services/events.service';
import { NgIf, DatePipe } from '@angular/common';
import { AuthService } from '../../Services/auth.service';
import { ParticipationOrderService } from '../../Services/participationOrder.service';

@Component({
  selector: 'app-event-detail',
  standalone: true,
  imports: [NgIf, DatePipe,RouterModule],
  templateUrl: './event-detail.component.html',
  styleUrls: ['./event-detail.component.css'],
})
export class EventDetailComponent implements OnInit {
  eventDetail: any = null;
  userId: string | null = null;
  alreadyParticipating: boolean = false; // To track if the user is already participating

  constructor(
    private route: ActivatedRoute,
    private eventsService: EventsService,
    private participationOrderService: ParticipationOrderService,
    private authService: AuthService,
    private router: Router
  ) {}

  ngOnInit(): void {
    const eventId = this.route.snapshot.paramMap.get('id');
    if (eventId) {
      this.userId = this.authService.getLoggedUserId();
      this.checkParticipationStatus(eventId);
      this.loadEventDetails(eventId);
    }
  }

  loadEventDetails(eventId: string): void {
    this.eventsService.getEventById(eventId).subscribe({
      next: (data) => {
        this.eventDetail = data;
      },
      error: (err) => {
        console.error('Failed to load event details:', err);
      },
    });
  }
  checkParticipationStatus(eventId: string): void {
    if (!this.userId) return;

    this.participationOrderService.getAllParticipationOrders().subscribe({
      next: (orders) => {
        this.alreadyParticipating = orders.some(
          (order: any) => order.userId === this.userId && order.eventId === eventId
        );
      },
      error: (err) => {
        console.error('Failed to fetch participation orders:', err);
      },
    });
  }

  participateOnEvent(): void {
    if (!this.userId || !this.eventDetail?.eventId) {
      console.error('User ID or Event ID is missing.');
      return;
    }

    this.participationOrderService.participateInEvent(this.userId, this.eventDetail.eventId).subscribe({
      next: () => {
        alert('Participation request submitted successfully!');
        this.alreadyParticipating = true; // Update UI after successful participation
      },
      error: (err) => console.error('Failed to submit participation:', err),
    });
  }
  
  goBack(): void {
    window.history.back();
  }
  //goBack(): void {
  //  this.router.navigate(['/categorieswithallevents', this.eventDetail?.categoryId]);
  //}
}
