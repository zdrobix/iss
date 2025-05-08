import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable, Subscription, take } from 'rxjs';
import { Drug } from 'src/app/features/models/drug.model';
import { Pharmacy } from 'src/app/features/models/pharmacy.model';
import { PharmaciesService } from '../services/pharmacies.service';
import { DrugsService } from '../../drugs/services/drugs.service';
import { UpdatePharmacyRequest } from 'src/app/features/models/update-pharmacy-request.model';
import { RouterModule } from '@angular/router';
import { UpdateDrugStorageRequest } from 'src/app/features/models/update-drogstorage-request.model';
import { AddStoredDrugRequest } from 'src/app/features/models/add-storeddrug-request.model';
import { StoredDrug } from 'src/app/features/models/stored-drug.mode';

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
  private getStoredDrugSubscription?: Subscription;
  drugs$: Observable<Drug[]> | undefined;
  
  constructor(private route: ActivatedRoute, private router: Router, private pharmaciesService: PharmaciesService, private drugsService: DrugsService) {
  }


  addToDrugStorage(drug: Drug, quantity: number) {
    if (!this.pharmacy || !drug || !this.pharmacy.storage || quantity <= 0)
      return;
    const request: AddStoredDrugRequest = {
      drug: drug,
      quantity: quantity,
      storageId: this.pharmacy.storage.id
    };
    const existingDrugIndex = this.pharmacy?.storage?.storedDrugs.findIndex(storedDrug => storedDrug.drug.id === drug.id);
    if (existingDrugIndex !== undefined && existingDrugIndex > -1) {
      this.pharmacy?.storage?.storedDrugs.splice(existingDrugIndex, 1);
    }
    this.pharmaciesService.addUpdateStoredDrug(request).pipe(take(1)).subscribe((storedDrug: StoredDrug) => {
      this.pharmacy?.storage?.storedDrugs.push(storedDrug);
    });
  }

  getQuantityForDrugInStorage(drugId: number, pharmacyId: number): number {
    return this.pharmacy?.storage?.storedDrugs.find(storedDrug => storedDrug.drug.id === drugId)?.quantity || 0;
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
