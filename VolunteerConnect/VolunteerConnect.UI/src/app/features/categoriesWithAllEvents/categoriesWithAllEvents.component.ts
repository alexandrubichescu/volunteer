import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { EventsService } from '../../Services/events.service';
import { NgFor, NgIf, DatePipe } from '@angular/common';

@Component({
  selector: 'app-events',
  standalone: true,
  imports: [NgIf, NgFor, DatePipe, RouterModule],
  templateUrl: './categoriesWithAllEvents.component.html',
  styleUrls: ['./categoriesWithAllEvents.component.css'],
})
export class CategoriesWithAllEventsComponent implements OnInit {
  categoryEventsList: any[] = [];
  categoryTitle: string = '';
  category: any = null;

  constructor(
    private route: ActivatedRoute,
    private eventsService: EventsService,
    private router: Router
  ) {}

  ngOnInit(): void {
    const categoryId = this.route.snapshot.paramMap.get('categoryId');
    if (categoryId) {
      this.loadEvents(categoryId);
    }
  }

  loadEvents(categoryId: string): void {
    this.eventsService.getAllCategoriesWithEvents().subscribe({
      next: (data) => {
        const category = data.find((cat: any) => cat.categoryId === categoryId);
        if (category) {
          this.categoryTitle = category.title;
          this.categoryEventsList = category.categoryEventsList;
        }
      },
      error: (err) => {
        console.error('Failed to load events:', err);
      },
    });
  }
  goBack(): void {
    this.router.navigate(['/home']);
  }
}
