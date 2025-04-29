import { Component, OnDestroy, OnInit } from '@angular/core';
import { LoginService } from '../services/login.service';
import { Observable, Subscription, take } from 'rxjs';
import { Router } from '@angular/router';
import { User } from '../../models/user.model';
import { PharmaciesService } from '../../admin/pharmacies/services/pharmacies.service';
import { HospitalsService } from '../../admin/hospitals/services/hospitals.service';

@Component({
  selector: 'app-info',
  templateUrl: './info.component.html',
  styleUrls: ['./info.component.css']
})
export class InfoComponent implements OnInit, OnDestroy {
  user$: Observable<User | null> | undefined;
  private logoutSubscription?: Subscription;
  private getPharmacySubscription?: Subscription;
  private getHospitalSubscription?: Subscription;

  constructor(private loginService: LoginService, private pharmaciesService: PharmaciesService, private hospitalsService: HospitalsService, private router: Router) {}

  ngOnInit() {
    this.user$ = this.loginService.user$;
  }

  getPharmacy(id: number) {
    if (!id)
      return;
    let pharmacyName = '';
    this.getPharmacySubscription = this.pharmaciesService.getPharmacy(id).pipe(take(1)).subscribe({
      next: (pharmacy) => {
        pharmacyName = pharmacy.name;
      }
    });
    return pharmacyName;
  }

  getHospital(id: number) {
    if (!id)
      return;
    let hospitalName = '';
    this.getHospitalSubscription = this.hospitalsService.getHospital(id).pipe(take(1)).subscribe({
      next: (hospital) => {
        hospitalName = hospital.name;
      }
    });
    return hospitalName;
  }

  logout(){
    this.logoutSubscription = this.loginService.logout().pipe(take(1)).subscribe({
      next: () => {
        this.router.navigate(['/login']);
      }
    });
  }

  ngOnDestroy() {
      this.logoutSubscription?.unsubscribe();
      this.getPharmacySubscription?.unsubscribe();
      this.getHospitalSubscription?.unsubscribe();
  }
}
