import { ComponentFixture, TestBed } from '@angular/core/testing';
import { UpdateContactComponent } from './update-contact.component';
import { HttpClientModule } from '@angular/common/http';
import { RouterTestingModule } from '@angular/router/testing';
import { FormsModule, NgForm } from '@angular/forms';
import { ContactService } from 'src/app/services/contact.service';
import { CountryService } from 'src/app/services/country.service';
import { StateService } from 'src/app/services/state.service';
import { ActivatedRoute, Router } from '@angular/router';
import { of, throwError } from 'rxjs';
import { Contactbook } from 'src/app/models/contactbook.model';
import { ApiResponse } from 'src/app/models/ApiResponse{T}';
import { ContactCountry } from 'src/app/models/contact.country.model';
import { ContactState } from 'src/app/models/contact.state.model';

describe('UpdateContactComponent', () => {
  let component: UpdateContactComponent;
  let fixture: ComponentFixture<UpdateContactComponent>;

  let contactServiceSpy: jasmine.SpyObj<ContactService>;
  let countryServiceSpy: jasmine.SpyObj<CountryService>;
  let stateServiceSpy: jasmine.SpyObj<StateService>;
  let routerSpy: Router
  let route: ActivatedRoute;

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

    contactServiceSpy = jasmine.createSpyObj('contactServiceSpy', ['getContactById','updateContact']);
    countryServiceSpy = jasmine.createSpyObj('CountryService', ['getAllCountries']);
    stateServiceSpy = jasmine.createSpyObj('StateService', ['getStatesByCountryId']);
    routerSpy = jasmine.createSpyObj('Router', ['navigate']);

    TestBed.configureTestingModule({
      imports:[HttpClientModule,RouterTestingModule,FormsModule],
      declarations: [UpdateContactComponent],
      providers: [     
        { provide: ContactService, useValue: contactServiceSpy },
        { provide: CountryService, useValue: countryServiceSpy },
        { provide: StateService, useValue: stateServiceSpy },
        {
          provide: ActivatedRoute,
          useValue: {
            params: of({ contactId: 1 })
          }
        }
      ]
    });
    fixture = TestBed.createComponent(UpdateContactComponent);
    component = fixture.componentInstance;
    // fixture.detectChanges();

    routerSpy = TestBed.inject(Router)
    route = TestBed.inject(ActivatedRoute);
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  //getContactById
  it('should initialize contactId from route params and load contact details', () => {
    // Arrange
    const mockResponse: ApiResponse<Contactbook> = { success: true, data: mockContact, message: '' };
    contactServiceSpy.getContactById.and.returnValue(of(mockResponse));

    // Act
    fixture.detectChanges(); // ngOnInit is called here

    // Assert
    expect(component.contactId).toBe(1);
    expect(contactServiceSpy.getContactById).toHaveBeenCalledWith(1);
    expect(component.contact).toEqual(mockContact);
  });

  it('should fail to load contact details', () => {
    // Arrange
    const mockResponse: ApiResponse<Contactbook> = { success: false, data: mockContact, message: '' };
    contactServiceSpy.getContactById.and.returnValue(of(mockResponse));
    spyOn(console, 'error')

    // Act
    fixture.detectChanges(); // ngOnInit is called here

    // Assert
    expect(console.error).toHaveBeenCalledWith('Falied to fetch contact', mockResponse.message)
    expect(contactServiceSpy.getContactById).toHaveBeenCalledWith(1);
  });

  it('should handle http error', () => {
    // Arrange
    spyOn(window, 'alert')
    const mockError = { error: {message: 'Network error'} };
    contactServiceSpy.getContactById.and.returnValue(throwError(() => mockError));

    // Act
    fixture.detectChanges(); // ngOnInit is called here

    // Assert
    expect(contactServiceSpy.getContactById).toHaveBeenCalledWith(1);
    expect(window.alert).toHaveBeenCalledWith(mockError.error.message);
  });

  //getAllCountries

  it('should load countries', () => {
    // Arrange
    
  const mockCountries: ContactCountry[] = [
    { countryId: 1, countryName: 'Category 1'},
    { countryId: 2, countryName: 'Category 2'},
  ];
    const mockResponse: ApiResponse<ContactCountry[]> = { success: true, data: mockCountries, message: '' };
    countryServiceSpy.getAllCountries.and.returnValue(of(mockResponse));
 
    // Act
    component.loadCountries();
  // fixture.detectChanges();// ngOnInit is called here
 
    // Assert
    expect(countryServiceSpy.getAllCountries).toHaveBeenCalled();
    expect(component.countries).toEqual(mockCountries);
  });
 
  it('should handle failed country loading', () => {
    // Arrange
    const mockResponse: ApiResponse<ContactCountry[]> = { success: false, data: [], message: 'Failed to fetch countries' };
    countryServiceSpy.getAllCountries.and.returnValue(of(mockResponse));
    spyOn(console, 'error');
 
    // Act
    component.loadCountries();
 
    // Assert
    expect(countryServiceSpy.getAllCountries).toHaveBeenCalled();
    expect(console.error).toHaveBeenCalledWith('Failed to fetch countries', 'Failed to fetch countries');
  });
 
  it('should handle error during country loading HTTP Error', () => {
    // Arrange
    const mockError = { message: 'Network error' };
    countryServiceSpy.getAllCountries.and.returnValue(throwError(() => mockError));
    spyOn(console, 'error');
 
    // Act
    component.loadCountries();
 
    // Assert
    expect(countryServiceSpy.getAllCountries).toHaveBeenCalled();
    expect(console.error).toHaveBeenCalledWith('Failed to fetch countries', mockError);
  });

  //getStatesByCountryIdId

  it('should load state from country Id', () => {
    // Arrange
    const mockStates: ContactState[] = [
      { countryId: 1, stateName: 'Category 1', stateId: 2},
      { countryId: 2, stateName: 'Category 2', stateId: 1},
    ];
    const mockResponse: ApiResponse<ContactState[]> = { success: true, data: mockStates, message: '' };
    stateServiceSpy.getStatesByCountryId.and.returnValue(of(mockResponse));
  const countryId = 1;
    // Act
    component.loadStatesByCountryId(countryId) // ngOnInit is called here

    // Assert
    expect(stateServiceSpy.getStatesByCountryId).toHaveBeenCalledWith(countryId);
    expect(component.states).toEqual(mockStates);
  });

  it('should not load state when response is false', () => {
    // Arrange

    const mockResponse: ApiResponse<ContactState[]> = { success: false, data: [], message: 'Failed to fetch states' };
    stateServiceSpy.getStatesByCountryId.and.returnValue(of(mockResponse));
    spyOn(console, 'error');

     const countryId = 1;
    // Act
    component.loadStatesByCountryId(countryId) // ngOnInit is called here

    // Assert
    expect(stateServiceSpy.getStatesByCountryId).toHaveBeenCalledWith(countryId);
    expect(console.error).toHaveBeenCalledWith('Failed to fetch states', 'Failed to fetch states');
  });

  it('should handle error during country loading HTTP Error', () => {
    // Arrange
    const mockError = { message: 'Network error' };
    stateServiceSpy.getStatesByCountryId.and.returnValue(throwError(() => mockError));
    spyOn(console, 'error');
    const countryId = 1;

    // Act
    component.loadStatesByCountryId(countryId) // ngOnInit is called here
 
    // Assert
    expect(stateServiceSpy.getStatesByCountryId).toHaveBeenCalledWith(countryId);
    expect(console.error).toHaveBeenCalledWith('Failed to fetch states', mockError);
  });

  //updateContact
  it('should navigate to /paginatedContacts on successful contact modification', () => {
    // Arrange
    spyOn(routerSpy, 'navigate');
    const mockResponse: ApiResponse<string> = { success: true, data:'', message: '' };
    contactServiceSpy.updateContact.and.returnValue(of(mockResponse));

    // Act
    component.contact.stateId = 2; // Ensure this.contact.stateId is set to match form.value.stateId
    component.onSubmit({ valid: true } as NgForm);

    // Assert
    expect(routerSpy.navigate).toHaveBeenCalledWith(['/contacts-pagination']);
    expect(component.loading).toBe(false);
  });

  it('should alert error message on unsuccessful category modification', () => {
    // Arrange
    spyOn(window, 'alert'); // Spy on console.error method
    const mockResponse: ApiResponse<string> = { success: false, data: '', message: 'Error modifying category' };
    contactServiceSpy.updateContact.and.returnValue(of(mockResponse));

    // Act
    component.contact.stateId = 2; // Ensure this.contact.stateId is set to match form.value.stateId
    component.onSubmit({ valid: true } as NgForm);

    // Assert
    expect(window.alert).toHaveBeenCalledWith(mockResponse.message); // Check if console.error was called with the correct error message
  });

  it('should alert error message on HTTP error', () => {
    // Arrange
    spyOn(window, 'alert');
    const mockError = { error: { message: 'HTTP error' } };
    contactServiceSpy.updateContact.and.returnValue(throwError(mockError));

    // Act
    component.contact.stateId = 2; // Ensure this.contact.stateId is set to match form.value.stateId
    component.onSubmit({ valid: true } as NgForm);

    // Assert
    expect(window.alert).toHaveBeenCalledWith(mockError.error.message);
  });

  it('should not call categoryService.modifyCategory on invalid form submission', () => {
    // Arrange
    const form = { valid: false } as NgForm;

    // Act
    component.onSubmit(form);

    // Assert
    expect(contactServiceSpy.updateContact).not.toHaveBeenCalled();
  });


});
