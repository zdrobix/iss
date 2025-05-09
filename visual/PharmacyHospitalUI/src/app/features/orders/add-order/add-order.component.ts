import { Component, Inject, OnDestroy } from '@angular/core';
import { AddOrderRequest } from '../../models/add-order-request.model';
import { Observable, Subscription, take } from 'rxjs';
import { Router } from '@angular/router';
import { OrdersService } from '../services/orders.service';
import { LoginService } from '../../account/services/login.service';
import { User } from '../../models/user.model';
import { Drug } from '../../models/drug.model';
import { DrugsService } from '../../admin/drugs/services/drugs.service';
import { OrderedDrug } from '../../models/ordered-drug.model';

@Inject({
  providedIn: 'root'
})

@Component({
  selector: 'app-add-order',
  templateUrl: './add-order.component.html',
  styleUrls: ['./add-order.component.css']
})

export class AddOrderComponent implements OnDestroy {
  model?: AddOrderRequest;
  drugs$?: Observable<Drug[]>;
  hasOrderedAnything: boolean;
  orderedDrugs$?: Observable<OrderedDrug[]>;
  orderTotal: number = 0;
  private addOrderSubscription?: Subscription;
  private addDrugToOrderSubscription?: Subscription;
  private getOrderedDrugsSubscription?: Subscription;
  private getLoggedInUserSubscription?: Subscription;

  constructor(private router: Router, private ordersService: OrdersService, private drugsService: DrugsService, private loginsService: LoginService) {
    this.drugs$ = this.drugsService.getDrugs();
    this.hasOrderedAnything = false;
  }

  /*
  After validating the quantity and the list of ordered drugs, it is checked if the drug is already present on the order, and in that case, the quantity 
  increases. Otherwise the ordered drug is added to the list.
  */
  addDrugToOrder(drug: Drug, quantity: number) {
    if (quantity <= 0) return;
    if (!this.orderedDrugs$) {
      this.orderedDrugs$ = new Observable<OrderedDrug[]>((observer) => {
        observer.next([]);
      });
    }
    this.addDrugToOrderSubscription = this.orderedDrugs$.pipe(take(1)).subscribe((orderedDrugs) => {
      const existingDrug = orderedDrugs.find(orderedDrug => orderedDrug.drug.id === drug.id);
      if (existingDrug) {
        existingDrug.quantity += quantity;
        return;
      }
      const newOrderedDrug: OrderedDrug = {
        drug: drug,
        quantity: quantity
      };
      const updatedOrderedDrugs = [...orderedDrugs, newOrderedDrug];
      this.orderedDrugs$ = new Observable<OrderedDrug[]>((observer) => {
        observer.next(updatedOrderedDrugs);
      });
    });

    this.hasOrderedAnything = true;
    this.orderTotal += drug.price * quantity;
  }

  /*
  After checking the logged user, to avoid null errors caused by deeply nested json's, the stored drugs list is initialized with an empty list.
  */
  placeOrder() {
    this.getLoggedInUserSubscription = this.loginsService.getLoggedInUser().pipe(take(1)).subscribe((user: User | null) => {
      if (user) {
        if (user.pharmacy?.storage) {
          user.pharmacy.storage.storedDrugs = [];
        }
        this.getOrderedDrugsSubscription = this.orderedDrugs$?.pipe(take(1)).subscribe((orderedDrugs) => {
          this.model = {
            placedBy: user,
            orderedDrugs: orderedDrugs,
            dateTime: new Date()
          };
          this.addOrderSubscription = this.ordersService.addOrder(this.model).subscribe(() => {
            this.hasOrderedAnything = false;
            this.orderTotal = 0;
            this.orderedDrugs$ = new Observable<OrderedDrug[]>((observer) => {
              observer.next([]);
            });
          });
        });
        this.router.navigateByUrl('/order/add');
      }
    });
  }

  ngOnDestroy() {
    this.addDrugToOrderSubscription?.unsubscribe();
    this.addOrderSubscription?.unsubscribe();
    this.getOrderedDrugsSubscription?.unsubscribe();
    this.getLoggedInUserSubscription?.unsubscribe();
  }
}
