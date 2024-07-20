import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UpdateContactRfComponent } from './update-contact-rf.component';
import { ReactiveFormsModule } from '@angular/forms';
import { RouterTestingModule } from '@angular/router/testing';
import { HttpClientModule } from '@angular/common/http';
import { DatePipe } from '@angular/common';

describe('UpdateContactRfComponent', () => {
  let component: UpdateContactRfComponent;
  let fixture: ComponentFixture<UpdateContactRfComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports:[HttpClientModule,RouterTestingModule,ReactiveFormsModule],
      declarations: [UpdateContactRfComponent],
      providers:[DatePipe]
    });
    fixture = TestBed.createComponent(UpdateContactRfComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
