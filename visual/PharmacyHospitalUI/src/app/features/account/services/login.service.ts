import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { LoginRequest } from '../../models/login-request.model';
import { BehaviorSubject, map, Observable, of, timer } from 'rxjs';
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

  login (model: LoginRequest) : Observable<User> {
    return this.http.post<User>(`${environment.apiBaseUrl}/api/user/login`, model);
  }

  logout(): Observable<void> {
    sessionStorage.removeItem('user');
    this.currentUserSubject.next(null);
    return timer(0).pipe(map(() => {}));
  }

  /*
  Returns the user stored in the browser's memory, otherwise null.
  */
  private getUserFromSession(): User | null {
    const userJson = sessionStorage.getItem('user');
    return userJson ? JSON.parse(userJson) : null;
  }
}
