import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { LoginRequest } from '../../models/login-request.model';
import { BehaviorSubject, map, Observable, of, tap, timer } from 'rxjs';
import { environment } from 'src/environment/environment';
import { User } from '../../models/user.model';

@Injectable({
  providedIn: 'root'
})

export class LoginService {
  private currentUserSubject = new BehaviorSubject<User | null>(this.getUserFromSession());
  public user$: Observable<User | null> = this.currentUserSubject.asObservable();

  constructor(private http: HttpClient) { }

  setLoggedInUser(user: User) {
    sessionStorage.setItem('user', JSON.stringify(user));
    this.currentUserSubject.next(user);
  }

  getLoggedInUser(): Observable<User | null> {
    return this.user$;
  }

  login (request: LoginRequest) : Observable<{token: string}> {
    return this.http.post<{token: string}>(`${environment.apiBaseUrl}/api/user/login`, request)
      .pipe(
        tap(response => {
          localStorage.setItem('jwt_token', response.token);
        })
      );
  }

  logout(): void {
    localStorage.removeItem('jwt_token');
  }

  /*
  Returns the user stored in the browser's memory, otherwise null.
  */
  private getUserFromSession(): User | null {
    const userJson = sessionStorage.getItem('user');
    return userJson ? JSON.parse(userJson) : null;
  }
}
