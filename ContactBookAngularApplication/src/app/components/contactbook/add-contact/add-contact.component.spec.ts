import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddContactComponent } from './add-contact.component';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, NgForm } from '@angular/forms';
import { RouterTestingModule } from '@angular/router/testing';
import { ContactService } from 'src/app/services/contact.service';
import { CountryService } from 'src/app/services/country.service';
import { StateService } from 'src/app/services/state.service';
import { Router } from '@angular/router';
import { ApiResponse } from 'src/app/models/ApiResponse{T}';
import { of, throwError } from 'rxjs';
import { ContactCountry } from 'src/app/models/contact.country.model';
import { ContactState } from 'src/app/models/contact.state.model';

describe('AddContactComponent', () => {
  let component: AddContactComponent;
  let fixture: ComponentFixture<AddContactComponent>;


  let contactServiceSpy: jasmine.SpyObj<ContactService>;
  let countryServiceSpy: jasmine.SpyObj<CountryService>;
  let stateServiceSpy: jasmine.SpyObj<StateService>;
  let routerSpy: Router

  beforeEach(() => {

    contactServiceSpy = jasmine.createSpyObj('ContactService', ['addContact']);
    countryServiceSpy = jasmine.createSpyObj('CountryService', ['getAllCountries']);
    stateServiceSpy = jasmine.createSpyObj('StateService', ['getStatesByCountryId']);
    routerSpy = jasmine.createSpyObj('Router', ['navigate']);

    TestBed.configureTestingModule({
      imports: [HttpClientModule, RouterTestingModule, FormsModule],
      declarations: [AddContactComponent],
      providers: [
        { provide: ContactService, useValue: contactServiceSpy },
        { provide: CountryService, useValue: countryServiceSpy },
        { provide: StateService, useValue: stateServiceSpy },
      ]
    });
    fixture = TestBed.createComponent(AddContactComponent);
    component = fixture.componentInstance;
    // fixture.detectChanges();
    routerSpy = TestBed.inject(Router)

  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });


  //addContact
  it('should navigate to /contacts-pagination on successful contact addition', () => {
    spyOn(routerSpy, 'navigate');
    const mockResponse: ApiResponse<string> = { success: true, data: '', message: '' };
    contactServiceSpy.addContact.and.returnValue(of(mockResponse));

    const form = <NgForm><unknown>{
      valid: true,
      value: {
        name: 'Test name',
        countryId: 2,
        stateId: 2,
        email: "Test@gmail.com",
        phoneNumber: "1234567891",
        fileName: '',
        file: "",
        company: "company 1",
        gender: "F",
        favourite: true,
        country: {
          countryId: 1,
          countryName: "country 1"
        },
        state: {
          countryId: 1,
          stateId: 2,
          stateName: "state 1"
        },
        birthDate: "09-08-2008"
      },
      controls: {
        contactId: { value: 1 },
        name: { value: 'Test name' },
        countryId: { value: 2 },
        stateId: { value: 2 },
        email: { value: "Test@gmail.com" },
        phoneNumber: { value: "1234567891" },
        fileName: { value: '' },
        file: { value: "" },
        company: { value: "company 1" },
        gender: { value: "F" },
        favourite: { value: true },
        birthDate: { value: "09-08-2008" }
      }
    };

    component.contact.stateId = 2; // Ensure this.contact.stateId is set to match form.value.stateId
    component.onSubmit(form);

    expect(contactServiceSpy.addContact).toHaveBeenCalledWith(component.contact); // Verify addContact was called with component.contact
    expect(routerSpy.navigate).toHaveBeenCalledWith(['/contacts-pagination']);
    expect(component.loading).toBe(false);
  });

  it('should alert error message on unsuccessful contact addition', () => {
    spyOn(window, 'alert');
    const mockResponse: ApiResponse<string> = { success: false, data: '', message: 'Error adding category' };
    contactServiceSpy.addContact.and.returnValue(of(mockResponse));
    const form = <NgForm><unknown>{
      valid: true,
      value: {
        name: 'Test name',
        countryId: 2,
        stateId: 2,
        email: "Test@gmail.com",
        phoneNumber: "1234567891",
        fileName: '',
        file: "",
        company: "company 1",
        gender: "F",
        favourite: true,
        country: {
          countryId: 1,
          countryName: "country 1"
        },
        state: {
          countryId: 1,
          stateId: 2,
          stateName: "state 1"
        },
        birthDate: "09-08-2008"
      },
      controls: {
        contactId: { value: 1 },
        name: { value: 'Test name' },
        countryId: { value: 2 },
        stateId: { value: 2 },
        email: { value: "Test@gmail.com" },
        phoneNumber: { value: "1234567891" },
        fileName: { value: '' },
        file: { value: "" },
        company: { value: "company 1" },
        gender: { value: "F" },
        favourite: { value: true },
        birthDate: { value: "09-08-2008" }
      }
    };

    component.contact.stateId = 2;
    component.onSubmit(form);

    expect(window.alert).toHaveBeenCalledWith(mockResponse.message);
    expect(component.loading).toBe(false);
  });

  it('should alert error message on HTTP error', () => {
    spyOn(console, 'error');
    spyOn(window, 'alert');
    const mockError = { error: { message: 'HTTP error' } };
    contactServiceSpy.addContact.and.returnValue(throwError(mockError));

    const form = <NgForm><unknown>{
      valid: true,
      value: {
        name: 'Test name',
        countryId: 2,
        stateId: 2,
        email: "Test@gmail.com",
        phoneNumber: "1234567891",
        fileName: '',
        file: "",
        company: "company 1",
        gender: "F",
        favourite: true,
        country: {
          countryId: 1,
          countryName: "country 1"
        },
        state: {
          countryId: 1,
          stateId: 2,
          stateName: "state 1"
        },
        birthDate: "09-08-2008"
      },
      controls: {
        contactId: { value: 1 },
        name: { value: 'Test name' },
        countryId: { value: 2 },
        stateId: { value: 2 },
        email: { value: "Test@gmail.com" },
        phoneNumber: { value: "1234567891" },
        fileName: { value: '' },
        file: { value: "" },
        company: { value: "company 1" },
        gender: { value: "F" },
        favourite: { value: true },
        birthDate: { value: "09-08-2008" }
      }
    };

    component.contact.stateId = 2;
    component.onSubmit(form);

    //either console or alert should be write.
    expect(console.error).toHaveBeenCalledWith(mockError.error.message);
    expect(window.alert).toHaveBeenCalledWith(mockError.error.message);
    expect(component.loading).toBe(false);
  });

  it('should not call categoryService.addCategory on invalid form submission', () => {
    const form = <NgForm>{ valid: false };

    component.onSubmit(form);

    expect(contactServiceSpy.addContact).not.toHaveBeenCalled();
    expect(component.loading).toBe(false);
  });

  //getAllCountries
  it('should load countries on init', () => {
    // Arrange

    const mockCountries: ContactCountry[] = [
      { countryId: 1, countryName: 'Category 1' },
      { countryId: 2, countryName: 'Category 2' },
    ];
    const mockResponse: ApiResponse<ContactCountry[]> = { success: true, data: mockCountries, message: '' };
    countryServiceSpy.getAllCountries.and.returnValue(of(mockResponse));

    // Act
    component.ngOnInit();
    fixture.detectChanges();// ngOnInit is called here

    // Assert
    expect(countryServiceSpy.getAllCountries).toHaveBeenCalled();
    expect(component.countries).toEqual(mockCountries);
  });
  it('should handle failed country loading', () => {
    // Arrange
    spyOn(console, 'error');
    const mockResponse: ApiResponse<ContactCountry[]> = { success: false, data: [], message: 'Failed to fetch countries' };
    countryServiceSpy.getAllCountries.and.returnValue(of(mockResponse));

    // Act
    component.ngOnInit();

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
    component.ngOnInit();

    // Assert
    expect(countryServiceSpy.getAllCountries).toHaveBeenCalled();
    expect(console.error).toHaveBeenCalledWith('Failed to fetch countries', mockError);
  });

  // getStatesByCountryIdId
  it('should load state from country Id', () => {
    // Arrange
    const mockStates: ContactState[] = [
      { countryId: 1, stateName: 'Category 1', stateId: 2 },
      { countryId: 2, stateName: 'Category 2', stateId: 1 },
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



});
