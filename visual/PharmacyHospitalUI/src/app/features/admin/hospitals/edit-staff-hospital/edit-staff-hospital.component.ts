import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { map, Observable, Subscription } from 'rxjs';
import { Hospital } from 'src/app/features/models/hospital.model';
import { User } from 'src/app/features/models/user.model';
import { HospitalsService } from '../services/hospitals.service';
import { UsersService } from '../../users/services/users.service';
import { UpdateHospitalRequest } from 'src/app/features/models/update-hospital-request.model';

@Component({
  selector: 'app-edit-staff-hospital',
  templateUrl: './edit-staff-hospital.component.html',
  styleUrls: ['./edit-staff-hospital.component.css']
})
export class EditStaffHospitalComponent implements OnInit, OnDestroy {

  id: number | null = null;
  hospital?: Hospital | null = null;
  private updateHospitalSubscription?: Subscription;
  private getHospitalSubscription?: Subscription;
  private routeSubscription?: Subscription;
  users$: Observable<User[]> | undefined;

  constructor(private route: ActivatedRoute, private router: Router, private hospitalsService: HospitalsService, private usersService: UsersService) {}

  ngOnInit(): void {
      this.users$ = this.usersService.getUsers().pipe(
        map((users: any[]) => users.filter((user: { role: string; hospitalId: number;}) => user.role === 'HOSPITAL STAFF' && user.hospitalId === null))
      );
      this.routeSubscription = this.route.params.subscribe(params => {
        this.id = params['id'];
        if (this.id) {
          this.getHospitalSubscription = this.hospitalsService.getHospital(this.id).subscribe({
            next: (hospital) => {
              this.hospital = hospital;
            }
          });
      }
    });
  }

  addToHospitalStaff(user: User, addButton: HTMLButtonElement) {
    if (!this.hospital || !user || !this.hospital.staff)
      return;
    this.hospital.staff.push(user);
    addButton.disabled = true;
    addButton.innerText = '';
  }

  updateHospital() {
    if  (!this.hospital || !this.id)
      return;

    const updateRequest : UpdateHospitalRequest = {
      name: this.hospital.name,
      staff: this.hospital.staff,
    };

    this.updateHospitalSubscription = this.hospitalsService.updateHospital(this.id, updateRequest).subscribe({
      next: () => {
        this.router.navigateByUrl('/hospitals');
      }
    });
  }
  

  ngOnDestroy(): void {
    this.routeSubscription?.unsubscribe();
    this.updateHospitalSubscription?.unsubscribe();
    this.getHospitalSubscription?.unsubscribe();
  }

}
