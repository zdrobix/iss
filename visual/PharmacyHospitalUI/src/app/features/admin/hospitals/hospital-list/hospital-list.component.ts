import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { Hospital } from 'src/app/features/models/hospital.model';
import { HospitalsService } from '../services/hospitals.service';

@Component({
  selector: 'app-hospital-list',
  templateUrl: './hospital-list.component.html',
  styleUrls: ['./hospital-list.component.css']
})
export class HospitalListComponent implements OnInit{
  hospitals$?: Observable<Hospital[]>;
  
  constructor(private hospitalsService: HospitalsService) {

  }

  ngOnInit(): void {
    this.hospitals$ = this.hospitalsService.getHospitals();
  }
}
