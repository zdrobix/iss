import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditStaffPharmacyComponent } from './edit-staff.component';

describe('EditStaffComponent', () => {
  let component: EditStaffPharmacyComponent;
  let fixture: ComponentFixture<EditStaffPharmacyComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [EditStaffPharmacyComponent]
    });
    fixture = TestBed.createComponent(EditStaffPharmacyComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
