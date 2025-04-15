import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { Drug } from 'src/app/features/models/drug.model';
import { DrugsService } from 'src/app/features/admin/drugs/services/drugs.service';

@Component({
  selector: 'app-drug-list',
  templateUrl: './drug-list.component.html',
  styleUrls: ['./drug-list.component.css']
})
export class DrugListComponent implements OnInit{
  drugs$?: Observable<Drug[]>;
  
  constructor(private drugsService: DrugsService) {

  }

  ngOnInit(): void {
    this.drugs$ = this.drugsService.getDrugs();
  }
}
