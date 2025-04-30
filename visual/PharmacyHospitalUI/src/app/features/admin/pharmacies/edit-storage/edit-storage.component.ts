import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable, Subscription } from 'rxjs';
import { Drug } from 'src/app/features/models/drug.model';
import { Pharmacy } from 'src/app/features/models/pharmacy.model';
import { PharmaciesService } from '../services/pharmacies.service';
import { DrugsService } from '../../drugs/services/drugs.service';
import { UpdatePharmacyRequest } from 'src/app/features/models/update-pharmacy-request.model';
import { RouterModule } from '@angular/router';
import { UpdateDrugStorageRequest } from 'src/app/features/models/update-drogstorage-request.model';
import { AddStoredDrugRequest } from 'src/app/features/models/add-storeddrug-request.model';

@Component({
  selector: 'app-edit-storage',
  templateUrl: './edit-storage.component.html',
  styleUrls: ['./edit-storage.component.css']
})
export class EditStorageComponent implements OnInit, OnDestroy{
  
  id: number | null = null;
  pharmacy?: Pharmacy | null = null;
  private updateDrugStorageSubscription?: Subscription;
  private getPharmacySubscription?: Subscription;
  private routeSubscription?: Subscription;
  drugs$: Observable<Drug[]> | undefined;
  
  constructor(private route: ActivatedRoute, private router: Router, private pharmaciesService: PharmaciesService, private drugsService: DrugsService) {
  }


  updateStorage() {
      if  (!this.pharmacy || !this.id)
        return;
      console.log(this.pharmacy.storage)
      for (let i = 0; i < this.pharmacy.storage.storedDrugs.length; i++) {
        const storedDrug = this.pharmacy.storage.storedDrugs[i];
        if (storedDrug.drug.id === 0)
          continue
        const request: AddStoredDrugRequest = {
          drug: storedDrug.drug,
          quantity: storedDrug.quantity,
          storage: {
            id: this.pharmacy.storage.id,
            storedDrugs: []
          }
        };
        this.updateDrugStorageSubscription = this.pharmaciesService.addUpdateStoredDrug(request).subscribe({
          next: (d) => {
            //console.log(d);
          }
        });
      }
    }

  addToDrugStorage(drug: Drug, quantity: number) {
    if (!this.pharmacy || !drug || !this.pharmacy.storage || quantity <= 0)
      return;
    if (!Array.isArray(this.pharmacy.storage.storedDrugs)) 
      this.pharmacy.storage.storedDrugs = []; 
    this.pharmacy.storage.storedDrugs.push( 
      {
        id: 0,
        quantity: quantity,
        drug: drug,
        storage: {
          id: this.pharmacy.storage.id,
          storedDrugs: []
        }
      }
    );
  }
  
  ngOnInit(): void {
    this.drugs$ = this.drugsService.getDrugs();
    this.routeSubscription = this.route.params.subscribe(params => {
      this.id = params['id'];
      if (this.id) {
        this.getPharmacySubscription = this.pharmaciesService.getPharmacy(this.id).subscribe({
          next: (pharmacy) => {
            this.pharmacy = pharmacy;
          }
        });
      }
    });
  }


  ngOnDestroy(): void {
    this.updateDrugStorageSubscription?.unsubscribe();
    this.getPharmacySubscription?.unsubscribe();
    this.routeSubscription?.unsubscribe();
  }
}
