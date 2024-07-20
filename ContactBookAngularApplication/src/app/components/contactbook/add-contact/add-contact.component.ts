import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { ApiResponse } from 'src/app/models/ApiResponse{T}';
import { AddContact } from 'src/app/models/addcontact.model';
import { ContactCountry } from 'src/app/models/contact.country.model';
import { ContactState } from 'src/app/models/contact.state.model';
import { ContactService } from 'src/app/services/contact.service';
import { CountryService } from 'src/app/services/country.service';
import { StateService } from 'src/app/services/state.service';

@Component({
  selector: 'app-add-contact',
  templateUrl: './add-contact.component.html',
  styleUrls: ['./add-contact.component.css']
})
export class AddContactComponent implements OnInit {
  @ViewChild('imageInput') imageInput!: ElementRef;
  loading: boolean = false;
  imageUrl: string | ArrayBuffer | null = null; // Property to hold the data URL of the uploaded image

  contact: AddContact = {
    name: '',
    email: '',
    phoneNumber: '',
    company: '',
    fileName: '',
    file: '',
    birthDate: '',
    gender: '',
    favourite: false,
    stateId: null,
    countryId: null
  };

  countries: ContactCountry[] = [];
  states: ContactState[] = [];


  file: string = '';

  constructor(private contactService: ContactService, private router: Router, private countryService: CountryService, private stateService: StateService) { }

  ngOnInit(): void {
    this.loadCountries();
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
    //   this.contact.stateId = null;
    // }
  }

  loadStatesByCountryId(countryId: number): void {
    this.loading = true;
    this.stateService.getStatesByCountryId(countryId).subscribe({
      next: (response: ApiResponse<ContactState[]>) => {
        if (response.success) {
          this.states = response.data;
          // this.contact.stateId = this.states[0].stateId;
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
  onSubmit(addContactForm: NgForm): void {
    if (addContactForm.valid) {
      this.loading = true;
      console.log(addContactForm.value);
      this.contactService.addContact(this.contact).subscribe({
        next: (response) => {
          console.log("res" + response);
          if (response.success) {
            console.log('Contact added successfully:', response);
            this.router.navigate(['/contacts-pagination']);
            // addContactForm.resetForm();
            this.imageUrl = null; // Reset the image URL after form submission
          } else {
            alert(response.message);
          }

          this.loading = false;
        },
        error: (err) => {
          // console.log(err);
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
