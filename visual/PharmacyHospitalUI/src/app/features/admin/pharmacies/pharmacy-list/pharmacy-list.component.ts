import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { Pharmacy } from 'src/app/features/models/pharmacy.model';
import { PharmaciesService } from 'src/app/features/admin/pharmacies/services/pharmacies.service';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-pharmacy-list',
  templateUrl: './pharmacy-list.component.html',
  styleUrls: ['./pharmacy-list.component.css']
})

export class PharmacyListComponent implements OnInit{
  pharmacies$?: Observable<Pharmacy[]>;
  
  constructor(private pharmaciesService: PharmaciesService, ) {

  }

  ngOnInit(): void {
    this.pharmacies$ = this.pharmaciesService.getPharmacies();
  }
}
