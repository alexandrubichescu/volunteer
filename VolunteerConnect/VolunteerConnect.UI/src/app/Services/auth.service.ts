import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, tap } from 'rxjs';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private baseUrl = 'https://localhost:7081/api/Account';
  private loginApiUrl = 'https://localhost:7081/api/Account/login';
  private logoutApiUrl = 'https://localhost:7081/api/Account/logout';
  private participationUrl ='https://localhost:7081/api/ParticipationOrder/user';

  private tokenKey = 'authToken'; // Key to store the token in localStorage
  private roleKey = 'userRole'; // Key to store the role in localStorage
  private userIdKey = 'userId'; //id to store
  private userNameKey = 'userName'; //id to store
  private isLoggedInSubject = new BehaviorSubject<boolean>(
    !!localStorage.getItem(this.tokenKey) // Initialize based on token presence
  );
  isLoggedIn$ = this.isLoggedInSubject.asObservable(); // Observable for login state
  private tokenExpirationTimer: any;
  private activityTimeout: any; // Timer for tracking inactivity
  private userNameSubject = new BehaviorSubject<string | null>(
    this.getUserName()
  );
  userName$ = this.userNameSubject.asObservable();

  constructor(private http: HttpClient) {
    const storedUserName = this.getUserName();
    if (storedUserName) {
      this.userNameSubject.next(storedUserName);
    }
  }

  // Login method
  login(credentials: { email: string; password: string }): Observable<any> {
    const loginDto = {
      loginRequestDto: {
        email: credentials.email,
        password: credentials.password,
      },
    };
    return this.http.post<any>(this.loginApiUrl, loginDto).pipe(
      tap((response) => {
        if (response.token) {
          this.setTokenAndRoleAndId(
            response.token,
            response.role,
            response.userId,
            response.userName
          ); // Save token and role and Id
          this.startInactivityTimer(); // Start inactivity timer on login
        }
      })
    );
  }
  

  getAllUsers(): Observable<any[]> {
    const token = this.getToken(); // Get the token for authorization
    const headers = new HttpHeaders({
      Authorization: `Bearer ${token}`,
    });
    const apiUrl = this.baseUrl+'/all';
    var usersResult = this.http.get<any[]>(apiUrl, { headers });
    return usersResult;
  }

  getUserById(userId: string|null=null): Observable<any[]> {
    const token = this.getToken(); // Get the token for authorization
    const headers = new HttpHeaders({
      Authorization: `Bearer ${token}`,
    });
    const apiUrl = this.baseUrl+'/'+userId;
    var userCall = this.http.get<any[]>(apiUrl, { headers });
    return userCall;
  }

  // Get role
  getRole(): string | null {
    var role = localStorage.getItem(this.roleKey);
    return role;
  }
  // Get role
  getLoggedUserId(): string | null {
    var userId = localStorage.getItem(this.userIdKey);
    return userId;
  }
  // Get token
  getToken(): string | null {
    return localStorage.getItem(this.tokenKey);
  }
  getUserName(): string | null {
    return localStorage.getItem(this.userNameKey);
  }

  clearTokenAndRole(): void {
    localStorage.removeItem('authToken');
    localStorage.removeItem('userRole');
    localStorage.removeItem('userId');
    localStorage.removeItem('userName');

    this.isLoggedInSubject.next(false);
  }

  // Check if the user is authenticated
  isAuthenticated(): boolean {
    const token = this.getToken();
    return !!token;
  }
  // Set token and role after login
  setTokenAndRoleAndId(
    token: string,
    role: string,
    id: string,
    userName: string
  ): void {
    localStorage.setItem(this.tokenKey, token);
    localStorage.setItem(this.roleKey, role);
    localStorage.setItem(this.userIdKey, id);
    localStorage.setItem(this.userNameKey, userName);

    this.userNameSubject.next(userName); // Notify subscribers of userName updates
    this.isLoggedInSubject.next(true); // Update login state
  }

  // Logout
  logout() {
    console.log('User logged out.');
    this.clearTokenAndRole();
    this.clearTimers(); // Clear all timers
    return this.http.post<any>(this.logoutApiUrl, {}).pipe(
      tap(() => {
        console.log('Logout successful.');
      })
    );
  }
  /*
   * Setează un timer pentru a face logout automat după ce token-ul expiră.
   * Token-ul expiră pe FE în 10 de minute de la inactivitate
   */
  autoLogout(expirationDuration: number) {
    console.log('Expiration duration: ', expirationDuration);
    this.tokenExpirationTimer = setTimeout(() => {
      this.logout();
    }, expirationDuration);
  }
  private clearTimers() {
    console.log('Clearing timers...');
    if (this.tokenExpirationTimer) clearTimeout(this.tokenExpirationTimer);
    if (this.activityTimeout) clearTimeout(this.activityTimeout);
  }

  private resetInactivityTimer() {
    if (this.activityTimeout) clearTimeout(this.activityTimeout);
    this.activityTimeout = setTimeout(() => {
      console.log('Auto logout due to inactivity');
      this.logout();
    }, 10 * 60 * 1000); // 10 minutes
  }

  startInactivityTimer() {
    console.log('Starting inactivity timer...');
    this.resetInactivityTimer(); // Initialize inactivity timer
    ['mousemove', 'keydown', 'click'].forEach((event) =>
      window.addEventListener(event, () => this.resetInactivityTimer())
    );
  }
}
