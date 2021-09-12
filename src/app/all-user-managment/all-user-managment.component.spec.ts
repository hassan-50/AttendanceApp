import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AllUserManagmentComponent } from './all-user-managment.component';

describe('AllUserManagmentComponent', () => {
  let component: AllUserManagmentComponent;
  let fixture: ComponentFixture<AllUserManagmentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AllUserManagmentComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AllUserManagmentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
