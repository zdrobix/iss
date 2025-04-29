import { Component, OnDestroy, OnInit } from '@angular/core';
import { Observable, Subscription } from 'rxjs';
import { UpdateUserRequest } from 'src/app/features/models/update-user-request.model';
import { UsersService } from '../services/users.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Hospital } from 'src/app/features/models/hospital.model';
import { Pharmacy } from 'src/app/features/models/pharmacy.model';
import { HospitalsService } from '../../hospitals/services/hospitals.service';
import { PharmaciesService } from '../../pharmacies/services/pharmacies.service';

@Component({
  selector: 'app-edit-user',
  templateUrl: './edit-user.component.html',
  styleUrls: ['./edit-user.component.css']
})
export class EditUserComponent implements OnInit, OnDestroy{
    model: UpdateUserRequest = {
      name: '',
      username: '',
      password: '',
      role: '',
      hospital: null,
      pharmacy: null
    };
    hospitals$?: Observable<Hospital[]>;
    pharmacies$?: Observable<Pharmacy[]>;
    private updateUserSubscription?: Subscription;
  
    constructor(private router: Router, private route: ActivatedRoute,private userService: UsersService, private hospitalsService: HospitalsService, private pharmaciesService: PharmaciesService) {
    }
  
    onFormSubmit() {
      if (!this.model.name || !this.model.username || !this.model.password || !this.model.role) 
        return;
      const userId = Number(this.router.url.split('/').pop());
      if (!userId) return;
      if (this.model.pharmacy)
        if (this.model.pharmacy.id === 0)
          this.model.pharmacy = null;
      if (this.model.hospital)
        if (this.model.hospital.id === 0)
          this.model.hospital = null;
      console.log(this.model)
      this.updateUserSubscription = this.userService.updateUser(userId, this.model).subscribe({
        next: (response) => {
          this.router.navigateByUrl('/users');
        }
      });
    }

    ngOnInit(): void {
      this.hospitals$ = this.hospitalsService.getHospitals();
      this.pharmacies$ = this.pharmaciesService.getPharmacies();
  
      const userId = Number(this.route.snapshot.paramMap.get('id'));
      if (userId) {
        this.updateUserSubscription = this.userService.getUser(userId).subscribe(user => {
          this.model = {
            name: user.name,
            username: user.username,
            password: '',
            role: user.role,
            hospital: user.hospital || null,
            pharmacy: user.pharmacy || null
          };
        });
      }
    }
  
    ngOnDestroy(): void {
      this.updateUserSubscription?.unsubscribe();
    }
  
}
