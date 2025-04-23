import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditStorageComponent } from './edit-storage.component';

describe('EditStorageComponent', () => {
  let component: EditStorageComponent;
  let fixture: ComponentFixture<EditStorageComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [EditStorageComponent]
    });
    fixture = TestBed.createComponent(EditStorageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
