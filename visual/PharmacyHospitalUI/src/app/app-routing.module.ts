import { Component, NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './features/account/login/login.component';
import { InfoComponent } from './features/account/info/info.component';
import { FormsModule } from '@angular/forms';
import { DrugListComponent } from './features/admin/drugs/drug-list/drug-list.component';
import { AddDrugComponent } from './features/admin/drugs/add-drug/add-drug.component';
import { AuthGuard } from './core/guards/auth.guard';
import { PharmacyListComponent } from './features/admin/pharmacies/pharmacy-list/pharmacy-list.component';
import { AddPharmacyComponent } from './features/admin/pharmacies/add-pharmacy/add-pharmacy.component';
import { UserListComponent } from './features/admin/users/user-list/user-list.component';
import { AddUserComponent } from './features/admin/users/add-user/add-user.component';
import { EditStorageComponent } from './features/admin/pharmacies/edit-storage/edit-storage.component';
import { EditStaffComponent } from './features/admin/pharmacies/edit-staff/edit-staff.component';


const routes: Routes = [
  { path: 'login', component: LoginComponent, runGuardsAndResolvers: 'always'},
  { path: 'account', component: InfoComponent, canActivate: [AuthGuard]},
  { path: 'drugs', component: DrugListComponent, canActivate: [AuthGuard]},
  { path: 'drugs/add', component: AddDrugComponent, canActivate: [AuthGuard]},
  { path: 'pharmacies', component: PharmacyListComponent, canActivate: [AuthGuard]},
  { path: 'pharmacies/add', component: AddPharmacyComponent, canActivate: [AuthGuard]},
  { path: 'pharmacies/edit/staff/:id', component: EditStaffComponent, canActivate: [AuthGuard]},
  { path: 'pharmacies/edit/storage/:id', component: EditStorageComponent, canActivate: [AuthGuard]},
  { path: 'users', component: UserListComponent, canActivate: [AuthGuard]},
  { path: 'users/add', component: AddUserComponent, canActivate: [AuthGuard]},
  { path: '', redirectTo: '/login', pathMatch: 'full' },
  { path: '**', redirectTo: '/login' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
