import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ApiResponse } from 'src/app/models/ApiResponse{T}';
import { ContactCountry } from 'src/app/models/contact.country.model';
import { ContactState } from 'src/app/models/contact.state.model';
import { ContactbookSP } from 'src/app/models/contactbooksp.model';
import { AuthService } from 'src/app/services/auth.service';
import { ContactService } from 'src/app/services/contact.service';
import { CountryService } from 'src/app/services/country.service';
import { StateService } from 'src/app/services/state.service';

@Component({
  selector: 'app-contact-report',
  templateUrl: './contact-report.component.html',
  styleUrls: ['./contact-report.component.css']
})
export class ContactReportComponent implements OnInit {

  loading: boolean = false;
  isAuthenticated: boolean = false;

  totalCountsFromCountry: number | null= null;
  countryId: number = 0;
  countries: ContactCountry[] = [];
  
  contactdetails: ContactbookSP[] | null= null;
  stateId: number = 0;
  states: ContactState[] = [];
  
  totalCountsFromGender: number | null= null;
  gender : string = "";

  monthId: number = 0;
  months : string[] = [ 
    "January", "February", "March", "April", "May", "June",
    "July", "August", "September", "October", "November", "December"
  ]

  constructor(
      private authService: AuthService,
      private contactService: ContactService,
      private countryService: CountryService,
      private stateService: StateService,
      private router: Router,
      private cdr: ChangeDetectorRef) { }

  ngOnInit(): void {
    // this.authService.isAuthenticated().subscribe((authState: boolean) => {
    //   this.isAuthenticated = authState;
    //   this.cdr.detectChanges();   
    // });

    // this.loadBookCountWithDateOrStudent();
    // // this.loadUserCount();
    // this.loadAllIssueBooks(this.userId, this.selectedDate);
    this.loadCountries();
    this.loadStates();
    // this.loadAllContactsByStatesSP(this.stateId);
  }

  onCountryChange(event: Event): void {
    console.log('countryId:', this.countryId);

    this.stateId = 0;
    this.gender = "";
    this.monthId= 0;

    this.totalCountsFromGender = null;
    this.contactdetails = null;

    this.loadAllContactsCountByCountrySP(this.countryId);
  }

  onStateChange(event: Event): void {
    console.log('stateId:', this.stateId);

    this.countryId = 0;
    this.gender = "";
    this.monthId= 0;

    this.totalCountsFromCountry = null;
    this.totalCountsFromGender = null;
    this.contactdetails = null;

    this.loadAllContactsByStatesSP(this.stateId);

  }

  onGenderChange(event: Event): void {
    console.log('gender:', this.gender);

    this.stateId = 0;
    this.monthId = 0;
    this.monthId= 0;

    this.totalCountsFromCountry = null;
    this.contactdetails = null;

    this.loadAllContactsCountByGenderSP(this.gender);

  }

  onMonthChange(event: Event): void {
    console.log('monthId:', this.monthId);

    this.stateId = 0;
    this.gender = "";
    this.countryId= 0;

    this.totalCountsFromCountry = null;
    this.totalCountsFromGender = null;
    this.contactdetails = null;

    this.loadAllContactsByBirthdayMonth(this.monthId);


  }

  loadAllContactsCountByCountrySP(countryId: number): void {
    this.loading = true;
    this.contactService.getAllContactsCountByCountrySP(countryId).subscribe({
      next: (response: ApiResponse<number>) => {
        if (response.success) {
          console.log(response.data);
          this.totalCountsFromCountry = response.data;
        }
        else {
          this.totalCountsFromCountry = 0;
          console.error('Failed to fetch count', response.message);
        }
        this.loading = false;
      },
      error: (err) => {
        this.totalCountsFromCountry = 0;
        this.loading = false;
        console.error(err.error.message);
        this.cdr.detectChanges();

      },
      complete: () => {
        this.loading = false;
        console.log("Completed");
      }

    })

  }

  loadAllContactsByStatesSP(stateId: number): void {
    this.loading = true;
    this.contactService.getAllContactsByStatesSP(stateId).subscribe({
      next: (response: ApiResponse<ContactbookSP[]>) => {
        if (response.success) {
          console.log(response.data);
          this.contactdetails = response.data;
        }
        else {
          this.contactdetails = [];
          console.error('Failed to fetch count', response.message);
        }
        this.loading = false;
      },
      error: (err) => {
        this.contactdetails = [];
        this.loading = false;
        console.error(err.error.message);
        this.cdr.detectChanges();

      },
      complete: () => {
        this.loading = false;
        console.log("Completed");
      }

    })

  }

  loadAllContactsCountByGenderSP(gender: string): void {
    this.loading = true;
    this.contactService.getAllContactsCountByGenderSP(gender).subscribe({
      next: (response: ApiResponse<number>) => {
        if (response.success) {
          console.log(response.data);
          this.totalCountsFromGender = response.data;
        }
        else {
          this.totalCountsFromGender = 0;
          console.error('Failed to fetch count', response.message);
        }
        this.loading = false;
      },
      error: (err) => {
        this.totalCountsFromGender = 0;
        this.loading = false;
        console.error(err.error.message);
        this.cdr.detectChanges();

      },
      complete: () => {
        this.loading = false;
        console.log("Completed");
      }

    })

  }

  loadAllContactsByBirthdayMonth(monthId: number): void {
    this.loading = true;
    this.contactService.getAllContactsByBirthdayMonth(monthId).subscribe({
      next: (response: ApiResponse<ContactbookSP[]>) => {
        if (response.success) {
          console.log(response.data);
          this.contactdetails = response.data;
        }
        else {
          this.contactdetails = [];
          console.error('Failed to fetch count', response.message);
        }
        this.loading = false;
      },
      error: (err) => {
        this.contactdetails = [];
        this.loading = false;
        console.error(err.error.message);
        this.cdr.detectChanges();

      },
      complete: () => {
        this.loading = false;
        console.log("Completed");
      }

    })

  }

  loadCountries(): void {
    this.loading = true;
    this.countryService.getAllCountries().subscribe({
      next: (response: ApiResponse<ContactCountry[]>) => {
        if (response.success) {
          this.countries = response.data;
        }
        else {
          this.countries = [];
          console.error('Failed to fetch users', response.message);
        }
        this.loading = false;
      },
      error: (err) => {
        this.loading = false;
        this.countries = [];
        console.error(err.error.message);
        this.cdr.detectChanges();

      },
      complete: () => {
        this.loading = false;
        console.log("Completed");
      }
    });
  }

  loadStates(): void {
    this.loading = true;
    this.stateService.getAllStates().subscribe({
      next: (response: ApiResponse<ContactState[]>) => {
        if (response.success) {
          this.states = response.data;
        }
        else {
          this.states = [];
          console.error('Failed to fetch users', response.message);
        }
        this.loading = false;
      },
      error: (err) => {
        this.loading = false;
        this.states = [];
        console.error(err.error.message);
        this.cdr.detectChanges();

      },
      complete: () => {
        this.loading = false;
        console.log("Completed");
      }
    });
  }

}
