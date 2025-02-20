import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root',
})
export class CategoryService {
  private baseUrl = 'https://localhost:7081/api/Category'; 

  constructor(private http: HttpClient, private authService: AuthService) {}

  // Fetch all categories
  getAllCategories(): Observable<any> {
    var categories= this.http.get(`${this.baseUrl}/all`);
    return categories;
  }

  // Add a new category (Admin only)
  createCategory(title: string, description: string, imageUrl:string): Observable<any> {
    const token = this.authService.getToken(); // Retrieve the token from AuthService
    const headers = new HttpHeaders({
      Authorization: `Bearer ${token}`, // Add the Bearer token
    });

    const body = { title, description, imageUrl }; // Request body

    return this.http.post(this.baseUrl, body, { headers });
  }
}