import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { PharmaciesService } from '../services/pharmacies.service';
import { Subscription } from 'rxjs/internal/Subscription';
import { UsersService } from '../../users/services/users.service';
import { map, Observable, of } from 'rxjs';
import { User } from 'src/app/features/models/user.model';
import { Pharmacy } from 'src/app/features/models/pharmacy.model';
import { UpdatePharmacyRequest } from 'src/app/features/models/update-pharmacy-request.model';

@Component({
  selector: 'app-edit-staff',
  templateUrl: './edit-staff.component.html',
  styleUrls: ['./edit-staff.component.css']
})
export class EditStaffComponent implements OnInit, OnDestroy{

  id: number | null = null;
  pharmacy?: Pharmacy | null = null;
  private updatePharmacySubscription?: Subscription;
  private getPharmacySubscription?: Subscription;
  private routeSubscription?: Subscription;
  users$: Observable<User[]> | undefined;

  constructor(private route: ActivatedRoute, private router: Router, private pharmaciesService: PharmaciesService, private usersService: UsersService) {

  }

  ngOnInit(): void {
    this.users$ = this.usersService.getUsers().pipe(
      map((users: any[]) => users.filter((user: { role: string; }) => user.role === 'PHARMACY STAFF'))
    );
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

  addToPharmacyStaff(user: User, addButton: HTMLButtonElement) {
    if (!this.pharmacy || !user || !this.pharmacy.staff)
      return;
    this.pharmacy.staff.push(user);
    addButton.disabled = true;
    addButton.innerText = '';
  }

  updatePharmacy() {
    if  (!this.pharmacy || !this.id)
      return;

    const updateRequest: UpdatePharmacyRequest = {
      name: this.pharmacy.name,
      staff: this.pharmacy.staff,
      storage: this.pharmacy.storage
    };
    this.updatePharmacySubscription = this.pharmaciesService.updatePharmacy(this.id, updateRequest).subscribe({
      next: () => {
        this.router.navigateByUrl('/pharmacies');
      }
    }); 
  }

  ngOnDestroy(): void {
    this.routeSubscription?.unsubscribe();
    this.updatePharmacySubscription?.unsubscribe();
    this.getPharmacySubscription?.unsubscribe();
  }
}
