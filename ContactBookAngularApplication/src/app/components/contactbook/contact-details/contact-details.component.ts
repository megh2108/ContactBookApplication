import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Contactbook } from 'src/app/models/contactbook.model';
import { ContactService } from 'src/app/services/contact.service';

@Component({
  selector: 'app-contact-details',
  templateUrl: './contact-details.component.html',
  styleUrls: ['./contact-details.component.css']
})
export class ContactDetailsComponent implements OnInit {

  contactId: number | undefined;
  loading: boolean = false;
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
    countryId: 0,
    stateId: 0,
    country: {
      countryId: 0,
      countryName: ''
    },
    state: {
      stateId: 0,
      stateName: '',
      countryId: 0
    }

  };

  constructor(private contactService: ContactService, private router: Router,private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.route.params.subscribe((params) => {
      this.contactId = params['contactId'];
      this.loadContactDetail(this.contactId);
    });
  }

  updateContact(contactId: number): void {
    this.router.navigate(['/update-contact', contactId]);
  }

  loadContactDetail(contactId: number | undefined): void {
    this.loading = true;

    this.contactService.getContactById(contactId).subscribe({
      next: (response) => {
        if (response.success) {
          console.log("contact data", response.data);
          this.contact = response.data;
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

  returnGenderName(gender: string): string {
    return gender === 'M' ? 'Male' : gender === 'F' ? 'Female' : 'Unknown'
  }



}
