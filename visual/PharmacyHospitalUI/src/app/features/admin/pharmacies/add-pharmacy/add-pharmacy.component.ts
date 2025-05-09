import { Component, OnDestroy } from '@angular/core';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { AddPharmacyRequest } from 'src/app/features/models/add-pharmacy-request.model';
import { PharmaciesService } from 'src/app/features/admin/pharmacies/services/pharmacies.service';

@Component({
  selector: 'app-add-pharmacy',
  templateUrl: './add-pharmacy.component.html',
  styleUrls: ['./add-pharmacy.component.css']
})

export class AddPharmacyComponent implements OnDestroy {
  model: AddPharmacyRequest;
  private addPharmacySubscription?: Subscription;

  constructor(private pharmaciesService: PharmaciesService,
    private router: Router
  ) {
    this.model = {
      name: ''
    };
  }

  onFormSubmit() {
    if (!this.model.name)
      return;

    this.addPharmacySubscription = this.pharmaciesService.addPharmacy(this.model).subscribe({
      next: () => {
        this.router.navigateByUrl('/pharmacies');
      }
    });
  }

  ngOnDestroy(): void {
    this.addPharmacySubscription?.unsubscribe();
  }
}
