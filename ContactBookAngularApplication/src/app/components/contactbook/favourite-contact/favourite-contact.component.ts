import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ApiResponse } from 'src/app/models/ApiResponse{T}';
import { Contactbook } from 'src/app/models/contactbook.model';
import { AuthService } from 'src/app/services/auth.service';
import { ContactService } from 'src/app/services/contact.service';

@Component({
  selector: 'app-favourite-contact',
  templateUrl: './favourite-contact.component.html',
  styleUrls: ['./favourite-contact.component.css']
})
export class FavouriteContactComponent implements OnInit {
  contacts: Contactbook[] | undefined;
  contactId: number | undefined;
  loading: boolean = false;
  pageNumber: number = 1;
  pageSize: number = 2;
  totalItems: number = 0;
  totalPages: number = 0;
  alphabet: string[] = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".split('');
  distinctFirstLetters: string[] = [];
  currentLetter: string | null = null;
  sortOrder: string = 'asc';
  isAuthenticated: boolean = false;



  constructor(private contactService: ContactService, private router: Router, private cdr: ChangeDetectorRef, private authService: AuthService) { }


  ngOnInit(): void {
    this.authService.isAuthenticated().subscribe((authState: boolean) => {
      this.isAuthenticated = authState;
      this.cdr.detectChanges();   //it code trigger change detection automatically
    });
    this.loadFavouriteContactsCount();
    this.loadAllFavouriteContact();
  }

  loadAllFavouriteContact(): void {
    this.loading = true; 
    this.contactService.getAllFavouriteContacts().subscribe({
      next: (response: ApiResponse<Contactbook[]>) => {
        if (response.success) {
          console.log(response.data);
          this.processContacts(response.data);        }
        else {
          console.error('Failed to fetch contacts', response.message);
        }
        this.loading = false; 
      },
      error: (error => {
        console.error('Failed to fetch contacts', error);
        this.loading = false; 

      })

    })

  }

  processContacts(contacts: any[]): void {
    const distinctLettersSet = new Set<string>();
    contacts.forEach(contact => {
      const firstLetter = contact.name.charAt(0).toUpperCase();
      if (firstLetter) {
        distinctLettersSet.add(firstLetter);
      }
    });
    this.distinctFirstLetters = Array.from(distinctLettersSet).sort();
  }

  loadFavouriteContactsCount(): void {
    this.loading = true;
    if (this.currentLetter) {
      this.loadFavouriteSpecificContactsCount(this.currentLetter)
    } else {
      this.loadAllFavouriteContactsCount();
    }
  }

  loadAllFavouriteContactsCount(): void {
    this.loading = true;
    this.contactService.getAllFavouriteContactsCount().subscribe({
      next: (response: ApiResponse<number>) => {
        if (response.success) { 
          console.log(response.data);
          this.totalItems = response.data;
          this.totalPages = Math.ceil(this.totalItems / this.pageSize);
          this.loadAllFavouriteContacts();
        } else {
          console.error('Failed to fetch contacts count', response.message);
        }
        this.loading = false;
      },
      error: (error) => {
        console.error('Error fetching contacts count.', error);
        this.loading = false;
      }
    });
  }

  loadFavouriteSpecificContactsCount(letter: string): void {
    this.loading = true;
    this.contactService.getAllFavouriteSpecificContactsCount(letter).subscribe({
      next: (response: ApiResponse<number>) => {
        if (response.success) {
          console.log(response.data);
          this.totalItems = response.data;
          this.totalPages = Math.ceil(this.totalItems / this.pageSize);
          this.pageNumber = 1;
          this.loadFavouriteSpecificContacts(letter);
        } else {
          console.error('Failed to fetch contacts count', response.message);
        }
        this.loading = false;
      },
      error: (error) => {
        console.error('Error fetching contacts count.', error);
        this.loading = false;
      }
    });
  }

  loadFavouriteContacts(): void {
    this.loading = true;
    if (this.currentLetter) {
      this.loadFavouriteSpecificContacts(this.currentLetter)
    }
    else {
      this.loadAllFavouriteContacts();
    }
  }

  loadFavouriteSpecificContacts(letter: string): void {
    this.loading = true;
    this.contactService.getAllFavouriteSpecificContactsWithPagination(this.pageNumber, this.pageSize, letter, this.sortOrder).subscribe({
      next: (response: ApiResponse<Contactbook[]>) => {
        if (response.success) {
          console.log(response.data);
          this.contacts = response.data;
        } else {
          console.error('Failed to fetch contacts', response.message);
        }
        this.loading = false;
      },
      error: (error) => {
        console.error('Error fetching contacts.', error);
        this.loading = false;
      }
    });
  }

  loadAllFavouriteContacts(): void {
    this.loading = true;
    this.contactService.getAllFavouriteContactsWithPagination(this.pageNumber, this.pageSize, this.sortOrder).subscribe({
      next: (response: ApiResponse<Contactbook[]>) => {
        if (response.success) {
          console.log(response.data);
          this.contacts = response.data;
        } else {
          console.error('Failed to fetch contacts', response.message);
        }
        this.loading = false;
      },
      error: (error) => {
        console.error('Error fetching contacts.', error);
        this.loading = false;
      }
    });
  }

  filterByLetter(letter: string | null): void {
    this.currentLetter = letter;
    this.loadFavouriteContactsCount();
  }

  changePage(pageNumber: number): void {
    this.pageNumber = pageNumber;
    this.loadFavouriteContacts();

  }

  changePageSize(pageSize: number): void {

    this.pageSize = pageSize;
    this.pageNumber = 1; // Reset to first page
    this.totalPages = Math.ceil(this.totalItems / this.pageSize); // Recalculate total pages
    this.loadFavouriteContacts();
  }

  //asc desc sorting
  sortAsc() {
    this.sortOrder = 'asc'
    this.pageNumber = 1;
    this.loadFavouriteContacts();
  }

  sortDesc() {
    this.sortOrder = 'desc'
    this.pageNumber = 1;
    this.loadFavouriteContacts();

  }

  contactDetails(contactId: number): void {
    this.router.navigate(['/contact-detail', contactId]);
  }


  updateContact(contactId: number): void {
    this.router.navigate(['/update-contact', contactId]);
  }

  updateContactRF(contactId: number): void {
    this.router.navigate(['/update-contact-rf', contactId]);
  }

  unauthorizeDelete(): void {
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
          this.totalItems--;
          this.totalPages = Math.ceil(this.totalItems / this.pageSize);
          if (this.pageNumber > this.totalPages) {
            this.pageNumber = this.totalPages;
          }
          this.loadFavouriteContacts();
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


}
