import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Contactbook } from '../models/contactbook.model';
import { ApiResponse } from '../models/ApiResponse{T}';
import { AddContact } from '../models/addcontact.model';
import { ContactbookSP } from '../models/contactbooksp.model';
import { Count } from '../models/count.model';

@Injectable({
  providedIn: 'root'
})
export class ContactService {

  private apiUrl = 'http://localhost:5191/api/Contactbook/';

  constructor(private http: HttpClient) { }

  //sp api
  getAllContactsByBirthdayMonth(month: number): Observable<ApiResponse<ContactbookSP[]>> { 
    if (month >= 0) {
      return this.http.get<ApiResponse<ContactbookSP[]>>(this.apiUrl + 'GetAllContactsByBirthdayMonthSP/' + month);
    } else {
      return this.http.get<ApiResponse<ContactbookSP[]>>(this.apiUrl + 'GetAllContactsByBirthdayMonthSP');
    }
  }

  getAllContactsByStatesSP(state: number): Observable<ApiResponse<ContactbookSP[]>> { 
    if (state >= 0) {
      return this.http.get<ApiResponse<ContactbookSP[]>>(this.apiUrl + 'GetAllContactsByStatesSP/' + state);
    } else {
      return this.http.get<ApiResponse<ContactbookSP[]>>(this.apiUrl + 'GetAllContactsByStatesSP');
    }
  }

  getAllContactsCountByCountrySP(country: number): Observable<ApiResponse<number>> { 
    if (country >= 0) {
      return this.http.get<ApiResponse<number>>(this.apiUrl + 'GetAllContactsCountByCountrySP/' + country);
    } else {
      return this.http.get<ApiResponse<number>>(this.apiUrl + 'GetAllContactsCountByCountrySP');
    }
  }

  getAllContactsCountByGenderSP(gender: string): Observable<ApiResponse<number>> { 
    if (gender != "") {
      return this.http.get<ApiResponse<number>>(this.apiUrl + 'GetAllContactsCountByGenderSP/' + gender);
    } else {
      return this.http.get<ApiResponse<number>>(this.apiUrl + 'GetAllContactsCountByGenderSP');
    }
  }

  //
  //get all contact
  getAllContacts(): Observable<ApiResponse<Contactbook[]>> {
    return this.http.get<ApiResponse<Contactbook[]>>(this.apiUrl + 'GetAllContacts');
  }
  //get all favourite contact
  getAllFavouriteContacts(): Observable<ApiResponse<Contactbook[]>> {
    return this.http.get<ApiResponse<Contactbook[]>>(this.apiUrl + 'GetAllFavouriteContacts');
  }

  //count of all contact
  getAllContactsCount(search: string | null): Observable<ApiResponse<number>> {
    if (search == null) {

      return this.http.get<ApiResponse<number>>(this.apiUrl + 'GetContactsCount');
    } else {

      return this.http.get<ApiResponse<number>>(this.apiUrl + 'GetContactsCount?search=' + search);
    }
  }

  //all contact with pagination
  getAllContactsWithPagination(pageNumber: number, pageSize: number, sortOrder: string, search: string | null): Observable<ApiResponse<Contactbook[]>> {
    if (search == null) {

      return this.http.get<ApiResponse<Contactbook[]>>(this.apiUrl + 'GetAllContactsByPagination?page=' + pageNumber + '&pageSize=' + pageSize + '&sortOrder=' + sortOrder);
    } else {

      return this.http.get<ApiResponse<Contactbook[]>>(this.apiUrl + 'GetAllContactsByPagination?search=' + search + '&page=' + pageNumber + '&pageSize=' + pageSize + '&sortOrder=' + sortOrder);
    }
  }

  //count of specific contact (with letter)
  getAllSpecificContactsCount(letter: string, search: string | null): Observable<ApiResponse<number>> {
    if (search == null) {
      return this.http.get<ApiResponse<number>>(this.apiUrl + 'GetSpecificContactsCount?letter=' + letter);

    } else {
      return this.http.get<ApiResponse<number>>(this.apiUrl + 'GetSpecificContactsCount?letter=' + letter + '&search=' + search);

    }
  }

  //all sopecific contact (with letter)
  getAllSpecificContactsWithPagination(pageNumber: number, pageSize: number, letter: string, sortOrder: string, search: string | null): Observable<ApiResponse<Contactbook[]>> {
    if (search == null) {
      return this.http.get<ApiResponse<Contactbook[]>>(this.apiUrl + 'GetSpecificContactsByPaginationWithLetter?page=' + pageNumber + '&pageSize=' + pageSize + "&letter=" + letter + '&sortOrder=' + sortOrder);
    } else {

      return this.http.get<ApiResponse<Contactbook[]>>(this.apiUrl + 'GetSpecificContactsByPaginationWithLetter?search=' + search + '&page=' + pageNumber + '&pageSize=' + pageSize + "&letter=" + letter + '&sortOrder=' + sortOrder);
    }
  }

  //count of all favourite contact
  getAllFavouriteContactsCount(): Observable<ApiResponse<number>> {
    return this.http.get<ApiResponse<number>>(this.apiUrl + 'GetFavouriteContactsCount');
  }

  //all favourite contact with pagination
  getAllFavouriteContactsWithPagination(pageNumber: number, pageSize: number, sortOrder: string): Observable<ApiResponse<Contactbook[]>> {
    return this.http.get<ApiResponse<Contactbook[]>>(this.apiUrl + 'GetAllFavouriteContactsByPagination?page=' + pageNumber + '&pageSize=' + pageSize + '&sortOrder=' + sortOrder);
  }

  //count of favourite specific contact (with letter)
  getAllFavouriteSpecificContactsCount(letter: string): Observable<ApiResponse<number>> {
    return this.http.get<ApiResponse<number>>(this.apiUrl + 'GetSpecificFavouriteContactsCount?letter=' + letter);
  }

  //all favourite specific contact (with letter)
  getAllFavouriteSpecificContactsWithPagination(pageNumber: number, pageSize: number, letter: string, sortOrder: string): Observable<ApiResponse<Contactbook[]>> {
    return this.http.get<ApiResponse<Contactbook[]>>(this.apiUrl + 'GetSpecificFavouriteContactsByPaginationWithLetter?page=' + pageNumber + '&pageSize=' + pageSize + "&letter=" + letter + '&sortOrder=' + sortOrder);
  }

  addContact(addContact: AddContact): Observable<ApiResponse<string>> {
    return this.http.post<ApiResponse<string>>(this.apiUrl + 'AddContact', addContact);
  }


  getContactById(contactId: number | undefined): Observable<ApiResponse<Contactbook>> {
    return this.http.get<ApiResponse<Contactbook>>(this.apiUrl + 'GetContactsById?id=' + contactId);
  }

  updateContact(updatedContact: Contactbook): Observable<ApiResponse<string>> {
    return this.http.put<ApiResponse<string>>(this.apiUrl + 'ModifyContact', updatedContact);
  }

  deleteContactById(contactId: number | undefined): Observable<ApiResponse<string>> {
    return this.http.delete<ApiResponse<string>>(this.apiUrl + 'RemoveContact/' + contactId);
  }

}
