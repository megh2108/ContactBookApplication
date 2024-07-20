import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FavouriteContactComponent } from './favourite-contact.component';
import { HttpClientModule } from '@angular/common/http';
import { ContactService } from 'src/app/services/contact.service';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';
import { ChangeDetectorRef } from '@angular/core';
import { Contactbook } from 'src/app/models/contactbook.model';
import { of, throwError } from 'rxjs';
import { ApiResponse } from 'src/app/models/ApiResponse{T}';

describe('FavouriteContactComponent', () => {
  let component: FavouriteContactComponent;
  let fixture: ComponentFixture<FavouriteContactComponent>;

  let contactServiceSpy: jasmine.SpyObj<ContactService>;
  let routerSpy: jasmine.SpyObj<Router>;
  let authServiceSpy: jasmine.SpyObj<AuthService>;
  let cdrSpy: jasmine.SpyObj<ChangeDetectorRef>;

  const mockContacts: Contactbook[] = [
    { contactId: 1, name: 'Test', email: 'Test@gmail.com', phoneNumber: '1234567890', gender: 'M', favourite: true, countryId: 1, stateId: 1, fileName: '', birthDate: '', file: '', company: '', country: { countryId: 1, countryName: 'India' }, state: { stateId: 1, stateName: 'Gujrat', countryId: 1 } },
    { contactId: 1, name: 'Test2', email: 'Test2@gmail.com', phoneNumber: '1244567890', gender: 'F', favourite: true, countryId: 1, stateId: 1, fileName: '', birthDate: '', file: '', company: '', country: { countryId: 1, countryName: 'India' }, state: { stateId: 1, stateName: 'Gujrat', countryId: 1 } },

  ];

  beforeEach(() => {
    contactServiceSpy = jasmine.createSpyObj('ContactService', ['getAllFavouriteContacts','getAllFavouriteContactsWithPagination', 'getAllFavouriteContactsCount', 'getAllFavouriteSpecificContactsWithPagination', 'getAllFavouriteSpecificContactsCount', 'deleteContactById']);
    authServiceSpy = jasmine.createSpyObj('AuthService', ['isAuthenticated']);
    cdrSpy = jasmine.createSpyObj('ChangeDetectorRef', ['detectChanges']);
    TestBed.configureTestingModule({
      imports: [HttpClientModule],
      declarations: [FavouriteContactComponent],
      providers: [
        { provide: ContactService, useValue: contactServiceSpy },
        { provide: AuthService, useValue: authServiceSpy },
        { provide: ChangeDetectorRef, useValue: cdrSpy }
      ]
    });
    fixture = TestBed.createComponent(FavouriteContactComponent);
    component = fixture.componentInstance;
    // fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();

  });
  //getAllFavouriteContacts
  it('should load all favourite contacts successfully', () => {
    // Arrange
    const mockResponse: ApiResponse<Contactbook[]> = { success: true, data: mockContacts, message: '' };
    contactServiceSpy.getAllFavouriteContacts.and.returnValue(of(mockResponse));
    spyOn(component, 'processContacts').and.stub();


    // Act
    component.loadAllFavouriteContact();

    // Assert
    expect(contactServiceSpy.getAllFavouriteContacts).toHaveBeenCalled();
    // expect(component.contacts).toEqual(mockResponse.data);  
    expect(component.processContacts).toHaveBeenCalledWith(mockResponse.data);
    expect(component.loading).toBeFalse();
  });

  it('should handle unsuccessful response when loading all favourite contacts', () => {
    // Arrange
    const mockResponse: ApiResponse<Contactbook[]> = { success: false, data: [], message: 'Error message' };
    contactServiceSpy.getAllFavouriteContacts.and.returnValue(of(mockResponse));
    spyOn(console, 'error');

    // Act
    component.loadAllFavouriteContact();

    // Assert
    expect(contactServiceSpy.getAllFavouriteContacts).toHaveBeenCalled();
    expect(console.error).toHaveBeenCalledWith('Failed to fetch contacts', 'Error message');
    expect(component.loading).toBeFalse();
  });

  it('should handle error when loading all favourite contacts', () => {
    // Arrange
    const mockError = new Error('Network error');
    contactServiceSpy.getAllFavouriteContacts.and.returnValue(throwError(mockError));
    spyOn(console, 'error');

    // Act
    component.loadAllFavouriteContact();

    // Assert
    expect(contactServiceSpy.getAllFavouriteContacts).toHaveBeenCalled();
    expect(console.error).toHaveBeenCalledWith('Failed to fetch contacts', mockError);
    expect(component.loading).toBeFalse();
  });

  //getAllFavouriteContactsWithPagination
  it('should load all favourite contacts successfully', () => {
    // Arrange
    const mockResponse: ApiResponse<Contactbook[]> = { success: true, data: mockContacts, message: '' };

    // Mock the service method to return a successful response
    contactServiceSpy.getAllFavouriteContactsWithPagination.and.returnValue(of(mockResponse));

    // Act
    component.loadAllFavouriteContacts();

    // Assert
    expect(contactServiceSpy.getAllFavouriteContactsWithPagination).toHaveBeenCalledWith(component.pageNumber, component.pageSize, component.sortOrder);
    expect(component.contacts).toEqual(mockResponse.data);
    expect(component.loading).toBeFalse();
  });

  it('should handle unsuccessful response when loading contacts with pagination', () => {
    // Arrange
    const errorResponse = { success: false, data: [], message: 'Failed to fetch contacts.' };

    contactServiceSpy.getAllFavouriteContactsWithPagination.and.returnValue(of(errorResponse));

    // Act
    component.loadAllFavouriteContacts();

    // Assert
    expect(contactServiceSpy.getAllFavouriteContactsWithPagination).toHaveBeenCalled();
    expect(component.contacts).toBeUndefined(); // Ensure contacts are undefined or remain unchanged as per your error handling logic
    expect(component.loading).toBeFalse();
  });

  it('should handle error when loading contacts with pagination', () => {
    // Arrange
    const errorResponse = { message: 'Error fetching contacts.' };

    contactServiceSpy.getAllFavouriteContactsWithPagination.and.returnValue(throwError(errorResponse));

    // Act
    component.loadAllFavouriteContacts();

    // Assert
    expect(contactServiceSpy.getAllFavouriteContactsWithPagination).toHaveBeenCalled();
    expect(component.contacts).toBeUndefined(); // Ensure contacts are undefined or remain unchanged as per your error handling logic
    expect(component.loading).toBeFalse();
  });

  //getAllFavouriteContactsCount
  it('should calculate all favourite contacts count successfully', () => {
    // Arrange
    const mockResponse: ApiResponse<number> = { success: true, data: 10, message: '' };

    contactServiceSpy.getAllFavouriteContactsCount.and.returnValue(of(mockResponse));
    spyOn(component, 'loadAllFavouriteContacts').and.stub();


    // Act
    component.loadAllFavouriteContactsCount();

    // Assert
    expect(contactServiceSpy.getAllFavouriteContactsCount).toHaveBeenCalled();
    expect(component.totalItems).toBe(mockResponse.data);
    expect(component.totalPages).toBe(Math.ceil(mockResponse.data / component.pageSize));
    expect(component.loadAllFavouriteContacts).toHaveBeenCalledWith();

  });
 
  it('should handle unsuccessful response when loading all favourite contacts count', () => {
    // Arrange
    const errorResponse: ApiResponse<number> = { success: false, data: 0, message: 'Failed to fetch contacts count.' };

    contactServiceSpy.getAllFavouriteContactsCount.and.returnValue(of(errorResponse));

    // Act
    component.loadAllFavouriteContactsCount();

    // Assert
    expect(contactServiceSpy.getAllFavouriteContactsCount).toHaveBeenCalled();
    expect(component.totalItems).toBe(0);
    expect(component.loading).toBeFalse();
  });

  it('should handle error when loading all favourite contacts count', () => {
    // Arrange
    const errorResponse = { message: 'Error fetching contacts count.' };

    contactServiceSpy.getAllFavouriteContactsCount.and.returnValue(throwError(errorResponse));

    // Act
    component.loadAllFavouriteContactsCount();

    // Assert
    expect(contactServiceSpy.getAllFavouriteContactsCount).toHaveBeenCalled();
    expect(component.totalItems).toBe(0);
    expect(component.loading).toBeFalse();
  });

   //getAllFavouriteSpecificContactsWithPagination
  it('should load specific favourite contacts successfully', () => {
    // Arrange
    const mockResponse: ApiResponse<Contactbook[]> = { success: true, data: mockContacts, message: '' };
    const letter = 'T';
  
    contactServiceSpy.getAllFavouriteSpecificContactsWithPagination.and.returnValue(of(mockResponse));
  
    // Act
    component.loadFavouriteSpecificContacts(letter);
  
    // Assert
    expect(contactServiceSpy.getAllFavouriteSpecificContactsWithPagination).toHaveBeenCalledWith(component.pageNumber, component.pageSize, letter, component.sortOrder);
    expect(component.contacts).toEqual(mockResponse.data);
    expect(component.loading).toBeFalse();
  });

  it('should handle unsuccessful response when loading specific favourite contacts with pagination', () => {
    // Arrange
    const errorResponse: ApiResponse<Contactbook[]> = { success: false, data: [], message: 'Failed to fetch contacts.' };
    const letter = 'T';
  
    contactServiceSpy.getAllFavouriteSpecificContactsWithPagination.and.returnValue(of(errorResponse));
  
    // Act
    component.loadFavouriteSpecificContacts(letter);
  
    // Assert
    expect(contactServiceSpy.getAllFavouriteSpecificContactsWithPagination).toHaveBeenCalled();
    expect(component.contacts).toBeUndefined(); 
    expect(component.loading).toBeFalse();
  });

  it('should handle error when loading specific favourite contacts with pagination', () => {
    // Arrange
    const errorResponse = { message: 'Error fetching contacts.' };
    const letter = 'T';
  
    contactServiceSpy.getAllFavouriteSpecificContactsWithPagination.and.returnValue(throwError(errorResponse));
  
    // Act
    component.loadFavouriteSpecificContacts(letter);
  
    // Assert
    expect(contactServiceSpy.getAllFavouriteSpecificContactsWithPagination).toHaveBeenCalled();
    expect(component.contacts).toBeUndefined();
    expect(component.loading).toBeFalse();
  });

  //getAllFavouriteSpecificContactsCount

  it('should calculate specific favourite contacts count successfully', () => {
    // Arrange
    const mockResponse: ApiResponse<number> = { success: true, data: 10, message: '' };
    const letter = 'T';
    contactServiceSpy.getAllFavouriteSpecificContactsCount.and.returnValue(of(mockResponse));
    spyOn(component, 'loadFavouriteSpecificContacts').and.stub();
  
    // Act
    component.loadFavouriteSpecificContactsCount(letter);
  
    // Assert
    expect(contactServiceSpy.getAllFavouriteSpecificContactsCount).toHaveBeenCalledWith(letter);
    expect(component.totalItems).toBe(mockResponse.data);
    expect(component.totalPages).toBe(Math.ceil(mockResponse.data / component.pageSize));
    expect(component.loadFavouriteSpecificContacts).toHaveBeenCalledWith(letter);
  });


  it('should handle unsuccessful response when loading specific favourite contacts count', () => {
    // Arrange
    const errorResponse: ApiResponse<number> = { success: false, data: 0, message: 'Failed to fetch contacts count.' };
    const letter = 'T';

    contactServiceSpy.getAllFavouriteSpecificContactsCount.and.returnValue(of(errorResponse));

    // Act
    component.loadFavouriteSpecificContactsCount(letter);

    // Assert
    expect(contactServiceSpy.getAllFavouriteSpecificContactsCount).toHaveBeenCalledWith(letter);
    expect(component.totalItems).toBe(0);
    expect(component.loading).toBeFalse();
  });

  it('should handle error when loading specific favourite contacts count', () => {
    // Arrange
    const errorResponse = { message: 'Error fetching contacts count.' };
    const letter = 'T';

    contactServiceSpy.getAllFavouriteSpecificContactsCount.and.returnValue(throwError(errorResponse));

    // Act
    component.loadFavouriteSpecificContactsCount(letter);

    // Assert
    expect(contactServiceSpy.getAllFavouriteSpecificContactsCount).toHaveBeenCalledWith(letter);
    expect(component.totalItems).toBe(0);
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
    spyOn(component, 'loadFavouriteContacts');

    // Act
    component.contactId = 1;
    component.deleteContact();

    // Assert
    expect(contactServiceSpy.deleteContactById).toHaveBeenCalledWith(1);
    expect(component.loadFavouriteContacts).toHaveBeenCalled();
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
