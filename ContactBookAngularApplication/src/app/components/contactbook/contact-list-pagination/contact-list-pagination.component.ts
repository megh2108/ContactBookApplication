import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ApiResponse } from 'src/app/models/ApiResponse{T}';
import { Contactbook } from 'src/app/models/contactbook.model';
import { AuthService } from 'src/app/services/auth.service';
import { ContactService } from 'src/app/services/contact.service';

@Component({
  selector: 'app-contact-list-pagination',
  templateUrl: './contact-list-pagination.component.html',
  styleUrls: ['./contact-list-pagination.component.css']
})
export class ContactListPaginationComponent implements OnInit {
  contacts: Contactbook[] | undefined ;
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

  searchQuery: string = '';



  constructor(private contactService: ContactService, private router: Router, private cdr: ChangeDetectorRef, private authService: AuthService) { }


  ngOnInit(): void {
    this.authService.isAuthenticated().subscribe((authState: boolean) => {
      this.isAuthenticated = authState;
      this.cdr.detectChanges();   //it code trigger change detection automatically
    });
    this.loadContactsCount();
    this.loadAllContact();
  }

  loadAllContact(): void {
    this.loading = true; 
    this.contactService.getAllContacts().subscribe({
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

  loadContactsCount(): void {
    this.loading = true;
    if (this.currentLetter) {
      this.loadSpecificContactsCount(this.currentLetter, this.searchQuery)
    } else {
      this.loadAllContactsCount(this.searchQuery);
    }
  }

  loadAllContactsCount(searchQuery: string): void {
    this.contactService.getAllContactsCount(searchQuery).subscribe({
      next: (response: ApiResponse<number>) => {
        if (response.success) {
          console.log(response.data);
          this.totalItems = response.data;
          this.totalPages = Math.ceil(this.totalItems / this.pageSize);
          this.loadAllContacts(searchQuery);
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

  loadSpecificContactsCount(letter: string, searchQuery: string): void {
    this.contactService.getAllSpecificContactsCount(letter, searchQuery).subscribe({
      next: (response: ApiResponse<number>) => {
        if (response.success) {
          console.log(response.data);
          this.totalItems = response.data;
          this.totalPages = Math.ceil(this.totalItems / this.pageSize);
          this.pageNumber = 1;
          this.loadSpecificContacts(letter, searchQuery);
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

  loadContacts(): void {
    this.loading = true;
    if (this.currentLetter) {
      this.loadSpecificContacts(this.currentLetter, this.searchQuery)
    }
    else {
      this.loadAllContacts(this.searchQuery);
    }
  }

  loadSpecificContacts(letter: string, searchQuery: string): void {
    this.loading = true;
    this.contactService.getAllSpecificContactsWithPagination(this.pageNumber, this.pageSize, letter, this.sortOrder, searchQuery).subscribe({
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

  loadAllContacts(searchQuery: string): void {
    this.loading = true;
    this.contactService.getAllContactsWithPagination(this.pageNumber, this.pageSize, this.sortOrder, searchQuery).subscribe({
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
  onSearch() {
    this.pageNumber = 1;
    if (this.currentLetter) {
      this.loadSpecificContactsCount(this.currentLetter, this.searchQuery);
    } else {
      this.loadAllContactsCount(this.searchQuery);

    }
    // this.getAllContactsWithLetter(this.letter, this.searchQuery);
    // this.getAllContactsCountWithLetter(this.letter, this.searchQuery);
  }
  clearSearch() {
    this.searchQuery = '';
    this.pageNumber = 1;
    this.loadContactsCount();
  }

  filterByLetter(letter: string | null): void {

    if (letter !== null && this.isSelected(letter)) {
      this.currentLetter = null;
    } else {
      this.currentLetter = letter;
      // 
    }
    this.pageNumber = 1;
    this.loadContactsCount();
  }

  isSelected(letter: string): boolean {
    return this.currentLetter === letter || (!this.currentLetter && !letter);
  }

  changePage(pageNumber: number): void {
    this.pageNumber = pageNumber;
    this.loadContacts();

  }

  changePageSize(pageSize: number): void {

    this.pageSize = pageSize;
    this.pageNumber = 1; // Reset to first page
    this.totalPages = Math.ceil(this.totalItems / this.pageSize); // Recalculate total pages
    this.loadContacts();
  }

  //sorting asc, desc
  sortAsc() {
    this.sortOrder = 'asc'
    this.pageNumber = 1;
    this.loadContacts();
  }

  sortDesc() {
    this.sortOrder = 'desc'
    this.pageNumber = 1;
    this.loadContacts();

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




}
