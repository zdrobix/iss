import { Component, OnDestroy } from '@angular/core';
import { Router } from '@angular/router';
import { empty, Subscription } from 'rxjs';
import { AddDrugRequest } from 'src/app/features/models/add-drug-request.model';
import { DrugsService } from 'src/app/features/admin/drugs/services/drugs.service';

@Component({
  selector: 'app-add-drug',
  templateUrl: './add-drug.component.html',
  styleUrls: ['./add-drug.component.css']
})
export class AddDrugComponent implements OnDestroy {
  model: AddDrugRequest;
  private addDrugSubscription?: Subscription;

  constructor(private drugsService: DrugsService,
      private router: Router
    ) {
      this.model = {
        name: '',
        price: null
      };
    }
  
  onFormSubmit() {
    if (!this.model.price || !this.model.name) 
      return;

    this.addDrugSubscription = this.drugsService.addDrug(this.model).subscribe({
      next: (response) => {
        this.router.navigateByUrl('/drugs');
      }});
  }

  ngOnDestroy(): void {
    this.addDrugSubscription?.unsubscribe();
  }
}
