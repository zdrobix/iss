import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environment/environment';
import { AddPharmacyRequest } from '../../../models/add-pharmacy-request.model';
import { Pharmacy } from 'src/app/features/models/pharmacy.model';
import { UpdateDrugStorageRequest } from 'src/app/features/models/update-drogstorage-request.model';
import { DrugStorage } from 'src/app/features/models/drug-storage.model';
import { StoredDrug } from 'src/app/features/models/stored-drug.mode';
import { AddStoredDrugRequest } from 'src/app/features/models/add-storeddrug-request.model';

@Injectable({
  providedIn: 'root'
})
export class PharmaciesService {
  constructor(private http: HttpClient) { }

  addPharmacy(request: AddPharmacyRequest): Observable<Pharmacy> {
    return this.http.post<Pharmacy>(`${environment.apiBaseUrl}/api/pharmacy`, request);
  }

  getPharmacies(): Observable<Pharmacy[]> {
    return this.http.get<Pharmacy[]>(`${environment.apiBaseUrl}/api/pharmacy`);
  }

  getPharmacy(id: number): Observable<Pharmacy> {
    return this.http.get<Pharmacy>(`${environment.apiBaseUrl}/api/pharmacy/${id}`);
  }

  updateDrugStorage(id: number, request: UpdateDrugStorageRequest): Observable<DrugStorage> {
    return this.http.put<DrugStorage>(`${environment.apiBaseUrl}/api/drugstorage/${id}`, request);
  }

  addUpdateStoredDrug(request: AddStoredDrugRequest): Observable<StoredDrug> {
    return this.http.post<StoredDrug>(`${environment.apiBaseUrl}/api/storeddrug`, request);
  }

  getStoredDrug(drugId: number, pharmacyId: number): Observable<StoredDrug> {
    return this.http.get<StoredDrug>(`${environment.apiBaseUrl}/api/storeddrug/${drugId}/stored/${pharmacyId}`);
  }
}
