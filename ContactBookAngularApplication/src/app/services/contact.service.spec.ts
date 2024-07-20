import { TestBed } from '@angular/core/testing';

import { ContactService } from './contact.service';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { Contactbook } from '../models/contactbook.model';
import { ApiResponse } from '../models/ApiResponse{T}';
import { AddContact } from '../models/addcontact.model';


describe('ContactService', () => {
  let service: ContactService;
  let httpMock: HttpTestingController; //add httpMock

  //add api response with dummy data
  const mockApiResponse: ApiResponse<Contactbook[]> = {
    success: true,
    data: [
      {
        contactId: 1,
        name: "Name 1",
        email: "name1@gmail.com",
        phoneNumber: '1234567890',
        company: 'company 1',
        file: '',
        fileName: 'sample.png',
        birthDate: '2024-05-04',
        gender: 'M',
        favourite: false,
        stateId: 1,
        countryId: 2,
        state: { stateId: 1, stateName: 'state 1', countryId: 2 },
        country: { countryId: 2, countryName: 'country 1' }
      },
    ],
    message: ''
  }


  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],// add import of http bcoz we inject http denpendency in service file
      providers: [ContactService] // add provider

    });

    service = TestBed.inject(ContactService);
    httpMock = TestBed.inject(HttpTestingController); //inject httpmock

  });

  afterEach(() => {
    httpMock.verify(); //verify mock
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });


  //get all contacts
  it('should fetch all contacts successfully', () => {
    //arrange
    const apiUrl = 'http://localhost:5191/api/Contactbook/GetAllContacts';

    //act
    service.getAllContacts().subscribe((response) => {
      //assert
      expect(response.data.length).toBe(1);
      expect(response.data).toEqual(mockApiResponse.data);

    });


    const req = httpMock.expectOne(apiUrl);
    expect(req.request.method).toBe('GET');
    req.flush(mockApiResponse);

  });

  it('should handle an empty contact list', () => {
    //arrange
    const apiUrl = 'http://localhost:5191/api/Contactbook/GetAllContacts';

    const emptyResponse: ApiResponse<Contactbook[]> = {
      success: true,
      data: [],
      message: ''
    }

    //act
    service.getAllContacts().subscribe((response) => {
      //assert
      expect(response.data.length).toBe(0);
      expect(response.data).toEqual([]);

    });


    const req = httpMock.expectOne(apiUrl);
    expect(req.request.method).toBe('GET');
    req.flush(emptyResponse);
  });

  it('should handle HTTP error gracefully', () => {
    //arrange
    const apiUrl = 'http://localhost:5191/api/Contactbook/GetAllContacts';
    const errorMessage = 'Failed to load contacts';

    //act
    service.getAllContacts().subscribe(
      () => fail('expected an error, not categories'),

      (error) => {
        //assert
        expect(error.status).toBe(500);
        expect(error.statusText).toBe('Internal Server Error');
      }
    );


    const req = httpMock.expectOne(apiUrl);
    expect(req.request.method).toBe('GET');

    //respond with error message
    req.flush(errorMessage, { status: 500, statusText: 'Internal Server Error' });
  });

  //getAllContactsCount
  it('should fetch a total contact count with search successfully', () => {
    const search = 'Abc'
    const mockSuccessResponse: ApiResponse<number> = {
      success: true,
      data: 0,
      message: ''

    };
    //Act
    service.getAllContactsCount(search).subscribe(response => {
      //Assert
      expect(response.success).toBeTrue();
      expect(response.message).toBe('');
      expect(response.data).toEqual(mockSuccessResponse.data);

    });
    const req = httpMock.expectOne('http://localhost:5191/api/Contactbook/GetContactsCount?search=' + search);
    expect(req.request.method).toBe('GET');
    req.flush(mockSuccessResponse);
  });
  it('should handle failed total contact count with search retrival', () => {
    const search = 'Abc'
    const mockErrorResponse: ApiResponse<number> = {
      success: false,
      data: 2,
      message: 'No record Found',
    };
    //Act
    service.getAllContactsCount(search).subscribe(response => {
      //Assert
      expect(response.success).toBeFalse();
      expect(response).toEqual(mockErrorResponse);
      expect(response.message).toEqual('No record Found');
    });
    const req = httpMock.expectOne('http://localhost:5191/api/Contactbook/GetContactsCount?search=' + search);
    expect(req.request.method).toBe('GET');
    req.flush(mockErrorResponse);
  });
  it('should handle Http error gracefully for total count with search', () => {
    const search = 'Abc'
    const apiUrl = 'http://localhost:5191/api/Contactbook/GetContactsCount?search=' + search;
    const errorMessage = 'Failed to load contacts';
    //Act
    service.getAllContactsCount(search).subscribe(
      () => fail('expected an error, not contact'),
      (error) => {
        expect(error.status).toBe(500);
        expect(error.statusText).toBe('Internal Server Error');
      }
    );
    const req = httpMock.expectOne(apiUrl);
    expect(req.request.method).toBe('GET');
    // Respond with error
    req.flush(errorMessage, { status: 500, statusText: 'Internal Server Error' })
  });

  it('should fetch a total contact count without search successfully', () => {
    const search = null;
    const mockSuccessResponse: ApiResponse<number> = {
      success: true,
      data: 0,
      message: ''

    };
    //Act
    service.getAllContactsCount(search).subscribe(response => {
      //Assert
      expect(response.success).toBeTrue();
      expect(response.message).toBe('');
      expect(response.data).toEqual(mockSuccessResponse.data);

    });
    const req = httpMock.expectOne('http://localhost:5191/api/Contactbook/GetContactsCount');
    expect(req.request.method).toBe('GET');
    req.flush(mockSuccessResponse);
  });
  it('should handle failed total contact count without search retrival', () => {
    const search = null;
    const mockErrorResponse: ApiResponse<number> = {
      success: false,
      data: 2,
      message: 'No record Found',
    };
    //Act
    service.getAllContactsCount(search).subscribe(response => {
      //Assert
      expect(response.success).toBeFalse();
      expect(response).toEqual(mockErrorResponse);
      expect(response.message).toEqual('No record Found');
    });
    const req = httpMock.expectOne('http://localhost:5191/api/Contactbook/GetContactsCount');
    expect(req.request.method).toBe('GET');
    req.flush(mockErrorResponse);
  });
  it('should handle Http error gracefully for total count without search', () => {
    const search = null;
    const apiUrl = 'http://localhost:5191/api/Contactbook/GetContactsCount';
    const errorMessage = 'Failed to load contacts';
    //Act
    service.getAllContactsCount(search).subscribe(
      () => fail('expected an error, not contact'),
      (error) => {
        expect(error.status).toBe(500);
        expect(error.statusText).toBe('Internal Server Error');
      }
    );
    const req = httpMock.expectOne(apiUrl);
    expect(req.request.method).toBe('GET');
    // Respond with error
    req.flush(errorMessage, { status: 500, statusText: 'Internal Server Error' })
  });

  //getAllContactsWithPagination
  it('should fetch all paginated contacts with search successfully', () => {
    const search = 'abc';
    const pageNumber = 1;
    const pageSize = 2;
    const sort = 'asc';

    //Arrange
    const apiUrl = 'http://localhost:5191/api/Contactbook/GetAllContactsByPagination?search=' + search + '&page=' + pageNumber + '&pageSize=' + pageSize + '&sortOrder=' + sort;
    //Act
    service.getAllContactsWithPagination(pageNumber, pageSize, sort, search).subscribe((response) => {
      expect(response.data.length).toBe(1);
      expect(response.data).toEqual(mockApiResponse.data);

    });
    const req = httpMock.expectOne(apiUrl);
    expect(req.request.method).toBe('GET');
    req.flush(mockApiResponse);

  });
  it('should handle an empty contacts list for paginated contact with search', () => {
    const search = 'abc';
    const pageNumber = 1;
    const pageSize = 2;
    const sort = 'asc';
    //Arrange
    const apiUrl = 'http://localhost:5191/api/Contactbook/GetAllContactsByPagination?search=' + search + '&page=' + pageNumber + '&pageSize=' + pageSize + '&sortOrder=' + sort;
    const emptyResponse: ApiResponse<Contactbook[]> = {
      success: true,
      data: [],
      message: ''
    }
    //Act
    service.getAllContactsWithPagination(pageNumber, pageSize, sort, search).subscribe((response) => {
      expect(response.data.length).toBe(0);
      expect(response.data).toEqual(emptyResponse.data);


    });
    const req = httpMock.expectOne(apiUrl);
    expect(req.request.method).toBe('GET');
    req.flush(emptyResponse);


  });
  it('should handle Http error gracefully for paginated contact with search', () => {
    const search = 'abc';
    const pageNumber = 1;
    const pageSize = 2;
    const sort = 'asc';
    //Arrange
    const apiUrl = 'http://localhost:5191/api/Contactbook/GetAllContactsByPagination?search=' + search + '&page=' + pageNumber + '&pageSize=' + pageSize + '&sortOrder=' + sort;;
    const errorMessage = 'Failed to load categories';
    //Act
    service.getAllContactsWithPagination(pageNumber, pageSize, sort, search).subscribe(
      () => fail('expected an error, not contact'),
      (error) => {
        expect(error.status).toBe(500);
        expect(error.statusText).toBe('Internal Server Error');
      }
    );
    const req = httpMock.expectOne(apiUrl);
    expect(req.request.method).toBe('GET');
    // Respond with error
    req.flush(errorMessage, { status: 500, statusText: 'Internal Server Error' })
  });

  it('should fetch all paginated contacts without search successfully', () => {
    const search = null;
    const pageNumber = 1;
    const pageSize = 2;
    const sort = 'asc';

    //Arrange
    const apiUrl = 'http://localhost:5191/api/Contactbook/GetAllContactsByPagination?page=' + pageNumber + '&pageSize=' + pageSize + '&sortOrder=' + sort;
    //Act
    service.getAllContactsWithPagination(pageNumber, pageSize, sort, search).subscribe((response) => {
      expect(response.data.length).toBe(1);
      expect(response.data).toEqual(mockApiResponse.data);

    });
    const req = httpMock.expectOne(apiUrl);
    expect(req.request.method).toBe('GET');
    req.flush(mockApiResponse);

  });
  it('should handle an empty contacts list for paginated contact without search', () => {
    const search = null;
    const pageNumber = 1;
    const pageSize = 2;
    const sort = 'asc';
    //Arrange
    const apiUrl = 'http://localhost:5191/api/Contactbook/GetAllContactsByPagination?page=' + pageNumber + '&pageSize=' + pageSize + '&sortOrder=' + sort;
    const emptyResponse: ApiResponse<Contactbook[]> = {
      success: true,
      data: [],
      message: ''
    }
    //Act
    service.getAllContactsWithPagination(pageNumber, pageSize, sort, search).subscribe((response) => {
      expect(response.data.length).toBe(0);
      expect(response.data).toEqual(emptyResponse.data);


    });
    const req = httpMock.expectOne(apiUrl);
    expect(req.request.method).toBe('GET');
    req.flush(emptyResponse);


  });
  it('should handle Http error gracefully for paginated contact without search', () => {
    const search = null;
    const pageNumber = 1;
    const pageSize = 2;
    const sort = 'asc';
    //Arrange
    const apiUrl = 'http://localhost:5191/api/Contactbook/GetAllContactsByPagination?page=' + pageNumber + '&pageSize=' + pageSize + '&sortOrder=' + sort;;
    const errorMessage = 'Failed to load categories';
    //Act
    service.getAllContactsWithPagination(pageNumber, pageSize, sort, search).subscribe(
      () => fail('expected an error, not contact'),
      (error) => {
        expect(error.status).toBe(500);
        expect(error.statusText).toBe('Internal Server Error');
      }
    );
    const req = httpMock.expectOne(apiUrl);
    expect(req.request.method).toBe('GET');
    // Respond with error
    req.flush(errorMessage, { status: 500, statusText: 'Internal Server Error' })
  });

  //getAllSpecificContactsCount

  it('should fetch a total specific contact count with search successfully', () => {
    const search = 'Abc';
    const letter = 'A';
    const mockSuccessResponse: ApiResponse<number> = {
      success: true,
      data: 0,
      message: ''

    };
    //Act
    service.getAllSpecificContactsCount(letter, search).subscribe(response => {
      //Assert
      expect(response.success).toBeTrue();
      expect(response.message).toBe('');
      expect(response.data).toEqual(mockSuccessResponse.data);

    });
    const req = httpMock.expectOne('http://localhost:5191/api/Contactbook/GetSpecificContactsCount?letter=' + letter + '&search=' + search);
    expect(req.request.method).toBe('GET');
    req.flush(mockSuccessResponse);
  });
  it('should handle failed total specific contact count with search retrival', () => {
    const search = 'Abc';
    const letter = 'A';
    const mockErrorResponse: ApiResponse<number> = {
      success: false,
      data: 2,
      message: 'No record Found',
    };
    //Act
    service.getAllSpecificContactsCount(letter, search).subscribe(response => {
      //Assert
      expect(response.success).toBeFalse();
      expect(response).toEqual(mockErrorResponse);
      expect(response.message).toEqual('No record Found');
    });
    const req = httpMock.expectOne('http://localhost:5191/api/Contactbook/GetSpecificContactsCount?letter=' + letter + '&search=' + search);
    expect(req.request.method).toBe('GET');
    req.flush(mockErrorResponse);
  });
  it('should handle Http error gracefully for totalspecific  count with search', () => {
    const search = 'Abc';
    const letter = 'A';
    const apiUrl = 'http://localhost:5191/api/Contactbook/GetSpecificContactsCount?letter=' + letter + '&search=' + search;
    const errorMessage = 'Failed to load contacts';
    //Act
    service.getAllSpecificContactsCount(letter, search).subscribe(
      () => fail('expected an error, not contact'),
      (error) => {
        expect(error.status).toBe(500);
        expect(error.statusText).toBe('Internal Server Error');
      }
    );
    const req = httpMock.expectOne(apiUrl);
    expect(req.request.method).toBe('GET');
    // Respond with error
    req.flush(errorMessage, { status: 500, statusText: 'Internal Server Error' })
  });

  it('should fetch a total specific contact count without search successfully', () => {
    const search = null;
    const letter = 'A';
    const mockSuccessResponse: ApiResponse<number> = {
      success: true,
      data: 0,
      message: ''

    };
    //Act
    service.getAllSpecificContactsCount(letter, search).subscribe(response => {
      //Assert
      expect(response.success).toBeTrue();
      expect(response.message).toBe('');
      expect(response.data).toEqual(mockSuccessResponse.data);

    });
    const req = httpMock.expectOne('http://localhost:5191/api/Contactbook/GetSpecificContactsCount?letter=' + letter);
    expect(req.request.method).toBe('GET');
    req.flush(mockSuccessResponse);
  });
  it('should handle failed total specific contact count without search retrival', () => {
    const search = null;
    const letter = 'A';
    const mockErrorResponse: ApiResponse<number> = {
      success: false,
      data: 2,
      message: 'No record Found',
    };
    //Act
    service.getAllSpecificContactsCount(letter, search).subscribe(response => {
      //Assert
      expect(response.success).toBeFalse();
      expect(response).toEqual(mockErrorResponse);
      expect(response.message).toEqual('No record Found');
    });
    const req = httpMock.expectOne('http://localhost:5191/api/Contactbook/GetSpecificContactsCount?letter=' + letter);
    expect(req.request.method).toBe('GET');
    req.flush(mockErrorResponse);
  });
  it('should handle Http error gracefully for total specific count without search', () => {
    const search = null;
    const letter = 'A';

    const apiUrl = 'http://localhost:5191/api/Contactbook/GetSpecificContactsCount?letter=' + letter;
    const errorMessage = 'Failed to load contacts';
    //Act
    service.getAllSpecificContactsCount(letter, search).subscribe(
      () => fail('expected an error, not contact'),
      (error) => {
        expect(error.status).toBe(500);
        expect(error.statusText).toBe('Internal Server Error');
      }
    );
    const req = httpMock.expectOne(apiUrl);
    expect(req.request.method).toBe('GET');
    // Respond with error
    req.flush(errorMessage, { status: 500, statusText: 'Internal Server Error' })
  });

  //getAllSpecificContactsWithPagination
  it('should fetch all paginated specific contacts with search successfully', () => {
    const search = 'abc';
    const letter = 'a';
    const pageNumber = 1;
    const pageSize = 2;
    const sort = 'asc';

    //Arrange
    const apiUrl = 'http://localhost:5191/api/Contactbook/GetSpecificContactsByPaginationWithLetter?search=' + search + '&page=' + pageNumber + '&pageSize=' + pageSize + '&letter=' + letter + '&sortOrder=' + sort;
    //Act
    service.getAllSpecificContactsWithPagination(pageNumber, pageSize, letter, sort, search).subscribe((response) => {
      expect(response.data.length).toBe(1);
      expect(response.data).toEqual(mockApiResponse.data);

    });
    const req = httpMock.expectOne(apiUrl);
    expect(req.request.method).toBe('GET');
    req.flush(mockApiResponse);

  });
  it('should handle an empty contacts list for paginated specific contact with search', () => {
    const search = 'abc';
    const letter = 'a';
    const pageNumber = 1;
    const pageSize = 2;
    const sort = 'asc';
    //Arrange
    const apiUrl = 'http://localhost:5191/api/Contactbook/GetSpecificContactsByPaginationWithLetter?search=' + search + '&page=' + pageNumber + '&pageSize=' + pageSize + '&letter=' + letter + '&sortOrder=' + sort;
    const emptyResponse: ApiResponse<Contactbook[]> = {
      success: true,
      data: [],
      message: ''
    }
    //Act
    service.getAllSpecificContactsWithPagination(pageNumber, pageSize, letter, sort, search).subscribe((response) => {
      expect(response.data.length).toBe(0);
      expect(response.data).toEqual(emptyResponse.data);


    });
    const req = httpMock.expectOne(apiUrl);
    expect(req.request.method).toBe('GET');
    req.flush(emptyResponse);


  });
  it('should handle Http error gracefully for paginated specific contact with search', () => {
    const search = 'abc';
    const letter = 'a';
    const pageNumber = 1;
    const pageSize = 2;
    const sort = 'asc';
    //Arrange
    const apiUrl = 'http://localhost:5191/api/Contactbook/GetSpecificContactsByPaginationWithLetter?search=' + search + '&page=' + pageNumber + '&pageSize=' + pageSize + '&letter=' + letter + '&sortOrder=' + sort;
    const errorMessage = 'Failed to load categories';
    //Act
    service.getAllSpecificContactsWithPagination(pageNumber, pageSize, letter, sort, search).subscribe(
      () => fail('expected an error, not contact'),
      (error) => {
        expect(error.status).toBe(500);
        expect(error.statusText).toBe('Internal Server Error');
      }
    );
    const req = httpMock.expectOne(apiUrl);
    expect(req.request.method).toBe('GET');
    // Respond with error
    req.flush(errorMessage, { status: 500, statusText: 'Internal Server Error' })
  });

  it('should fetch all paginated specific contacts without search successfully', () => {
    const search = null;
    const letter = 'a';
    const pageNumber = 1;
    const pageSize = 2;
    const sort = 'asc';

    //Arrange
    const apiUrl = 'http://localhost:5191/api/Contactbook/GetSpecificContactsByPaginationWithLetter?page=' + pageNumber + '&pageSize=' + pageSize + '&letter=' + letter + '&sortOrder=' + sort;
    //Act
    service.getAllSpecificContactsWithPagination(pageNumber, pageSize, letter, sort, search).subscribe((response) => {
      expect(response.data.length).toBe(1);
      expect(response.data).toEqual(mockApiResponse.data);

    });
    const req = httpMock.expectOne(apiUrl);
    expect(req.request.method).toBe('GET');
    req.flush(mockApiResponse);

  });
  it('should handle an empty contacts list for paginated specific contact without search', () => {
    const search = null;
    const letter = 'a';
    const pageNumber = 1;
    const pageSize = 2;
    const sort = 'asc';
    //Arrange
    const apiUrl = 'http://localhost:5191/api/Contactbook/GetSpecificContactsByPaginationWithLetter?page=' + pageNumber + '&pageSize=' + pageSize + '&letter=' + letter + '&sortOrder=' + sort;
    const emptyResponse: ApiResponse<Contactbook[]> = {
      success: true,
      data: [],
      message: ''
    }
    //Act
    service.getAllSpecificContactsWithPagination(pageNumber, pageSize, letter, sort, search).subscribe((response) => {
      expect(response.data.length).toBe(0);
      expect(response.data).toEqual(emptyResponse.data);


    });
    const req = httpMock.expectOne(apiUrl);
    expect(req.request.method).toBe('GET');
    req.flush(emptyResponse);


  });
  it('should handle Http error gracefully for paginated specific contact without search', () => {
    const search = null;
    const letter = 'a';
    const pageNumber = 1;
    const pageSize = 2;
    const sort = 'asc';
    //Arrange
    const apiUrl = 'http://localhost:5191/api/Contactbook/GetSpecificContactsByPaginationWithLetter?page=' + pageNumber + '&pageSize=' + pageSize + '&letter=' + letter + '&sortOrder=' + sort;
    const errorMessage = 'Failed to load categories';
    //Act
    service.getAllSpecificContactsWithPagination(pageNumber, pageSize, letter, sort, search).subscribe(
      () => fail('expected an error, not contact'),
      (error) => {
        expect(error.status).toBe(500);
        expect(error.statusText).toBe('Internal Server Error');
      }
    );
    const req = httpMock.expectOne(apiUrl);
    expect(req.request.method).toBe('GET');
    // Respond with error
    req.flush(errorMessage, { status: 500, statusText: 'Internal Server Error' })
  });

  //getAllFavouriteContactsCount
  it('should fetch a total favourite contact count successfully', () => {
    const mockSuccessResponse: ApiResponse<number> = {
      success: true,
      data: 0,
      message: ''

    };
    //Act
    service.getAllFavouriteContactsCount().subscribe(response => {
      //Assert
      expect(response.success).toBeTrue();
      expect(response.message).toBe('');
      expect(response.data).toEqual(mockSuccessResponse.data);

    });
    const req = httpMock.expectOne('http://localhost:5191/api/Contactbook/GetFavouriteContactsCount');
    expect(req.request.method).toBe('GET');
    req.flush(mockSuccessResponse);
  });
  it('should handle failed total favourite contact count retrival', () => {
    const search = 'Abc'
    const mockErrorResponse: ApiResponse<number> = {
      success: false,
      data: 2,
      message: 'No record Found',
    };
    //Act
    service.getAllFavouriteContactsCount().subscribe(response => {
      //Assert
      expect(response.success).toBeFalse();
      expect(response).toEqual(mockErrorResponse);
      expect(response.message).toEqual('No record Found');
    });
    const req = httpMock.expectOne('http://localhost:5191/api/Contactbook/GetFavouriteContactsCount');
    expect(req.request.method).toBe('GET');
    req.flush(mockErrorResponse);
  });
  it('should handle Http error gracefully for total favourite contact count ', () => {
    const search = 'Abc'
    const apiUrl = 'http://localhost:5191/api/Contactbook/GetFavouriteContactsCount';
    const errorMessage = 'Failed to load contacts';
    //Act
    service.getAllFavouriteContactsCount().subscribe(
      () => fail('expected an error, not contact'),
      (error) => {
        expect(error.status).toBe(500);
        expect(error.statusText).toBe('Internal Server Error');
      }
    );
    const req = httpMock.expectOne(apiUrl);
    expect(req.request.method).toBe('GET');
    // Respond with error
    req.flush(errorMessage, { status: 500, statusText: 'Internal Server Error' })
  });

  //getAllFavouriteContactsWithPagination
  it('should fetch all favourite paginated contacts successfully', () => {
    const pageNumber = 1;
    const pageSize = 2;
    const sort = 'asc';

    //Arrange
    const apiUrl = 'http://localhost:5191/api/Contactbook/GetAllFavouriteContactsByPagination?page=' + pageNumber + '&pageSize=' + pageSize + '&sortOrder=' + sort;
    //Act
    service.getAllFavouriteContactsWithPagination(pageNumber, pageSize, sort).subscribe((response) => {
      expect(response.data.length).toBe(1);
      expect(response.data).toEqual(mockApiResponse.data);

    });
    const req = httpMock.expectOne(apiUrl);
    expect(req.request.method).toBe('GET');
    req.flush(mockApiResponse);

  });
  it('should handle an empty contacts list for favourite paginated contact', () => {
    const pageNumber = 1;
    const pageSize = 2;
    const sort = 'asc';
    //Arrange
    const apiUrl = 'http://localhost:5191/api/Contactbook/GetAllFavouriteContactsByPagination?page=' + pageNumber + '&pageSize=' + pageSize + '&sortOrder=' + sort;
    const emptyResponse: ApiResponse<Contactbook[]> = {
      success: true,
      data: [],
      message: ''
    }
    //Act
    service.getAllFavouriteContactsWithPagination(pageNumber, pageSize, sort).subscribe((response) => {
      expect(response.data.length).toBe(0);
      expect(response.data).toEqual(emptyResponse.data);


    });
    const req = httpMock.expectOne(apiUrl);
    expect(req.request.method).toBe('GET');
    req.flush(emptyResponse);


  });
  it('should handle Http error gracefully for favourite paginated contact', () => {
    const pageNumber = 1;
    const pageSize = 2;
    const sort = 'asc';
    //Arrange
    const apiUrl = 'http://localhost:5191/api/Contactbook/GetAllFavouriteContactsByPagination?page=' + pageNumber + '&pageSize=' + pageSize + '&sortOrder=' + sort;
    const errorMessage = 'Failed to load categories';
    //Act
    service.getAllFavouriteContactsWithPagination(pageNumber, pageSize, sort).subscribe(
      () => fail('expected an error, not contact'),
      (error) => {
        expect(error.status).toBe(500);
        expect(error.statusText).toBe('Internal Server Error');
      }
    );
    const req = httpMock.expectOne(apiUrl);
    expect(req.request.method).toBe('GET');
    // Respond with error
    req.flush(errorMessage, { status: 500, statusText: 'Internal Server Error' })
  });

  //getAllFavouriteSpecificContactsCount
  it('should fetch a total favourite contact count with letter successfully', () => {
    const letter = 'a';
    const mockSuccessResponse: ApiResponse<number> = {
      success: true,
      data: 1,
      message: ''

    };
    //Act
    service.getAllFavouriteSpecificContactsCount(letter).subscribe(response => {
      //Assert
      expect(response.success).toBeTrue();
      expect(response.message).toBe('');
      expect(response.data).toEqual(mockSuccessResponse.data);

    });
    const req = httpMock.expectOne('http://localhost:5191/api/Contactbook/GetSpecificFavouriteContactsCount?letter='+letter);
    expect(req.request.method).toBe('GET');
    req.flush(mockSuccessResponse);
  });

  it('should handle failed total favourite contact count with letter retrival', () => {
    const letter = 'a';
    const mockErrorResponse: ApiResponse<number> = {
      success: false,
      data: 0,
      message: 'No record Found',
    };
    //Act
    service.getAllFavouriteSpecificContactsCount(letter).subscribe(response => {
      //Assert
      expect(response.success).toBeFalse();
      expect(response).toEqual(mockErrorResponse);
      expect(response.message).toEqual('No record Found');
    });
    const req = httpMock.expectOne('http://localhost:5191/api/Contactbook/GetSpecificFavouriteContactsCount?letter='+letter);
    expect(req.request.method).toBe('GET');
    req.flush(mockErrorResponse);
  });
  it('should handle Http error gracefully for total favourite contact count with letter', () => {
    const letter = 'a';
    const apiUrl = 'http://localhost:5191/api/Contactbook/GetSpecificFavouriteContactsCount?letter='+letter;
    const errorMessage = 'Failed to load contacts';
    //Act
    service.getAllFavouriteSpecificContactsCount(letter).subscribe(
      () => fail('expected an error, not contact'),
      (error) => {
        expect(error.status).toBe(500);
        expect(error.statusText).toBe('Internal Server Error');
      }
    );
    const req = httpMock.expectOne(apiUrl);
    expect(req.request.method).toBe('GET');
    // Respond with error
    req.flush(errorMessage, { status: 500, statusText: 'Internal Server Error' })
  });

  //getAllFavouriteSpecificContactsWithPagination
  it('should fetch all specific favourite paginated contacts successfully', () => {
    const pageNumber = 1;
    const pageSize = 2;
    const sort = 'asc';
    const letter = 'a';

    //Arrange
    const apiUrl = 'http://localhost:5191/api/Contactbook/GetSpecificFavouriteContactsByPaginationWithLetter?page=' + pageNumber + '&pageSize=' + pageSize + '&letter=' + letter + '&sortOrder=' + sort;
    //Act
    service.getAllFavouriteSpecificContactsWithPagination(pageNumber, pageSize,letter, sort).subscribe((response) => {
      expect(response.data.length).toBe(1);
      expect(response.data).toEqual(mockApiResponse.data);

    });
    const req = httpMock.expectOne(apiUrl);
    expect(req.request.method).toBe('GET');
    req.flush(mockApiResponse);

  });
  it('should handle an empty contacts list for specific favourite paginated contact', () => {
    const pageNumber = 1;
    const pageSize = 2;
    const sort = 'asc';
    const letter = 'a';

    //Arrange
    const apiUrl = 'http://localhost:5191/api/Contactbook/GetSpecificFavouriteContactsByPaginationWithLetter?page=' + pageNumber + '&pageSize=' + pageSize + '&letter=' + letter + '&sortOrder=' + sort;
    const emptyResponse: ApiResponse<Contactbook[]> = {
      success: true,
      data: [],
      message: ''
    }
    //Act
    service.getAllFavouriteSpecificContactsWithPagination(pageNumber, pageSize, letter,sort).subscribe((response) => {
      expect(response.data.length).toBe(0);
      expect(response.data).toEqual(emptyResponse.data);


    });
    const req = httpMock.expectOne(apiUrl);
    expect(req.request.method).toBe('GET');
    req.flush(emptyResponse);


  });
  it('should handle Http error gracefully for specific favourite paginated contact', () => {
    const pageNumber = 1;
    const pageSize = 2;
    const sort = 'asc';
    const letter = 'a';

    //Arrange
    const apiUrl = 'http://localhost:5191/api/Contactbook/GetSpecificFavouriteContactsByPaginationWithLetter?page=' + pageNumber + '&pageSize=' + pageSize + '&letter=' + letter + '&sortOrder=' + sort;
    const errorMessage = 'Failed to load categories';
    //Act
    service.getAllFavouriteSpecificContactsWithPagination(pageNumber, pageSize,letter, sort).subscribe(
      () => fail('expected an error, not contact'),
      (error) => {
        expect(error.status).toBe(500);
        expect(error.statusText).toBe('Internal Server Error');
      }
    );
    const req = httpMock.expectOne(apiUrl);
    expect(req.request.method).toBe('GET');
    // Respond with error
    req.flush(errorMessage, { status: 500, statusText: 'Internal Server Error' })
  });


  //add contact
  it('should add a contacts successfully', () => {
    //arrange
    const addContact: AddContact = {
      "name": "string",
      "email": "string",
      "fileName": "string",
      "file": "string",
      "phoneNumber": "string",
      "company": "string",
      "gender": "string",
      "favourite": true,
      "countryId": 0,
      "stateId": 0,
      "birthDate": "2024-05-04",
    }

    const mockSuccessResponse: ApiResponse<string> = {
      success: true,
      message: "Contact saved successfully.",
      data: ""
    }
    //act
    service.addContact(addContact).subscribe(response => {
      //assert
      expect(response).toBe(mockSuccessResponse);
    });
    const req = httpMock.expectOne('http://localhost:5191/api/Contactbook/AddContact');
    expect(req.request.method).toBe('POST');
    req.flush(mockSuccessResponse);

  });
  it('should handle failed contact addition', () => {
    //arrange
    const addContact: AddContact = {
      "name": "string",
      "email": "string",
      "fileName": "string",
      "file": "string",
      "phoneNumber": "string",
      "company": "string",
      "gender": "string",
      "favourite": true,
      "countryId": 0,
      "stateId": 0,
      "birthDate": "2024-05-04",
    };
    const mockErrorResponse: ApiResponse<string> = {
      success: true,
      message: "Category already exists.",
      data: ""
    }
    //act
    service.addContact(addContact).subscribe(response => {
      //assert
      expect(response).toBe(mockErrorResponse);
    });
    const req = httpMock.expectOne('http://localhost:5191/api/Contactbook/AddContact');
    expect(req.request.method).toBe('POST');
    req.flush(mockErrorResponse);

  });
  it('should handle Http error while adding contact', () => {
    //arrange
    const addContact: AddContact = {
      "name": "string",
      "email": "string",
      "fileName": "string",
      "file": "string",
      "phoneNumber": "string",
      "company": "string",
      "gender": "string",
      "favourite": true,
      "countryId": 0,
      "stateId": 0,
      "birthDate": "2024-05-04",
    };
    const mockHttpError = {
      statusText: "Internal Server Error",
      status: 500
    }
    //act
    service.addContact(addContact).subscribe({
      next: () => fail('should have failed with the 500 error'),
      error: (error) => {
        //assert
        expect(error.status).toEqual(500);
        expect(error.statusText).toEqual('Internal Server Error');
      }
    });
    const req = httpMock.expectOne('http://localhost:5191/api/Contactbook/AddContact');
    expect(req.request.method).toBe('POST');
    req.flush({}, mockHttpError);

  });

  //get contact by id
  it('should fetch a contact by id successfully', () => {
    const contactId = 1;
    const mockSuccessResponse: ApiResponse<Contactbook> = {
      success: true,
      data: {
        "contactId": 1,
        "name": "string",
        "email": "string",
        "phoneNumber": "string",
        "company": "string",
        "fileName": "string",
        "file": "string",
        "birthDate": "2024-05-04",
        "gender": "string",
        "favourite": true,
        "countryId": 0,
        "stateId": 0,
        "state": { "stateId": 1, "stateName": 'state 1', "countryId": 2 },
        "country": { "countryId": 2, "countryName": 'country 1' }
      },
      message: ''

    };
    //Act
    service.getContactById(contactId).subscribe(response => {
      //Assert
      expect(response.success).toBeTrue();
      expect(response.message).toBe('');
      expect(response.data).toEqual(mockSuccessResponse.data);
      expect(response.data.contactId).toEqual(contactId);

    });
    const req = httpMock.expectOne('http://localhost:5191/api/Contactbook/GetContactsById?id=' + contactId);
    expect(req.request.method).toBe('GET');
    req.flush(mockSuccessResponse);
  });

  it('should handle failed contact retrival', () => {
    const contactId = 1;
    const mockErrorResponse: ApiResponse<Contactbook> = {
      success: false,
      data: {} as Contactbook,
      message: 'No record Found',
    };
    //Act
    service.getContactById(contactId).subscribe(response => {
      //Assert
      expect(response.success).toBeFalse();
      expect(response).toEqual(mockErrorResponse);
      expect(response.message).toEqual('No record Found');
    });
    const req = httpMock.expectOne('http://localhost:5191/api/Contactbook/GetContactsById?id=' + contactId);
    expect(req.request.method).toBe('GET');
    req.flush(mockErrorResponse);
  });

  it('should handle Http error while contact retrival', () => {
    const contactId = 1;
    const mockHttpError = {
      statusText: "Internal Server Error",
      status: 500
    }
    //Act
    service.getContactById(contactId).subscribe({
      next: () => fail('should have failed with the 500 error'),
      error: (error) => {
        //assert
        expect(error.status).toEqual(500);
        expect(error.statusText).toEqual('Internal Server Error');
      }
    });
    const req = httpMock.expectOne('http://localhost:5191/api/Contactbook/GetContactsById?id=' + contactId);
    expect(req.request.method).toBe('GET');
    req.flush({}, mockHttpError);
  });

  // update contact
  it('should update a contacts successfully', () => {
    //arrange
    const updateContact: Contactbook = {
      "contactId": 1,
      "name": "string",
      "email": "string",
      "phoneNumber": "string",
      "company": "string",
      "fileName": "string",
      "file": "string",
      "birthDate": "2024-05-04",
      "gender": "string",
      "favourite": true,
      "countryId": 0,
      "stateId": 0,
      "state": { "stateId": 1, "stateName": 'state 1', "countryId": 2 },
      "country": { "countryId": 2, "countryName": 'country 1' }
    }
    const mockSuccessResponse: ApiResponse<string> = {
      success: true,
      message: "Contact update successfully.",
      data: ""
    }
    //act
    service.updateContact(updateContact).subscribe(response => {
      //assert
      expect(response).toBe(mockSuccessResponse);
    });
    const req = httpMock.expectOne('http://localhost:5191/api/Contactbook/ModifyContact');
    expect(req.request.method).toBe('PUT');
    req.flush(mockSuccessResponse);

  });
  it('should handle failed contact update', () => {
    //arrange
    const updateContact: Contactbook = {
      "contactId": 1,
      "name": "string",
      "email": "string",
      "fileName": "string",
      "file": "string",
      "phoneNumber": "string",
      "company": "string",
      "gender": "string",
      "favourite": true,
      "countryId": 0,
      "stateId": 0,
      "birthDate": "2024-05-04",
      "state": { "stateId": 1, "stateName": 'state 1', "countryId": 2 },
      "country": { "countryId": 2, "countryName": 'country 1' }
    };
    const mockErrorResponse: ApiResponse<string> = {
      success: true,
      message: "Category already exists.",
      data: ""
    }
    //act
    service.updateContact(updateContact).subscribe(response => {
      //assert
      expect(response).toBe(mockErrorResponse);
    });
    const req = httpMock.expectOne('http://localhost:5191/api/Contactbook/ModifyContact');
    expect(req.request.method).toBe('PUT');
    req.flush(mockErrorResponse);

  });
  it('should handle Http error while adding contact', () => {
    //arrange
    const updateContact: Contactbook = {
      "contactId": 1,
      "name": "string",
      "email": "string",
      "fileName": "string",
      "file": "string",
      "phoneNumber": "string",
      "company": "string",
      "gender": "string",
      "favourite": true,
      "countryId": 0,
      "stateId": 0,
      "birthDate": "2024-05-04",
      "state": { "stateId": 1, "stateName": 'state 1', "countryId": 2 },
      "country": { "countryId": 2, "countryName": 'country 1' }
    };
    const mockHttpError = {
      statusText: "Internal Server Error",
      status: 500
    }
    //act
    service.updateContact(updateContact).subscribe({
      next: () => fail('should have failed with the 500 error'),
      error: (error) => {
        //assert
        expect(error.status).toEqual(500);
        expect(error.statusText).toEqual('Internal Server Error');
      }
    });
    const req = httpMock.expectOne('http://localhost:5191/api/Contactbook/ModifyContact');
    expect(req.request.method).toBe('PUT');
    req.flush({}, mockHttpError);

  });


  //remove contact
  it('should delete a contact by id successfully', () => {
    const contactId = 1;
    const mockSuccessResponse: ApiResponse<string> = {
      success: true,
      data: '',
      message: 'contact deleted successfully.'

    };
    //Act
    service.deleteContactById(contactId).subscribe(response => {
      //Assert
      expect(response.success).toBeTrue();
      expect(response.message).toBe('contact deleted successfully.');
      expect(response.data).toEqual(mockSuccessResponse.data);


    });
    const req = httpMock.expectOne('http://localhost:5191/api/Contactbook/RemoveContact/' + contactId);
    expect(req.request.method).toBe('DELETE');
    req.flush(mockSuccessResponse);
  });
  it('should handle failed contact deletion', () => {
    const contactId = 1;
    const mockErrorResponse: ApiResponse<string> = {
      success: false,
      data: '',
      message: 'No record Found',
    };
    //Act
    service.deleteContactById(contactId).subscribe(response => {
      //Assert
      expect(response.success).toBeFalse();
      expect(response).toEqual(mockErrorResponse);
      expect(response.message).toEqual('No record Found');
    });
    const req = httpMock.expectOne('http://localhost:5191/api/Contactbook/RemoveContact/' + contactId);
    expect(req.request.method).toBe('DELETE');
    req.flush(mockErrorResponse);
  });
  it('should handle Http error while contact deletion', () => {
    const contactId = 1;
    const mockHttpError = {
      statusText: "Internal Server Error",
      status: 500
    }
    //Act
    service.deleteContactById(contactId).subscribe({
      next: () => fail('should have failed with the 500 error'),
      error: (error) => {
        //assert
        expect(error.status).toEqual(500);
        expect(error.statusText).toEqual('Internal Server Error');
      }
    });
    const req = httpMock.expectOne('http://localhost:5191/api/Contactbook/RemoveContact/' + contactId);
    expect(req.request.method).toBe('DELETE');
    req.flush({}, mockHttpError);
  });



});
