import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ApiResponse } from 'src/app/models/ApiResponse{T}';
import { Contactbook } from 'src/app/models/contactbook.model';
import { AuthService } from 'src/app/services/auth.service';
import { ContactService } from 'src/app/services/contact.service';

@Component({
  selector: 'app-contactbook-list',
  templateUrl: './contactbook-list.component.html',
  styleUrls: ['./contactbook-list.component.css']
})
export class ContactbookListComponent implements OnInit {
  //create empty array
  contacts: Contactbook[] | undefined;
  contactId: number | undefined;
  loading: boolean = false;
  isAuthenticated: boolean = false;


  //inject dependecy here
  constructor(private contactService: ContactService,private router: Router,private cdr: ChangeDetectorRef,private authService: AuthService) { }

  //consider it is a load evenet or constructor
  ngOnInit(): void {
    this.authService.isAuthenticated().subscribe((authState: boolean) => {
      this.isAuthenticated = authState;
      this.cdr.detectChanges();   //it code trigger change detection automatically
    });
    this.loadContacts();
  }

  loadContacts(): void {
    this.loading = true; //set loading to true before data is loaded
    this.contactService.getAllContacts().subscribe({
      next: (response: ApiResponse<Contactbook[]>) => {
        if (response.success) {
          console.log(response.data);
          this.contacts = response.data;
        }
        else {
          console.error('Falied to fetch categories', response.message);
        }
        this.loading = false; //set loading to false after data is loaded
      },
      error: (error => {
        console.error('Error fetching categories.', error);
        this.loading = false; //set loading to false after data is loaded

      })

    })

  }

  contactDetails(contactId: number): void {
    this.router.navigate(['/contact-detail', contactId]);
  }


  updateContact(contactId: number): void {
    this.router.navigate(['/update-contact', contactId]);
  }

  unauthorizeDelete() : void{
    this.router.navigate(['/signin']);

  }
  confirmDelete(contactId: number): void {
    if (confirm('Are you sure want to delete ?')) {
      // alert('yes');
      this.loading = true;
      this.contactId = contactId;
      this.deleteContact();
    }

  }

  deleteContact(): void {

    this.contactService.deleteContactById(this.contactId).subscribe({
      next: (response) => {
        if (response.success) {
          // alert(response.message);
          this.loadContacts();
          
        } else {
          alert(response.message);
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

  returnFileName(fileName: string): string {
    return fileName === '' ? 'defaultimage.png' : fileName
  }

}
