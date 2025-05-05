import { Component, OnDestroy, OnInit } from '@angular/core';
import { catchError, map, Observable, of, Subscription, take, tap } from 'rxjs';
import { OrdersService } from '../services/orders.service';
import { Order } from '../../models/order.model';
import { OrderedDrug } from '../../models/ordered-drug.model';
import { DrugStorage } from '../../models/drug-storage.model';
import { User } from '../../models/user.model';
import { LoginService } from '../../account/services/login.service';
import { StoredDrug } from '../../models/stored-drug.mode';
import { Drug } from '../../models/drug.model';

@Component({
  selector: 'app-resolve-order',
  templateUrl: './resolve-order.component.html',
  styleUrls: ['./resolve-order.component.css']
})
export class ResolveOrderComponent implements OnInit, OnDestroy{
  storageId?: number;
  orderIsValid?: boolean;
  orders$?: Observable<Order[]>;
  selectedOrder$?: Observable<Order>;
  drugStorage$?: Observable<DrugStorage>;
  isAvailable: boolean | null = null;
  unavailableDrugs?: Drug[];
  private getOrderedDrugsSubscription?: Subscription;
  private getLoggedInUserSubscription?: Subscription;
  private getStoredDrugSubscription?: Subscription;
  

  constructor (private ordersService: OrdersService, private loginsService: LoginService) {}

  selectOrder(order: Order) {
    this.unavailableDrugs = [];
    this.selectedOrder$= this.ordersService.getOrder(order.id);
    this.checkOrder();
  }

  resolveOrder() {
    if (!this.selectedOrder$) {
      return;
    }
    this.selectedOrder$.pipe(take(1)).subscribe((order: Order) => { console.log(order)});
    if (!this.orderIsValid) {
      console.log("Order cannot be resolved");
      return;
    }
    console.log("Resolving order");
  }

  isDrugAvailable(drug: Drug): boolean {
    if (!this.unavailableDrugs || this.unavailableDrugs.length === 0) {
      return true;
    }
    return !this.unavailableDrugs.some(unavailableDrug => unavailableDrug.id === drug.id);
  }


  ngOnInit(): void {
    this.unavailableDrugs = [];
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

  checkOrder() : void {
    this.orderIsValid = true;
    if (!this.selectedOrder$) {
      this.orderIsValid = false;
      return;
    }
    if (!this.storageId) {
      this.orderIsValid = false;
      return;
    }
    this.unavailableDrugs = [];
    this.getOrderedDrugsSubscription = this.selectedOrder$.pipe(take(1)).subscribe((order: Order) => {
      order.orderedDrugs.forEach((orderedDrug: OrderedDrug) => { 
        this.getStoredDrugSubscription = this.ordersService.getStoredDrug(orderedDrug.drug.id, this.storageId!).pipe(take(1)).subscribe((storedDrug: StoredDrug) => {
          if (storedDrug) {
            if (storedDrug.quantity < orderedDrug.quantity) {
              this.orderIsValid = false;
              this.unavailableDrugs?.push(orderedDrug.drug);
            }
          } 
        })
      });
    });
  }

  ngOnDestroy(): void {
    this.getOrderedDrugsSubscription?.unsubscribe();
    this.getLoggedInUserSubscription?.unsubscribe();
    this.getStoredDrugSubscription?.unsubscribe();
  }
}
