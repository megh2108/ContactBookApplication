import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ChangepasswordComponent } from './changepassword.component';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';

describe('ChangepasswordComponent', () => {
  let component: ChangepasswordComponent;
  let fixture: ComponentFixture<ChangepasswordComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports:[HttpClientModule,FormsModule],
      declarations: [ChangepasswordComponent]
    });
    fixture = TestBed.createComponent(ChangepasswordComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
