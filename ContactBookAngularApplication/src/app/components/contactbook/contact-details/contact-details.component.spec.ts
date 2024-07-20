import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ContactDetailsComponent } from './contact-details.component';
import { RouterTestingModule } from '@angular/router/testing';
import { HttpClientModule } from '@angular/common/http';
import { ContactService } from 'src/app/services/contact.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Contactbook } from 'src/app/models/contactbook.model';
import { ApiResponse } from 'src/app/models/ApiResponse{T}';
import { of, throwError } from 'rxjs';

describe('ContactDetailsComponent', () => {
  let component: ContactDetailsComponent;
  let fixture: ComponentFixture<ContactDetailsComponent>;
  let contactService: jasmine.SpyObj<ContactService>;
  let route: ActivatedRoute;
  let router: Router;

  const mockContact: Contactbook = {
    contactId: 1,
    name: 'Test Category',
    phoneNumber: '',
    email: '',
    company: '',
    fileName: '',
    file: '',
    favourite: false,
    gender: '',
    birthDate: '',
    countryId: 1,
    stateId: 1,
    country: {
      countryId: 1,
      countryName: ''
    },
    state: {
      stateId: 1,
      stateName: '',
      countryId: 1
    },
  };

  beforeEach(() => {
    const contactServiceSpy = jasmine.createSpyObj('ContactService', ['getContactById']);

    TestBed.configureTestingModule({
      imports: [HttpClientModule, RouterTestingModule],
      declarations: [ContactDetailsComponent],
      providers: [
        { provide: ContactService, useValue: contactServiceSpy },
        {
          provide: ActivatedRoute,
          useValue: {
            params: of({ contactId: 1 })
          }
        }
      ]
    });
    fixture = TestBed.createComponent(ContactDetailsComponent);
    component = fixture.componentInstance;
    // fixture.detectChanges();
    contactService = TestBed.inject(ContactService) as jasmine.SpyObj<ContactService>;
    route = TestBed.inject(ActivatedRoute);
    router = TestBed.inject(Router);

  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should initialize contactId from route params and load contact details', () => {
    // Arrange
    const mockResponse: ApiResponse<Contactbook> = { success: true, data: mockContact, message: '' };
    contactService.getContactById.and.returnValue(of(mockResponse));

    // Act
    fixture.detectChanges(); // ngOnInit is called here

    // Assert
    expect(component.contactId).toBe(1);
    expect(contactService.getContactById).toHaveBeenCalledWith(1);
    expect(component.contact).toEqual(mockContact);
  });

  it('should fail to load contact details', () => {
    // Arrange
    const mockResponse: ApiResponse<Contactbook> = { success: false, data: mockContact, message: '' };
    contactService.getContactById.and.returnValue(of(mockResponse));
    spyOn(console, 'error')

    // Act
    fixture.detectChanges(); // ngOnInit is called here

    // Assert
    expect(console.error).toHaveBeenCalledWith('Falied to fetch contact', mockResponse.message)
    expect(contactService.getContactById).toHaveBeenCalledWith(1);
  });

  it('should handle http error', () => {
    // Arrange
    spyOn(window, 'alert')
    const mockError = { error: {message: 'Network error'} };
    contactService.getContactById.and.returnValue(throwError(() => mockError));

    // Act
    fixture.detectChanges(); // ngOnInit is called here

    // Assert
    expect(contactService.getContactById).toHaveBeenCalledWith(1);
    expect(window.alert).toHaveBeenCalledWith(mockError.error.message);
  });
});
