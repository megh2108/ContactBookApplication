import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiResponse } from '../models/ApiResponse{T}';
import { ContactCountry } from '../models/contact.country.model';

@Injectable({
  providedIn: 'root'
})
export class CountryService {

  private apiUrl = 'http://localhost:5191/api/Country/';

  constructor(private http: HttpClient) {}

  getAllCountries() : Observable<ApiResponse<ContactCountry[]>>{
    return this.http.get<ApiResponse<ContactCountry[]>>(this.apiUrl+'GetAllCountries');
  }
}
