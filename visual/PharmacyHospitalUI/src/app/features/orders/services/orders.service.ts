import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AddOrderRequest } from '../../models/add-order-request.model';
import { Observable } from 'rxjs';
import { Order } from '../../models/order.model';
import { environment } from 'src/environment/environment';
import { NumberSymbol } from '@angular/common';
import { UpdateOrderRequest } from '../../models/update-order-request.model';

@Injectable({
  providedIn: 'root'
})
export class OrdersService {

  constructor(private http: HttpClient) { }

  addOrder(request: AddOrderRequest) : Observable<Order> {
    console.log(request);
    return this.http.post<Order>(`${environment.apiBaseUrl}/api/order`, request);
  }

  getOrders() : Observable<Order[]> {
    return this.http.get<Order[]>(`${environment.apiBaseUrl}/api/order`);
  }

  getOrder(id: number) : Observable<Order> {
    return this.http.get<Order>(`${environment.apiBaseUrl}/api/order/${id}`)
  }

  updateOrder(id: number, request: UpdateOrderRequest) : Observable<Order> {
    return this.http.put<Order>(`${environment.apiBaseUrl}/api/order/${id}`, request)
  }
}
