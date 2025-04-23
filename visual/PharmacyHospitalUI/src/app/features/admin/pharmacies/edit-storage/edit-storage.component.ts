import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable, Subscription } from 'rxjs';
import { Drug } from 'src/app/features/models/drug.model';
import { Pharmacy } from 'src/app/features/models/pharmacy.model';
import { PharmaciesService } from '../services/pharmacies.service';
import { DrugsService } from '../../drugs/services/drugs.service';
import { UpdatePharmacyRequest } from 'src/app/features/models/update-pharmacy-request.model';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-edit-storage',
  templateUrl: './edit-storage.component.html',
  styleUrls: ['./edit-storage.component.css']
})
export class EditStorageComponent implements OnInit, OnDestroy{
  
  id: number | null = null;
  pharmacy?: Pharmacy | null = null;
  private updatePharmacySubscription?: Subscription;
  private getPharmacySubscription?: Subscription;
  private routeSubscription?: Subscription;
  drugs$: Observable<Drug[]> | undefined;
  
  constructor(private route: ActivatedRoute, private router: Router, private pharmaciesService: PharmaciesService, private drugsService: DrugsService) {
  }


  updatePharmacy() {
      if  (!this.pharmacy || !this.id)
        return;
  
      const updateRequest: UpdatePharmacyRequest = {
        name: this.pharmacy.name,
        staff: this.pharmacy.staff,
        storage: this.pharmacy.storage
      };
      console.log(updateRequest);
      this.updatePharmacySubscription = this.pharmaciesService.updatePharmacy(this.id, updateRequest).subscribe({
        next: () => {
          this.router.navigateByUrl('/pharmacies');
        }
      }); 
    }

  addToDrugStorage(drug: Drug, quantity: number) {
    if (!this.pharmacy || !drug || !this.pharmacy.storage || quantity <= 0)
      return;
    if (!Array.isArray(this.pharmacy.storage.storedDrugs)) 
      this.pharmacy.storage.storedDrugs = []; 
    this.pharmacy.storage.storedDrugs.push( 
      {
        quantity: quantity,
        drug: drug
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
    this.updatePharmacySubscription?.unsubscribe();
    this.getPharmacySubscription?.unsubscribe();
    this.routeSubscription?.unsubscribe();
  }
}
