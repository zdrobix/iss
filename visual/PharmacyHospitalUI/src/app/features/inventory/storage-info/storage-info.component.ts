import { Component, OnDestroy, OnInit } from '@angular/core';
import { User } from '../../models/user.model';
import { StorageService } from '../services/storage.service';
import { LoginService } from '../../account/services/login.service';
import { Observable, Subscription } from 'rxjs';
import { DrugStorage } from '../../models/drug-storage.model';

@Component({
  selector: 'app-storage-info',
  templateUrl: './storage-info.component.html',
  styleUrls: ['./storage-info.component.css']
})
export class StorageInfoComponent implements OnInit, OnDestroy {
  storageId?: number;
  storage$?: Observable<DrugStorage>;
  drugStorage?: DrugStorage;
  loggedInUser?: User;
  getLoggedInUserSubscription?: Subscription;
  getDrugStorageSubscription?: Subscription;

  constructor(private loginsService: LoginService, private storagesService: StorageService) { }
  
  ngOnInit(): void {
    this.getLoggedInUserSubscription = this.loginsService.getLoggedInUser().subscribe((user: User | null) => {
      if (user) {
        this.loggedInUser = user;
        if (user.pharmacy) {
          this.storageId = user.pharmacy.storage.id;
        }
      }
    });
    if (this.storageId) {
      this.storage$ = this.storagesService.getStorageById(this.storageId);
      this.getDrugStorageSubscription = this.storage$.subscribe((storage: DrugStorage) => {
        this.drugStorage = storage;
      });
    }
  }

  ngOnDestroy(): void {
    this.getLoggedInUserSubscription?.unsubscribe();
    this.getDrugStorageSubscription?.unsubscribe();
  }
}
