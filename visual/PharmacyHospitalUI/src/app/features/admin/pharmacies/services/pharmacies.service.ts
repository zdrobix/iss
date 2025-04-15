import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Pharmacy } from '../../../models/pharmacy.model';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environment/environment';
import { AddPharmacyRequest } from '../../../models/add-pharmacy-request.model';

@Injectable({
  providedIn: 'root'
})
export class PharmaciesService {
  constructor(private http: HttpClient) { }

  addPharmacy(model: AddPharmacyRequest) : Observable<void> {
    return this.http.put<void>(`${environment.apiBaseUrl}/api/pharmacy`, model);
  }

  getPharmacies(): Observable<Pharmacy[]> {
    return this.http.get<Pharmacy[]>(`${environment.apiBaseUrl}/api/pharmacies`);
  }
}
