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


const routes: Routes = [
  { path: '', component: LoginComponent },
  { path: 'account', component: InfoComponent, canActivate: [AuthGuard]},
  { path: 'drugs', component: DrugListComponent, canActivate: [AuthGuard]},
  { path: 'drugs/add', component: AddDrugComponent, canActivate: [AuthGuard]},
  { path: 'pharmacies', component: PharmacyListComponent, canActivate: [AuthGuard]},
  { path: 'pharmacies/add', component: AddPharmacyComponent, canActivate: [AuthGuard]},
  { path: '**', redirectTo: '' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
