import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environment/environment';
import { AddPharmacyRequest } from '../../../models/add-pharmacy-request.model';
import { Pharmacy } from 'src/app/features/models/pharmacy.model';
import { UpdatePharmacyRequest } from 'src/app/features/models/update-pharmacy-request.model';

@Injectable({
  providedIn: 'root'
})
export class PharmaciesService {
  constructor(private http: HttpClient) { }

  addPharmacy(request: AddPharmacyRequest) : Observable<Pharmacy> {
    return this.http.post<Pharmacy>(`${environment.apiBaseUrl}/api/pharmacy`, request);
  }

  getPharmacies(): Observable<Pharmacy[]> {
    return this.http.get<Pharmacy[]>(`${environment.apiBaseUrl}/api/pharmacy`);
  }

  getPharmacy(id: number): Observable<Pharmacy> {
    return this.http.get<Pharmacy>(`${environment.apiBaseUrl}/api/pharmacy/${id}`);
  }

  updatePharmacy(id: number, request: UpdatePharmacyRequest): Observable<Pharmacy> {
    return this.http.put<Pharmacy>(`${environment.apiBaseUrl}/api/pharmacy/${id}`, request);
  }
}
