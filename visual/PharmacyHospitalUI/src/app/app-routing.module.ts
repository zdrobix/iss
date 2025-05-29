import { Component, NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LocationStrategy, HashLocationStrategy } from '@angular/common';
import { LoginComponent } from './features/account/login/login.component';
import { InfoComponent } from './features/account/info/info.component';
import { DrugListComponent } from './features/admin/drugs/drug-list/drug-list.component';
import { AddDrugComponent } from './features/admin/drugs/add-drug/add-drug.component';
import { AuthGuard } from './core/guards/auth.guard';
import { PharmacyListComponent } from './features/admin/pharmacies/pharmacy-list/pharmacy-list.component';
import { AddPharmacyComponent } from './features/admin/pharmacies/add-pharmacy/add-pharmacy.component';
import { UserListComponent } from './features/admin/users/user-list/user-list.component';
import { AddUserComponent } from './features/admin/users/add-user/add-user.component';
import { EditStorageComponent } from './features/admin/pharmacies/edit-storage/edit-storage.component';
import { HospitalListComponent } from './features/admin/hospitals/hospital-list/hospital-list.component';
import { AddHospitalComponent } from './features/admin/hospitals/add-hospital/add-hospital.component';
import { AddOrderComponent } from './features/orders/add-order/add-order.component';
import { EditUserComponent } from './features/admin/users/edit-user/edit-user.component';
import { ResolveOrderComponent } from './features/orders/resolve-order/resolve-order.component';
import { HistoryOrderComponent } from './features/orders/history-order/history-order.component';
import { StorageInfoComponent } from './features/inventory/storage-info/storage-info.component';


const routes: Routes = [
  { path: 'login', component: LoginComponent, runGuardsAndResolvers: 'always'},
  { path: 'account', component: InfoComponent, canActivate: [AuthGuard]},
  { path: 'drugs', component: DrugListComponent, canActivate: [AuthGuard]},
  { path: 'drugs/add', component: AddDrugComponent, canActivate: [AuthGuard]},
  { path: 'pharmacies', component: PharmacyListComponent, canActivate: [AuthGuard]},
  { path: 'pharmacies/add', component: AddPharmacyComponent, canActivate: [AuthGuard]},
  { path: 'pharmacies/edit/storage/:id', component: EditStorageComponent, canActivate: [AuthGuard]},
  { path: 'hospitals', component: HospitalListComponent, canActivate: [AuthGuard]},
  { path: 'hospitals/add', component: AddHospitalComponent, canActivate: [AuthGuard]},
  { path: 'users', component: UserListComponent, canActivate: [AuthGuard]},
  { path: 'users/add', component: AddUserComponent, canActivate: [AuthGuard]},
  { path: 'users/edit/:id', component: EditUserComponent, canActivate: [AuthGuard]},
  { path: 'order/add', component: AddOrderComponent, canActivate: [AuthGuard]},
  { path: 'order/resolve', component: ResolveOrderComponent, canActivate: [AuthGuard]},
  { path: 'history', component: HistoryOrderComponent, canActivate: [AuthGuard]},
  { path: 'inventory', component: StorageInfoComponent, canActivate: [AuthGuard]},
  { path: '', redirectTo: '/login', pathMatch: 'full' },
  { path: '**', redirectTo: '/login' }
];

@NgModule({
  providers: [{provide: LocationStrategy, useClass: HashLocationStrategy}],
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
