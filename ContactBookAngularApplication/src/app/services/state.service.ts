import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ApiResponse } from '../models/ApiResponse{T}';
import { Observable } from 'rxjs';
import { ContactState } from '../models/contact.state.model';

@Injectable({
  providedIn: 'root'
})
export class StateService {

  private apiUrl = 'http://localhost:5191/api/State/';

  constructor(private http: HttpClient) { }

  getAllStates() : Observable<ApiResponse<ContactState[]>>{
    return this.http.get<ApiResponse<ContactState[]>>(this.apiUrl+'GetAllStates');
  }

  getStatesByCountryId(countryId: number): Observable<ApiResponse<ContactState[]>> {
    return this.http.get<ApiResponse<ContactState[]>>(this.apiUrl + 'GetStateByCountryId/' + countryId);
  }

}
