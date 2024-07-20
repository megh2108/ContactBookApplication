import { Component, ElementRef, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ApiResponse } from 'src/app/models/ApiResponse{T}';
import { ContactCountry } from 'src/app/models/contact.country.model';
import { ContactState } from 'src/app/models/contact.state.model';
import { Contactbook } from 'src/app/models/contactbook.model';
import { ContactService } from 'src/app/services/contact.service';
import { CountryService } from 'src/app/services/country.service';
import { StateService } from 'src/app/services/state.service';

@Component({
  selector: 'app-update-contact',
  templateUrl: './update-contact.component.html',
  styleUrls: ['./update-contact.component.css']
})
export class UpdateContactComponent {
  @ViewChild('imageInput') imageInput!: ElementRef;

  contactId: number | undefined;
  imageUrl: string | ArrayBuffer | null = null; // Property to hold the data URL of the uploaded image

  contact: Contactbook = {
    contactId: 0,
    name: '',
    email: '',
    phoneNumber: '',
    company: '',
    fileName: '',
    file: '',
    birthDate: '',
    gender: '',
    favourite: false,
    countryId: null,
    stateId: null,
    country: {
      countryId: 0,
      countryName: ''
    },
    state: {
      stateId: 0,
      stateName: '',
      countryId: 0
    }
  }

  countries: ContactCountry[] = [];
  states: ContactState[] = [];

  file: string = '';

  loading: boolean = false;

  constructor(private contactService: ContactService, private router: Router, private route: ActivatedRoute, private countryService: CountryService, private stateService: StateService) { }

  ngOnInit(): void {
    this.route.params.subscribe((params) => {
      this.contactId = params['contactId'];
      this.loadContactDetail(this.contactId);
      this.loadCountries();

    });
  }
  formatDate(date: Date): string {
    const year = date.getFullYear();
    const month = ('0' + (date.getMonth() + 1)).slice(-2);
    const day = ('0' + date.getDate()).slice(-2);
    return `${year}-${month}-${day}`;
  }


  loadCountries(): void {
    this.loading = true;
    this.countryService.getAllCountries().subscribe({
      next: (response: ApiResponse<ContactCountry[]>) => {
        if (response.success) {
          this.countries = response.data;
        }
        else {
          console.error('Failed to fetch countries', response.message);
        }
        this.loading = false;
      },
      error: (error => {
        console.error('Failed to fetch countries', error);
        this.loading = false;
      })
    });
  }
  onCountryChanage(): void {
    this.contact.stateId = null; // Reset the state selection
    this.states = []; // Clear the states list
    if (this.contact.countryId) {
      this.loadStatesByCountryId(this.contact.countryId);
    } 
    // else {
    //   this.states = [];
    // }
  }

  loadStatesByCountryId(countryId: number): void {
    this.loading = true;
    this.stateService.getStatesByCountryId(countryId).subscribe({
      next: (response: ApiResponse<ContactState[]>) => {
        if (response.success) {
          this.states = response.data;
        }
        else {
          console.error('Failed to fetch states', response.message);
        }
        this.loading = false;
      },
      error: (error => {
        console.error('Failed to fetch states', error);
        this.loading = false;
      })
    });
  }

  loadContactDetail(contactId: number | undefined): void {
    this.loading = true;
    this.contactService.getContactById(contactId).subscribe({
      next: (response) => {
        if (response.success) {
          console.log(response.data);
          this.contact = response.data;
          this.contact.birthDate = this.formatDate(new Date(this.contact.birthDate));
          this.loadStatesByCountryId(this.contact.country.countryId); // this for load state of country
        } else {
          console.error("Falied to fetch contact", response.message);
        }
      },
      error: (err) => {
        this.loading = false;
        alert(err.error.message);
      },
      complete: () => {
        this.loading = false;
        console.log("Completed");
      }
    })
  }


  onFileChange(event: any): void {
    const file = event.target.files[0];
    if (file) {
      const fileType = file.type; // Get the MIME type of the file
      if (fileType === 'image/jpeg' || fileType === 'image/png' || fileType === 'image/jpg') {
        const reader = new FileReader();
        reader.onload = () => {
          this.contact.file = (reader.result as string).split(',')[1]; // Get only the base64 string part
          this.contact.fileName = file.name;
          this.imageUrl = reader.result; // Store the data URL for displaying the image
        };
        reader.readAsDataURL(file);
      } else {
        // Alert user about invalid file format
        this.imageInput.nativeElement.value = '';
        alert('Invalid file format! Please upload an image in JPG, JPEG, or PNG format.');

      }
    }
  }

  removeImage(event: any) {
    if (event.target) {
      this.imageUrl = null;
      this.contact.file = '';
      this.contact.fileName = '';
      this.imageInput.nativeElement.value = '';
    }
    else {
      this.contact.file = this.file;
      this.imageUrl = 'data:image/jpeg;base64,' + this.contact.file;
    }
  }


  onSubmit(updateContactForm: NgForm): void {
    if (updateContactForm.valid) {
      this.loading = true;
      console.log(updateContactForm.value);


      this.contactService.updateContact(this.contact).subscribe({
        next: (response) => {
          if (response.success) {
            console.log('Contact update successfully:', response);
            this.router.navigate(['/contacts-pagination']);
          } else {
            alert(response.message);
          }

          this.loading = false;
        },
        error: (err) => {
          console.error(err.error.message);
          this.loading = false;
          alert(err.error.message);
        },
        complete: () => {
          this.loading = false;
          console.log("completed");
        }

      });
    }
  }
}
