import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, ValidationErrors, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ApiResponse } from 'src/app/models/ApiResponse{T}';
import { ContactCountry } from 'src/app/models/contact.country.model';
import { ContactState } from 'src/app/models/contact.state.model';
import { ContactService } from 'src/app/services/contact.service';
import { CountryService } from 'src/app/services/country.service';
import { StateService } from 'src/app/services/state.service';

@Component({
  selector: 'app-add-contact-rf',
  templateUrl: './add-contact-rf.component.html',
  styleUrls: ['./add-contact-rf.component.css']
})
export class AddContactRfComponent implements OnInit {

  imageUrl: string | ArrayBuffer | null = null;
  loading: boolean = false;

  country: ContactCountry[] = [];
  state: ContactState[] = [];
  contactForm!: FormGroup;
  @ViewChild('imageInput') imageInput!: ElementRef;
  
fileSizeExceeded =  false;
fileFormatInvalid =  false;

  constructor(
    private contactService: ContactService,
    private countryService: CountryService, 
    private stateService: StateService, 
    private router: Router,
    private fb: FormBuilder
  ) {}

  ngOnInit(): void {
    this.contactForm = this.fb.group({

      name: ['',[Validators.required, Validators.minLength(2)]],
      company: ['', Validators.required],
      phoneNumber: ['',[Validators.required, Validators.minLength(10), Validators.maxLength(10)]],
      countryId : [0, [Validators.required, this.contactValidator]],
      stateId : [0, [Validators.required, this.contactValidator]],
      email: ['',[Validators.required, Validators.email]],
      gender: [, Validators.required],
      favourite: [false],
      file: [''],
      fileName: [null],
      birthDate: ['',[this.validateBirthdate]]

    })
    this.loadCountries();
    this.fetchStateByCountry();

     }

     get formControl(){
      return this.contactForm.controls;
     }
  
     contactValidator(control: any){
      return control.value ==''? {invalidContact:true}:null;
     }

     validateBirthdate(control: AbstractControl): ValidationErrors | null {
      const selectedDate = new Date(control.value);
      const currentDate = new Date();
    
      // Set hours, minutes, seconds, and milliseconds to 0 to compare only the date part
      selectedDate.setHours(0, 0, 0, 0);
      currentDate.setHours(0, 0, 0, 0);
    
      if (selectedDate > currentDate) {
        return { invalidBirthDate: true };
      }
      return null;
     }

     loadCountries():void{
      this.loading = true;
      this.countryService.getAllCountries().subscribe({
        next:(response: ApiResponse<ContactCountry[]>) =>{
          if(response.success){
            this.country = response.data;
          }
          else{
            console.error('Failed to fetch countries', response.message);
          }
          this.loading = false;
        },
        error:(error => {
          console.error('Failed to fetch countries', error);
          this.loading = false;
        })
      }
    )
    }

    fetchStateByCountry(): void {
      this.contactForm.get('countryId')?.valueChanges.subscribe((countryId: number) => {
        if (countryId !== 0) {
        this.state = [];
        this.contactForm.get('stateId')?.setValue(null); // Reset the state control's value to null

          this.loading = true;
          this.stateService.getStatesByCountryId(countryId).subscribe({
            next: (response: ApiResponse<ContactState[]>) => {
              if (response.success) {
                this.state = response.data;
              } else {
                console.error('Failed to fetch states', response.message);
              }
              this.loading = false;
            },
            error: (error) => {
              console.error('Failed to fetch states', error);
              this.loading = false;
            }
          });
        }
      });
    }
    removeFile() {
      this.contactForm.patchValue({
        imageUrl: null,
        fileName: null
      });
      this.imageUrl = null; // Also reset imageUrl to remove the displayed image
      const inputElement = document.getElementById('file') as HTMLInputElement;
      inputElement.value = '';
       
    }
    
    onFileChange(event: any): void {
      const file = event.target.files[0];

    

      if (file) {
        const fileType = file.type; // Get the MIME type of the file
        if (fileType === 'image/jpeg' || fileType === 'image/png' || fileType === 'image/jpg')
           {
            if(file.size > 50 * 1024)
              {
                this.fileSizeExceeded = true; // Set flag to true if file size exceeds the limit
                const inputElement = document.getElementById('file') as HTMLInputElement;
                inputElement.value = '';
                return;
              }
              this.fileFormatInvalid = false;
              this.fileSizeExceeded = false; 

        const reader = new FileReader();
        reader.onload = () => {
          this.contactForm.patchValue({
            file: (reader.result as string).split(',')[1],
            fileName: file.name
          });
          this.imageUrl = reader.result;
        };
        reader.readAsDataURL(file);
      }
      else {
        this.fileFormatInvalid = true;
        const inputElement = document.getElementById('file') as HTMLInputElement;
        inputElement.value = '';

         
      }

     
    }
  }

    OnSubmit(){
      this.loading = true;
  
      if(this.contactForm.valid){
       if(this.imageUrl === null)
        {
          this.contactForm.patchValue({
              file : '',
              fileName: null
          })
        }
        console.log(this.contactForm.value);
        this.contactService.addContact(this.contactForm.value).subscribe({
          next: (response) => {
            if (response.success) {
              console.log("Contact created successfully:", response);
              this.router.navigate(['/contacts-pagination']);
            }
            else{
              this.imageUrl = null;
              alert(response.message)
            }
          },
          error:(err)=>{
            alert(err.error.message);
            this.loading = false;
  
          },
          complete:()=>{
            console.log("Completed");
            this.loading = false;
  
          }
        })
      }
    }
}
