import { Component, OnDestroy } from '@angular/core';
import { Subscription } from 'rxjs';
import { HospitalsService } from '../services/hospitals.service';
import { Router } from '@angular/router';
import { AddHospitalRequest } from 'src/app/features/models/add-hospital-request.model';

@Component({
  selector: 'app-add-hospital',
  templateUrl: './add-hospital.component.html',
  styleUrls: ['./add-hospital.component.css']
})
export class AddHospitalComponent implements OnDestroy{
  model: AddHospitalRequest;
  private addHospitalSubscription?: Subscription;

  constructor(private hospitalsService: HospitalsService,
      private router: Router
    ) {
      this.model = {
        name: ''
      };
    }

  onFormSubmit() {
    if (!this.model.name) 
      return;

    this.addHospitalSubscription = this.hospitalsService.addHospital(this.model).subscribe({
      next: () => {
        this.router.navigateByUrl('/hospitals');
    }});
  }

  ngOnDestroy(): void {
    this.addHospitalSubscription?.unsubscribe();
  }
}
