import { TestBed } from '@angular/core/testing';

import { StateService } from './state.service';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { ApiResponse } from '../models/ApiResponse{T}';
import { ContactState } from '../models/contact.state.model';

describe('StateService', () => {
  let service: StateService;

  let httpMock: HttpTestingController;
  const mockApiResponse: ApiResponse<ContactState[]> = {
    success: true,
    data: [
      {
        "stateId": 1,
        "stateName": "Gujarat",
        "countryId": 1,
      },
      {
        "stateId": 2,
        "stateName": "Uttar Pradesh",
        "countryId": 1,
      },
      {
        "stateId": 3,
        "stateName": "Karnataka",
        "countryId": 1,
      },
    ],
    message: ''
  }

  beforeEach(() => {
    TestBed.configureTestingModule({ //add imports and providers in beforeeach
      imports:[HttpClientTestingModule],
      providers:[StateService]
    });
    service = TestBed.inject(StateService);
    httpMock=TestBed.inject(HttpTestingController); //inject httpmock

  });

  afterEach(()=>{
    httpMock.verify();
    
  });
  
  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should fetch all state by country id successfully',()=>{
    //Arrange
    const countryId=1;
    const apiUrl='http://localhost:5191/api/State/GetStateByCountryId/'+countryId;
    
    //Act
    service.getStatesByCountryId(countryId).subscribe((response)=>{
      expect(response.data.length).toBe(3);
      expect(response.data).toEqual(mockApiResponse.data);


    });
    const req= httpMock.expectOne(apiUrl);
    expect(req.request.method).toBe('GET');
    req.flush(mockApiResponse);

  });
  it('should handle an empty state list',()=>{
    const countryId=1;
    const apiUrl='http://localhost:5191/api/State/GetStateByCountryId/'+countryId;
    const emptyResponse:ApiResponse<ContactState[]>={
      success:true,
      data:[],
      message:''
    }
    //Act
    service.getStatesByCountryId(countryId).subscribe((response)=>{
      expect(response.data.length).toBe(0);
      expect(response.data).toEqual(emptyResponse.data);


    });
    const req= httpMock.expectOne(apiUrl);
    expect(req.request.method).toBe('GET');
    req.flush(emptyResponse);


  });
  it('should handle Http error gracefully for fetching all state by countryid',()=>{
    const countryId=1;
    const apiUrl='http://localhost:5191/api/State/GetStateByCountryId/'+countryId;
    const errorMessage='Failed to load categories';
    
    //Act
    service.getStatesByCountryId(countryId).subscribe( 
      ()=>fail('expected an error, not country'),
      (error)=>{
        expect(error.status).toBe(500);
        expect(error.statusText).toBe('Internal Server Error');
      }    
    );
    const req= httpMock.expectOne(apiUrl);
    expect(req.request.method).toBe('GET');
   // Respond with error
   req.flush(errorMessage,{status:500,statusText:'Internal Server Error'})
  });
});
