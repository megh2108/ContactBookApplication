import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PasswordconfirmationComponent } from './passwordconfirmation.component';

describe('PasswordconfirmationComponent', () => {
  let component: PasswordconfirmationComponent;
  let fixture: ComponentFixture<PasswordconfirmationComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [PasswordconfirmationComponent]
    });
    fixture = TestBed.createComponent(PasswordconfirmationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
