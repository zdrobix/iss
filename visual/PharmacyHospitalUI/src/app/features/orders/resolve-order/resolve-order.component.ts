import { Component, OnDestroy, OnInit } from '@angular/core';
import { catchError, map, Observable, of, Subscription, take, tap } from 'rxjs';
import { OrdersService } from '../services/orders.service';
import { Order } from '../../models/order.model';
import { OrderedDrug } from '../../models/ordered-drug.model';
import { DrugStorage } from '../../models/drug-storage.model';
import { User } from '../../models/user.model';
import { LoginService } from '../../account/services/login.service';
import { StoredDrug } from '../../models/stored-drug.mode';

@Component({
  selector: 'app-resolve-order',
  templateUrl: './resolve-order.component.html',
  styleUrls: ['./resolve-order.component.css']
})
export class ResolveOrderComponent implements OnInit, OnDestroy{
  storageId?: number;
  orders$?: Observable<Order[]>;
  selectedOrder$?: Observable<Order>;
  drugStorage$?: Observable<DrugStorage>;
  isAvailable: boolean | null = null;
  private getOrderedDrugsSubscription?: Subscription;
  private getLoggedInUserSubscription?: Subscription;
  private getStoredDrugSubscription?: Subscription;

  constructor (private ordersService: OrdersService, private loginsService: LoginService) {}

  selectOrder(order: Order) {
    this.selectedOrder$= this.ordersService.getOrder(order.id);
  }

  resolveOrder() {
    if (!this.canBeResolved()) {
      console.log("Order cannot be resolved");
      return;
    }
    console.log("Resolving order");
  }


  ngOnInit(): void {
    this.orders$ = this.ordersService.getUnresolvedOrders();
    this.getLoggedInUserSubscription = this.loginsService.getLoggedInUser().pipe(take(1)).subscribe((user: User | null) => {
      if (user) {
        if (user.pharmacy) {
          this.storageId = user.pharmacy.storage.id;
        }
      }});
      if (this.storageId)
        this.drugStorage$ = this.ordersService.getDrugStorage(this.storageId);
  }

  canBeResolved() : boolean {
    if (!this.selectedOrder$) {
      return false
    }
    if (!this.storageId) {
      return false;
    }
    let valid = true;
    this.getOrderedDrugsSubscription = this.selectedOrder$.pipe(take(1)).subscribe((order: Order) => {
      order.orderedDrugs.forEach((orderedDrug: OrderedDrug) => { 
        this.getStoredDrugSubscription = this.ordersService.getStoredDrug(orderedDrug.drug.id, this.storageId!).pipe(take(1)).subscribe((storedDrug: StoredDrug) => {
          if (storedDrug) {
            console.log("Stored drug: ", storedDrug);
            if (storedDrug.quantity < orderedDrug.quantity) {
              valid = false;
            }
          } 
        })
      });
  });
    return valid;
  }

  ngOnDestroy(): void {
    this.getOrderedDrugsSubscription?.unsubscribe();
    this.getLoggedInUserSubscription?.unsubscribe();
    this.getStoredDrugSubscription?.unsubscribe();
  }
}
