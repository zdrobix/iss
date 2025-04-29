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
import { AddUserComponent } from './features/admin/users/add-user/add-user.component';
import { UserListComponent } from './features/admin/users/user-list/user-list.component';
import { EditStorageComponent } from './features/admin/pharmacies/edit-storage/edit-storage.component';
import { AddHospitalComponent } from './features/admin/hospitals/add-hospital/add-hospital.component';
import { HospitalListComponent } from './features/admin/hospitals/hospital-list/hospital-list.component';
import { AddOrderComponent } from './features/orders/add-order/add-order.component';
import { EditUserComponent } from './features/admin/users/edit-user/edit-user.component';
import { ResolveOrderComponent } from './features/orders/resolve-order/resolve-order.component';

@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    LoginComponent,
    InfoComponent,
    AddDrugComponent,
    DrugListComponent,
    PharmacyListComponent,
    AddPharmacyComponent,
    AddUserComponent,
    UserListComponent,
    EditStorageComponent,
    AddHospitalComponent,
    HospitalListComponent,
    AddOrderComponent,
    EditUserComponent,
    ResolveOrderComponent
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
