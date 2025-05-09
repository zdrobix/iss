import { Component, OnDestroy, OnInit } from '@angular/core';
import { Observable, Subscription } from 'rxjs';
import { Drug } from 'src/app/features/models/drug.model';
import { DrugsService } from 'src/app/features/admin/drugs/services/drugs.service';

@Component({
  selector: 'app-drug-list',
  templateUrl: './drug-list.component.html',
  styleUrls: ['./drug-list.component.css']
})

export class DrugListComponent implements OnInit, OnDestroy {

  drugs$?: Observable<Drug[]>;
  private deleteDrugSubscription?: Subscription;

  constructor(private drugsService: DrugsService) {
  }

  ngOnInit(): void {
    this.drugs$ = this.drugsService.getDrugs();
  }

  deleteDrug(id: number) {
    this.deleteDrugSubscription = this.drugsService.deleteDrug(id).subscribe(() => {
      this.drugs$ = this.drugsService.getDrugs();
    });
  }

  ngOnDestroy(): void {
    this.deleteDrugSubscription?.unsubscribe();
  }
}
