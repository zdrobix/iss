import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AddHospitalRequest } from 'src/app/features/models/add-hospital-request.model';
import { Hospital } from 'src/app/features/models/hospital.model';
import { UpdateHospitalRequest } from 'src/app/features/models/update-hospital-request.model';
import { environment } from 'src/environment/environment';

@Injectable({
  providedIn: 'root'
})
export class HospitalsService {
  
  constructor(private http: HttpClient) { }
  
  getHospitals(): Observable<Hospital[]> {
    return this.http.get<Hospital[]>(`${environment.apiBaseUrl}/api/hospital`);
  }
  
  addHospital(request: AddHospitalRequest) : Observable<Hospital> {
    return this.http.post<Hospital>(`${environment.apiBaseUrl}/api/hospital`, request);
  }
  
  getHospital(id: number) : Observable<Hospital> {
    return this.http.get<Hospital>(`${environment.apiBaseUrl}/api/hospital/${id}`);
  }

  updateHospital(id: number, request: UpdateHospitalRequest) : Observable<Hospital> {
    return this.http.put<Hospital>(`${environment.apiBaseUrl}/api/hospital/${id}`, request);
  }
}
