import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UpdateuserprofileComponent } from './updateuserprofile.component';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { RouterTestingModule } from '@angular/router/testing';

describe('UpdateuserprofileComponent', () => {
  let component: UpdateuserprofileComponent;
  let fixture: ComponentFixture<UpdateuserprofileComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports:[HttpClientModule,RouterTestingModule,FormsModule],
      declarations: [UpdateuserprofileComponent]
    });
    fixture = TestBed.createComponent(UpdateuserprofileComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
