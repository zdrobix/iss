import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { RouterModule } from '@angular/router';
import { NavbarComponent } from './core/components/navbar/navbar.component';
import { LoginComponent } from './features/account/login/login.component';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { InfoComponent } from './features/account/info/info.component';
import { AddDrugComponent } from './features/admin/drugs/add-drug/add-drug.component';
import { DrugListComponent } from './features/admin/drugs/drug-list/drug-list.component';
import { PharmacyListComponent } from './features/admin/pharmacies/pharmacy-list/pharmacy-list.component';
import { AddPharmacyComponent } from './features/admin/pharmacies/add-pharmacy/add-pharmacy.component';
import { PharmacyEditComponent } from './features/admin/pharmacies/pharmacy-edit/pharmacy-edit.component';
import { AddUserComponent } from './features/admin/users/add-user/add-user.component';
import { UserListComponent } from './features/admin/users/user-list/user-list.component';
import { EditStaffComponent } from './features/admin/pharmacies/edit-staff/edit-staff.component';
import { EditStorageComponent } from './features/admin/pharmacies/edit-storage/edit-storage.component';

@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    LoginComponent,
    InfoComponent,
    AddDrugComponent,
    DrugListComponent,
    PharmacyListComponent,
    PharmacyEditComponent,
    AddPharmacyComponent,
    AddUserComponent,
    UserListComponent,
    EditStaffComponent,
    EditStorageComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    RouterModule,
    FormsModule,
    HttpClientModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
