import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditStaffHospitalComponent } from './edit-staff-hospital.component';

describe('EditStaffHospitalComponent', () => {
  let component: EditStaffHospitalComponent;
  let fixture: ComponentFixture<EditStaffHospitalComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [EditStaffHospitalComponent]
    });
    fixture = TestBed.createComponent(EditStaffHospitalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
