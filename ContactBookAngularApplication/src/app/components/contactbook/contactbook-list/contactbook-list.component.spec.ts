import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ContactbookListComponent } from './contactbook-list.component';
import { HttpClientModule } from '@angular/common/http';
import { RouterTestingModule } from '@angular/router/testing';

describe('ContactbookListComponent', () => {
  let component: ContactbookListComponent;
  let fixture: ComponentFixture<ContactbookListComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports:[HttpClientModule,RouterTestingModule],
      declarations: [ContactbookListComponent]
    });
    fixture = TestBed.createComponent(ContactbookListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
