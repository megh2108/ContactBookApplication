import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ForgotpasswordComponent } from './forgotpassword.component';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';

describe('ForgotpasswordComponent', () => {
  let component: ForgotpasswordComponent;
  let fixture: ComponentFixture<ForgotpasswordComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports:[HttpClientModule,FormsModule],
      declarations: [ForgotpasswordComponent]
    });
    fixture = TestBed.createComponent(ForgotpasswordComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
