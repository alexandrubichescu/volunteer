import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable()
export class RegisterService {
  private apiUrl = 'https://localhost:7081/api/Account/register';

  constructor(private http: HttpClient) {}

  register(formData: { email: string; password: string; firstName: string; lastName: string }): Observable<any> {
    const dto = {
      registerRequestDto: {
        email: formData.email,
        password: formData.password,
        firstName: formData.firstName,
        lastName: formData.lastName,
      },
    };

    return this.http.post<any>(this.apiUrl, dto); // Expect JSON response, including errors
  }
}
