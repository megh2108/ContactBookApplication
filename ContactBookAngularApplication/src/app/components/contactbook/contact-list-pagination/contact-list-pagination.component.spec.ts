import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ContactListPaginationComponent } from './contact-list-pagination.component';
import { HttpClientModule } from '@angular/common/http';
import { ContactService } from 'src/app/services/contact.service';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';
import { ChangeDetectorRef } from '@angular/core';
import { Contactbook } from 'src/app/models/contactbook.model';
import { ApiResponse } from 'src/app/models/ApiResponse{T}';
import { of, throwError } from 'rxjs';

describe('ContactListPaginationComponent', () => {
  let component: ContactListPaginationComponent;
  let fixture: ComponentFixture<ContactListPaginationComponent>;

  let contactServiceSpy: jasmine.SpyObj<ContactService>;
  let routerSpy: jasmine.SpyObj<Router>;
  let authServiceSpy: jasmine.SpyObj<AuthService>;
  let cdrSpy: jasmine.SpyObj<ChangeDetectorRef>;

  const mockContacts: Contactbook[] = [
    { contactId: 1, name: 'Test', email: 'Test@gmail.com', phoneNumber: '1234567890', gender: 'M', favourite: true, countryId: 1, stateId: 1, fileName: '', birthDate: '', file: '', company: '', country: { countryId: 1, countryName: 'India' }, state: { stateId: 1, stateName: 'Gujrat', countryId: 1 } },
    { contactId: 1, name: 'Test2', email: 'Test2@gmail.com', phoneNumber: '1244567890', gender: 'F', favourite: true, countryId: 1, stateId: 1, fileName: '', birthDate: '', file: '', company: '', country: { countryId: 1, countryName: 'India' }, state: { stateId: 1, stateName: 'Gujrat', countryId: 1 } },

  ];



  beforeEach(() => {

    contactServiceSpy = jasmine.createSpyObj('ContactService', ['getAllContacts','getAllContactsWithPagination','getAllContactsCount','getAllSpecificContactsWithPagination','getAllSpecificContactsCount','deleteContactById']);
    authServiceSpy = jasmine.createSpyObj('AuthService', ['isAuthenticated']);
    cdrSpy = jasmine.createSpyObj('ChangeDetectorRef', ['detectChanges']);

    TestBed.configureTestingModule({
      imports: [HttpClientModule],
      declarations: [ContactListPaginationComponent],
      providers: [
        { provide: ContactService, useValue: contactServiceSpy },
        { provide: AuthService, useValue: authServiceSpy },
        { provide: ChangeDetectorRef, useValue: cdrSpy }
      ],
    });
    fixture = TestBed.createComponent(ContactListPaginationComponent);
    component = fixture.componentInstance;
    // fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  //getAllContacts
  it('should load all contacts successfully', () => {
    // Arrange
    const mockResponse: ApiResponse<Contactbook[]> = { success: true, data: mockContacts, message: '' };
    contactServiceSpy.getAllContacts.and.returnValue(of(mockResponse));
    spyOn(component, 'processContacts').and.stub();


    // Act
    component.loadAllContact();

    // Assert
    expect(contactServiceSpy.getAllContacts).toHaveBeenCalled();
    // expect(component.contacts).toEqual(mockResponse.data);  
    expect(component.processContacts).toHaveBeenCalledWith(mockResponse.data);
    expect(component.loading).toBeFalse();
  });

  it('should handle unsuccessful response when loading all contacts', () => {
    // Arrange
    const mockResponse: ApiResponse<Contactbook[]> = { success: false, data: [], message: 'Error message' };
    contactServiceSpy.getAllContacts.and.returnValue(of(mockResponse));
    spyOn(console, 'error');

    // Act
    component.loadAllContact();

    // Assert
    expect(contactServiceSpy.getAllContacts).toHaveBeenCalled();
    expect(console.error).toHaveBeenCalledWith('Failed to fetch contacts', 'Error message');
    expect(component.loading).toBeFalse();
  });

  it('should handle error when loading all contacts', () => {
    // Arrange
    const mockError = new Error('Network error');
    contactServiceSpy.getAllContacts.and.returnValue(throwError(mockError));
    spyOn(console, 'error');

    // Act
    component.loadAllContact();

    // Assert
    expect(contactServiceSpy.getAllContacts).toHaveBeenCalled();
    expect(console.error).toHaveBeenCalledWith('Failed to fetch contacts', mockError);
    expect(component.loading).toBeFalse();
  });
  //getAllContactsCount

  it('should calculate all contacts count with search', () => {
    // Arrange
    const search = 'e';
    const mockResponse: ApiResponse<number> = { success: true, data: 2, message: '' };
    contactServiceSpy.getAllContactsCount.and.returnValue(of(mockResponse));
    spyOn(component, 'loadAllContacts').and.stub();

    // Act
    component.loadAllContactsCount(search);

    // Assert
    expect(contactServiceSpy.getAllContactsCount).toHaveBeenCalledWith(search);
    expect(component.totalItems).toBe(mockResponse.data);
    expect(component.totalPages).toBe(Math.ceil(mockResponse.data / component.pageSize));
    expect(component.loadAllContacts).toHaveBeenCalledWith(search);
    expect(component.loading).toBeFalse();
  });

  it('should handle unsuccessful response when fetching contacts count', () => {
    // Arrange
    const search = 'e';
    const mockResponse: ApiResponse<number> = { success: false, data: 0, message: 'Error message' };
    contactServiceSpy.getAllContactsCount.and.returnValue(of(mockResponse));
    spyOn(console, 'error');

    // Act
    component.loadAllContactsCount(search);

    // Assert
    expect(contactServiceSpy.getAllContactsCount).toHaveBeenCalledWith(search);
    expect(console.error).toHaveBeenCalledWith('Failed to fetch contacts count', 'Error message');
    expect(component.loading).toBeFalse();
  });

  it('should handle error when fetching contacts count', () => {
    // Arrange
    const search = 'e';
    const mockError = new Error('Network error');
    contactServiceSpy.getAllContactsCount.and.returnValue(throwError(mockError));
    spyOn(console, 'error');

    // Act
    component.loadAllContactsCount(search);

    // Assert
    expect(contactServiceSpy.getAllContactsCount).toHaveBeenCalledWith(search);
    expect(console.error).toHaveBeenCalledWith('Error fetching contacts count.', mockError);
    expect(component.loading).toBeFalse();
  });


  //loadSpecificContactsCount
  
  it('should calculate specific contacts count with letter and search', () => {
    // Arrange
    const letter = 'A';
    const search = 'e';
    const mockResponse: ApiResponse<number> = { success: true, data: 2, message: '' };
    contactServiceSpy.getAllSpecificContactsCount.and.returnValue(of(mockResponse));
    spyOn(component, 'loadSpecificContacts').and.stub();

    // Act
    component.loadSpecificContactsCount(letter, search);

    // Assert
    expect(contactServiceSpy.getAllSpecificContactsCount).toHaveBeenCalledWith(letter, search);
    expect(component.totalItems).toBe(mockResponse.data);
    expect(component.totalPages).toBe(Math.ceil(mockResponse.data / component.pageSize));
    expect(component.pageNumber).toBe(1);
    expect(component.loadSpecificContacts).toHaveBeenCalledWith(letter, search);
    expect(component.loading).toBeFalse();
  });

  it('should handle unsuccessful response when fetching specific contacts count', () => {
    // Arrange
    const letter = 'A';
    const search = 'e';
    const mockResponse: ApiResponse<number> = { success: false, data: 0, message: 'Error message' };
    contactServiceSpy.getAllSpecificContactsCount.and.returnValue(of(mockResponse));
    spyOn(console, 'error');

    // Act
    component.loadSpecificContactsCount(letter, search);

    // Assert
    expect(contactServiceSpy.getAllSpecificContactsCount).toHaveBeenCalledWith(letter, search);
    expect(console.error).toHaveBeenCalledWith('Failed to fetch contacts count', 'Error message');
    expect(component.loading).toBeFalse();
  });

  it('should handle error when fetching specific contacts count', () => {
    // Arrange
    const letter = 'A';
    const search = 'e';
    const mockError = new Error('Network error');
    contactServiceSpy.getAllSpecificContactsCount.and.returnValue(throwError(mockError));
    spyOn(console, 'error');

    // Act
    component.loadSpecificContactsCount(letter, search);

    // Assert
    expect(contactServiceSpy.getAllSpecificContactsCount).toHaveBeenCalledWith(letter, search);
    expect(console.error).toHaveBeenCalledWith('Error fetching contacts count.', mockError);
    expect(component.loading).toBeFalse();
  });

  //getAllContactsWithPagination
  it('should load all contacts with pagination', () => {
    // Arrange
    const search = 'e';
    const mockResponse: ApiResponse<Contactbook[]> = { success: true, data: mockContacts, message: '' };
    contactServiceSpy.getAllContactsWithPagination.and.returnValue(of(mockResponse));

    // Act
    component.loadAllContacts(search);

    // Assert
    expect(contactServiceSpy.getAllContactsWithPagination).toHaveBeenCalledWith(component.pageNumber, component.pageSize, component.sortOrder, search);
    expect(component.contacts).toBe(mockResponse.data);
    expect(component.loading).toBeFalse();
  });

  it('should handle unsuccessful response when loading contacts with pagination', () => {
    // Arrange
    const search = 'e';
    const mockResponse: ApiResponse<Contactbook[]> = { success: false, data: [], message: 'Error message' };
    contactServiceSpy.getAllContactsWithPagination.and.returnValue(of(mockResponse));
    spyOn(console, 'error');

    // Act
    component.loadAllContacts(search);

    // Assert
    expect(contactServiceSpy.getAllContactsWithPagination).toHaveBeenCalledWith(component.pageNumber, component.pageSize, component.sortOrder, search);
    expect(console.error).toHaveBeenCalledWith('Failed to fetch contacts', 'Error message');
    expect(component.loading).toBeFalse();
  });

  it('should handle error when loading contacts with pagination', () => {
    // Arrange
    const search = 'e';
    const mockError = new Error('Network error');
    contactServiceSpy.getAllContactsWithPagination.and.returnValue(throwError(mockError));
    spyOn(console, 'error');

    // Act
    component.loadAllContacts(search);

    // Assert
    expect(contactServiceSpy.getAllContactsWithPagination).toHaveBeenCalledWith(component.pageNumber, component.pageSize, component.sortOrder, search);
    expect(console.error).toHaveBeenCalledWith('Error fetching contacts.', mockError);
    expect(component.loading).toBeFalse();
  });


  //getAllSpecificContactsWithPagination
  it('should load specific contacts with pagination successfully', () => {
    // Arrange
    const letter = 'A';
    const search = 'e';
    const mockResponse: ApiResponse<Contactbook[]> = { success: true, data: mockContacts, message: '' };
    contactServiceSpy.getAllSpecificContactsWithPagination.and.returnValue(of(mockResponse));

    // Act
    component.loadSpecificContacts(letter, search);

    // Assert
    expect(contactServiceSpy.getAllSpecificContactsWithPagination).toHaveBeenCalledWith(component.pageNumber, component.pageSize, letter, component.sortOrder, search);
    expect(component.contacts).toBe(mockResponse.data);
    expect(component.loading).toBeFalse();
  });

  it('should handle unsuccessful response when loading specific contacts with pagination', () => {
    // Arrange
    const letter = 'A';
    const search = 'e';
    const mockResponse: ApiResponse<Contactbook[]> = { success: false, data: [], message: 'Error message' };
    contactServiceSpy.getAllSpecificContactsWithPagination.and.returnValue(of(mockResponse));
    spyOn(console, 'error');

    // Act
    component.loadSpecificContacts(letter, search);

    // Assert
    expect(contactServiceSpy.getAllSpecificContactsWithPagination).toHaveBeenCalledWith(component.pageNumber, component.pageSize, letter, component.sortOrder, search);
    expect(console.error).toHaveBeenCalledWith('Failed to fetch contacts', 'Error message');
    expect(component.loading).toBeFalse();
  });

  it('should handle error when loading specific contacts with pagination', () => {
    // Arrange
    const letter = 'A';
    const search = 'e';
    const mockError = new Error('Network error');
    contactServiceSpy.getAllSpecificContactsWithPagination.and.returnValue(throwError(mockError));
    spyOn(console, 'error');

    // Act
    component.loadSpecificContacts(letter, search);

    // Assert
    expect(contactServiceSpy.getAllSpecificContactsWithPagination).toHaveBeenCalledWith(component.pageNumber, component.pageSize, letter, component.sortOrder, search);
    expect(console.error).toHaveBeenCalledWith('Error fetching contacts.', mockError);
    expect(component.loading).toBeFalse();
  });

  
//deleteContactById

it('should call confirmDelete and set contactId for deletion', () => {
  // Arrange
  spyOn(window, 'confirm').and.returnValue(true);
  spyOn(component, 'deleteContact');

  // Act
  component.confirmDelete(1);

  // Assert
  expect(window.confirm).toHaveBeenCalledWith('Are you sure want to delete ?');
  expect(component.contactId).toBe(1);
  expect(component.deleteContact).toHaveBeenCalled();
});

it('should not call deleteContact if confirm is cancelled', () => {
  // Arrange
  spyOn(window, 'confirm').and.returnValue(false);
  spyOn(component, 'deleteContact');

  // Act
  component.confirmDelete(1);

  // Assert
  expect(window.confirm).toHaveBeenCalledWith('Are you sure want to delete ?');
  expect(component.deleteContact).not.toHaveBeenCalled();
});

it('should delete contact and reload contacts', () => {
  // Arrange
  const mockDeleteResponse: ApiResponse<string> = { success: true, data: "", message: 'Contact deleted successfully' };
  contactServiceSpy.deleteContactById.and.returnValue(of(mockDeleteResponse));
  spyOn(component, 'loadContacts');

  // Act
  component.contactId = 1;
  component.deleteContact();

  // Assert
  expect(contactServiceSpy.deleteContactById).toHaveBeenCalledWith(1);
  expect(component.loadContacts).toHaveBeenCalled();
});

it('should alert error message if delete contact fails', () => {
  // Arrange
  const mockDeleteResponse: ApiResponse<string> = { success: false, data: "", message: 'Failed to delete contact' };
  contactServiceSpy.deleteContactById.and.returnValue(of(mockDeleteResponse));
  spyOn(window, 'alert');

  // Act
  component.contactId = 1;
  component.deleteContact();

  // Assert
  expect(window.alert).toHaveBeenCalledWith('Failed to delete contact');
});
it('should alert error message if delete contact throws error', () => {
  // Arrange
  const mockError = { error: { message: 'Delete error' } };
  contactServiceSpy.deleteContactById.and.returnValue(throwError(() => mockError));
  spyOn(window, 'alert');

  // Act
  component.contactId = 1;
  component.deleteContact();

  // Assert
  expect(window.alert).toHaveBeenCalledWith(mockError.error.message);
});


});
