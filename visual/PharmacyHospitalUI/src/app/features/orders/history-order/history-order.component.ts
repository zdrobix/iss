import { Component, OnDestroy, OnInit } from '@angular/core';
import { Drug } from '../../models/drug.model';
import { StoredDrug } from '../../models/stored-drug.mode';
import { DrugStorage } from '../../models/drug-storage.model';
import { map, Observable, Subscription, take } from 'rxjs';
import { Order } from '../../models/order.model';
import { User } from '../../models/user.model';
import { OrdersService } from '../services/orders.service';
import { LoginService } from '../../account/services/login.service';

@Component({
  selector: 'app-history-order',
  templateUrl: './history-order.component.html',
  styleUrls: ['./history-order.component.css']
})
export class HistoryOrderComponent implements OnInit, OnDestroy{
  storageId?: number;
  loggedInUser?: User;
  orderIsValid?: boolean;
  orders$?: Observable<Order[]>;
  selectedOrder?: Order;
  selectedOrder$?: Observable<Order>;
  getLoggedInUserSubscription?: Subscription;

  constructor(private ordersService: OrdersService, private loginsService: LoginService) {}

  selectOrder(order: Order) {
    this.selectedOrder$= this.ordersService.getOrder(order.id);
  }

  ngOnInit(): void {
    this.getLoggedInUserSubscription = this.loginsService.getLoggedInUser().pipe(take(1)).subscribe((user: User | null) => {
      if (user) {
        this.loggedInUser = user;
        if (user.pharmacy) {
          this.storageId = user.pharmacy.storage.id;
        }
    }});
    if (this.loggedInUser?.role === "PHARMACY STAFF") {
      this.orders$ = this.ordersService.getResolvedOrders().pipe(
        map(orders => orders.filter(order => order.resolvedBy?.id === this.loggedInUser?.id))
      );
    } else if (this.loggedInUser?.role === "HOSPITAL STAFF") {
      this.orders$ = this.ordersService.getResolvedOrders().pipe(
        map(orders => orders.filter(order => order.placedBy?.id === this.loggedInUser?.id))
      );
    }
  }

  ngOnDestroy(): void {
    this.getLoggedInUserSubscription?.unsubscribe();
  }

  getMillisecondsFromDate(dateString: string): string {
    const date = new Date(dateString);
    return date.getMilliseconds().toString().padStart(3, '0');
  }
}
