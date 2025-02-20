import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root',
})
export class EventsService {
  private categoryBaseUrl = 'https://localhost:7081/api/Category'; 
  private eventsBaseUrl = 'https://localhost:7081/api/Events'; 

  constructor(private http: HttpClient, private authService: AuthService) {}

  // Fetch all categories with events
  getAllCategoriesWithEvents(): Observable<any> {
    const token = this.authService.getToken();
      const headers = new HttpHeaders({
        Authorization: `Bearer ${token}`,
      });
    var urlCategWithEvents=this.categoryBaseUrl+'/allwithevents';
   var result= this.http.get(urlCategWithEvents,{ headers });
    return result;
  }




  // Fetch all categories with events
   getAllEvents(): Observable<any> {
      const token = this.authService.getToken();
      const headers = new HttpHeaders({
        Authorization: `Bearer ${token}`,
      });
   var result= this.http.get(this.eventsBaseUrl,{ headers });
      return result;
    }

  // Fetch event by ID
  getEventById(eventId: string): Observable<any> {
    const token = this.authService.getToken();
    const headers = new HttpHeaders({
      Authorization: `Bearer ${token}`,
    });
    const fullUrl = `${this.eventsBaseUrl}/${eventId}`;
    return this.http.get(fullUrl, { headers });
  }

  // Create a new event
  createEvent(eventData: any): Observable<any> {
    const token = this.authService.getToken();
    const headers = new HttpHeaders({
      Authorization: `Bearer ${token}`,
    });
    var result= this.http.post(this.eventsBaseUrl, eventData, { headers });
    return result;
  }

  // Delete an event by ID
  deleteEvent(eventId: string): Observable<any> {
    const token = this.authService.getToken();
    const headers = new HttpHeaders({
      Authorization: `Bearer ${token}`,
    });
    const fullUrl = `${this.eventsBaseUrl}/${eventId}`;
    return this.http.delete(fullUrl, { headers });
  }
  updateEvent(eventId: string, eventData: any): Observable<any> {
    const token = this.authService.getToken();
    const headers = new HttpHeaders({
      Authorization: `Bearer ${token}`,
    });
    const updatedEventData = { ...eventData, eventId };
    var result= this.http.put(this.eventsBaseUrl, updatedEventData, { headers });
    return result;
  }
  
  
}
