import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root',
})
export class ParticipationOrderService {
  private baseUrl = 'https://localhost:7081/api/ParticipationOrder';

  constructor(private http: HttpClient,private authService: AuthService) {}

 getAllParticipationOrders(): Observable<any> {
     const token = this.authService.getToken();
       const headers = new HttpHeaders({
         Authorization: `Bearer ${token}`,
       });
     var urlParticipationOrdersAll=this.baseUrl+'/all';
    var result= this.http.get(urlParticipationOrdersAll,{ headers });
     return result;
   }



   getParticipationOrdersByUserId(userId:string|null=null): Observable<any[]> {
    const token = this.authService.getToken(); // Retrieve the token from AuthService
    const headers = new HttpHeaders({
      Authorization: `Bearer ${token}`, // Add the Bearer token
    });
    var result= this.http.get<any[]>(`${this.baseUrl}/user/${userId}`, { headers });
    return result;
  }



  // Participate in an event
  participateInEvent(userId: string | null = null, eventId: string): Observable<any> {
    const token = this.authService.getToken(); // Retrieve the token from AuthService
    const headers = new HttpHeaders({
      Authorization: `Bearer ${token}`, // Add the Bearer token
    });
    const body = { userId, eventId }; // Request body
    var result= this.http.post(this.baseUrl, body, { headers });
    return result;
  }

  updateParticipationOrder(participationOrderData: any): Observable<any> {
    const token = this.authService.getToken();
    const headers = new HttpHeaders({
      Authorization: `Bearer ${token}`,
    });
    const updatedParticipationOrderData = participationOrderData;
    var result= this.http.put(this.baseUrl, updatedParticipationOrderData, { headers });
    return result;
  }
  deleteParticipationOrder(participationOrderId: string): Observable<any> {
    const token = this.authService.getToken();
    const headers = new HttpHeaders({
      Authorization: `Bearer ${token}`,
    });
    var url=this.baseUrl+'/'+participationOrderId;
    var result= this.http.delete(url, { headers });
    return result;
  }


}