import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AddDrugRequest } from '../../../models/add-drug-request.model';
import { Observable } from 'rxjs';
import { environment } from 'src/environment/environment';
import { Drug } from '../../../models/drug.model';

@Injectable({
  providedIn: 'root'
})
export class DrugsService {

  constructor(private http: HttpClient) { }

  addDrug (drug: AddDrugRequest): Observable<void> {
    return this.http.post<void>(`${environment.apiBaseUrl}/api/drug`, drug);
  }

  getDrugs () : Observable<Drug[]> {
    return this.http.get<Drug[]>(`${environment.apiBaseUrl}/api/drug`);
  }
}
