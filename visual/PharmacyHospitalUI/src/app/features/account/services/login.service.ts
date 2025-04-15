import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { LoginRequest } from '../../models/login-request.model';
import { BehaviorSubject, Observable } from 'rxjs';
import { environment } from 'src/environment/environment';
import { UserDTO } from '../../models/user-dto.model';

@Injectable({
  providedIn: 'root'
})

export class LoginService {
  private currentUserSubject = new BehaviorSubject<UserDTO | null>(this.getUserFromSession());
  public user$: Observable<UserDTO | null> = this.currentUserSubject.asObservable();

  constructor(private http: HttpClient) { }

  setLoggedInUser(user: UserDTO) {
    sessionStorage.setItem('user', JSON.stringify(user));
    this.currentUserSubject.next(user);
  }

  getLoggedInUser(): Observable<UserDTO | null> {
    return this.user$;
  }

  login (model: LoginRequest) : Observable<UserDTO> {
    return this.http.post<UserDTO>(`${environment.apiBaseUrl}/api/user/login`, model);
  }

  logout(): void {
    sessionStorage.removeItem('user');
    this.currentUserSubject.next(null);
  }

  private getUserFromSession(): UserDTO | null {
    const userJson = sessionStorage.getItem('user');
    return userJson ? JSON.parse(userJson) : null;
  }
}
