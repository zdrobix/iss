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
import { UpdateOrderRequest } from '../../models/update-order-request.model';
import { Router } from '@angular/router';
import { AddStoredDrugRequest } from '../../models/add-storeddrug-request.model';

@Component({
  selector: 'app-resolve-order',
  templateUrl: './resolve-order.component.html',
  styleUrls: ['./resolve-order.component.css']
})
export class ResolveOrderComponent implements OnInit, OnDestroy{
  storageId?: number;
  loggedInUser?: User;
  orderIsValid?: boolean;
  orders$?: Observable<Order[]>;
  selectedOrder?: Order;
  selectedOrder$?: Observable<Order>;
  drugStorage$?: Observable<DrugStorage>;
  drugStorage?: DrugStorage;
  unavailableDrugs?: Drug[];
  storedDrugs?: StoredDrug[];
  private getOrderedDrugsSubscription?: Subscription;
  private getSelectedOrderSubscription?: Subscription;
  private getLoggedInUserSubscription?: Subscription;
  private getStoredDrugSubscription?: Subscription;
  private updateOrderSubscription?: Subscription;
  private getDrugStorageSubscription?: Subscription;

  constructor (private ordersService: OrdersService, private loginsService: LoginService, private router: Router) {}

  selectOrder(order: Order) {
    this.unavailableDrugs = [];
    this.storedDrugs = [];
    this.selectedOrder$= this.ordersService.getOrder(order.id);
    this.checkOrder();
  }

  resolveOrder() {
    if (!this.selectedOrder$) 
      return;

    this.getSelectedOrderSubscription = this.selectedOrder$.pipe(take(1)).subscribe((order: Order) => { 
      this.selectedOrder = order;  
    });
    
    if (!this.orderIsValid) 
      return;

    if (!this.selectedOrder || !this.loggedInUser) return;
    this.selectedOrder?.orderedDrugs.forEach((orderedDrug: OrderedDrug) => {
      if (!this.storedDrugs) return;
      const updateStoredDrugRequest: AddStoredDrugRequest = {
        quantity: this.storedDrugs?.find(storedDrug => storedDrug.drug.id === orderedDrug.drug.id)?.quantity! - orderedDrug.quantity,
        drug: orderedDrug.drug,
        storageId: this.drugStorage!.id
      };
      this.ordersService.updateStoredDrug(updateStoredDrugRequest).pipe(take(1)).subscribe((storedDrug: StoredDrug) => {
        console.log(storedDrug);
      });
    });

    const updateOrderRequest: UpdateOrderRequest = {
      resolvedById: this.loggedInUser.id
    };

    this.updateOrderSubscription = this.ordersService.updateOrder(this.selectedOrder.id, updateOrderRequest).subscribe((order: Order) => {
      this.orders$ = this.ordersService.getUnresolvedOrders(); 
      this.selectedOrder = undefined;
      this.unavailableDrugs = [];
      this.storedDrugs = [];

      this.router.navigateByUrl('/history');
    });
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
        this.loggedInUser = user;
        if (user.pharmacy) {
          this.storageId = user.pharmacy.storage.id;
        }
      }});
      if (this.storageId) {
        this.drugStorage$ = this.ordersService.getDrugStorage(this.storageId);
        this.getDrugStorageSubscription = this.drugStorage$.pipe(take(1)).subscribe((storage: DrugStorage) => {
          this.drugStorage = storage;
        });
      }

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
    this.storedDrugs = [];
    this.getOrderedDrugsSubscription = this.selectedOrder$.pipe(take(1)).subscribe((order: Order) => {
      order.orderedDrugs.forEach((orderedDrug: OrderedDrug) => { 
        this.getStoredDrugSubscription = this.ordersService.getStoredDrug(orderedDrug.drug.id, this.storageId!).pipe(take(1)).subscribe((storedDrug: StoredDrug) => {
          if (storedDrug) {
            this.storedDrugs?.push(storedDrug);
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
    this.updateOrderSubscription?.unsubscribe();
    this.getSelectedOrderSubscription?.unsubscribe();
    this.getDrugStorageSubscription?.unsubscribe();
  }

  getMillisecondsFromDate(dateString: string): string {
    const date = new Date(dateString);
    return date.getMilliseconds().toString().padStart(3, '0');
  }
}
