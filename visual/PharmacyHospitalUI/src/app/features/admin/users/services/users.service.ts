import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs';
import { Observable } from 'rxjs/internal/Observable';
import { AddUserRequest } from 'src/app/features/models/add-user-request.model';
import { UpdateUserRequest } from 'src/app/features/models/update-user-request.model';
import { User } from 'src/app/features/models/user.model';
import { environment } from 'src/environment/environment';

@Injectable({
  providedIn: 'root'
})

export class UsersService {


  constructor(private http: HttpClient) { }

  addUser(user: AddUserRequest): Observable<void> {
    return this.http.post<void>(`${environment.apiBaseUrl}/api/user`, user);
  }

  deleteUser(id: number): Observable<void> {
    return this.http.delete<void>(`${environment.apiBaseUrl}/api/user/${id}`);
  }

  getUsers(): Observable<User[]> {
    return this.http.get<User[]>(`${environment.apiBaseUrl}/api/user`);
  }

  getUser(id: number): Observable<User> {
    return this.http.get<User>(`${environment.apiBaseUrl}/api/user/${id}`);
  }

  updateUser(id: number, request: UpdateUserRequest): Observable<User> {
    return this.http.put<User>(`${environment.apiBaseUrl}/api/user/${id}`, request);
  }
}
