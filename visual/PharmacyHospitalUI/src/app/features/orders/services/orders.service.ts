import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AddOrderRequest } from '../../models/add-order-request.model';
import { Observable } from 'rxjs';
import { Order } from '../../models/order.model';
import { environment } from 'src/environment/environment';
import { UpdateOrderRequest } from '../../models/update-order-request.model';
import { DrugStorage } from '../../models/drug-storage.model';
import { StoredDrug } from '../../models/stored-drug.mode';
import { AddStoredDrugRequest } from '../../models/add-storeddrug-request.model';

@Injectable({
  providedIn: 'root'
})

export class OrdersService {

  constructor(private http: HttpClient) { }

  addOrder(request: AddOrderRequest): Observable<Order> {
    console.log(request);
    return this.http.post<Order>(`${environment.apiBaseUrl}/api/order`, request);
  }

  getOrders(): Observable<Order[]> {
    return this.http.get<Order[]>(`${environment.apiBaseUrl}/api/order`);
  }

  getUnresolvedOrders(): Observable<Order[]> {
    return this.http.get<Order[]>(`${environment.apiBaseUrl}/api/order/unresolved`);
  }

  getResolvedOrders(): Observable<Order[]> {
    return this.http.get<Order[]>(`${environment.apiBaseUrl}/api/order/resolved`);
  }

  getOrder(id: number): Observable<Order> {
    return this.http.get<Order>(`${environment.apiBaseUrl}/api/order/${id}`)
  }

  updateOrder(id: number, request: UpdateOrderRequest): Observable<Order> {
    return this.http.put<Order>(`${environment.apiBaseUrl}/api/order/${id}`, request)
  }

  getDrugStorage(id: number): Observable<DrugStorage> {
    return this.http.get<DrugStorage>(`${environment.apiBaseUrl}/api/drugstorage/${id}`);
  }

  getStoredDrug(drugId: number, storageId: number): Observable<StoredDrug> {
    return this.http.get<StoredDrug>(`${environment.apiBaseUrl}/api/storeddrug/${drugId}/stored/${storageId}`);
  }

  updateStoredDrug(request: AddStoredDrugRequest): Observable<StoredDrug> {
    console.log(request);
    return this.http.post<StoredDrug>(`${environment.apiBaseUrl}/api/storeddrug`, request);
  }
}
