import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TotalHoursComponent } from './total-hours.component';

describe('TotalHoursComponent', () => {
  let component: TotalHoursComponent;
  let fixture: ComponentFixture<TotalHoursComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TotalHoursComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TotalHoursComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
