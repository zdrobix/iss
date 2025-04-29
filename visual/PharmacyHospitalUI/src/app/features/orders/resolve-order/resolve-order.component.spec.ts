import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ResolveOrderComponent } from './resolve-order.component';

describe('ResolveOrderComponent', () => {
  let component: ResolveOrderComponent;
  let fixture: ComponentFixture<ResolveOrderComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ResolveOrderComponent]
    });
    fixture = TestBed.createComponent(ResolveOrderComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
