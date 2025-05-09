import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { DrugStorage } from '../../models/drug-storage.model';
import { environment } from 'src/environment/environment';

@Injectable({
  providedIn: 'root'
})

export class StorageService {

  constructor(private http: HttpClient) { }

  getStorageById(id: number): Observable<DrugStorage> {
    return this.http.get<DrugStorage>(`${environment.apiBaseUrl}/api/drugstorage/${id}`);
  }
}
