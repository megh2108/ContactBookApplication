import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';


import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './components/home/home.component';
import { PrivacyComponent } from './components/privacy/privacy.component';
import { SigninComponent } from './components/auth/signin/signin.component';
import { SignupComponent } from './components/auth/signup/signup.component';
import { ContactbookListComponent } from './components/contactbook/contactbook-list/contactbook-list.component';
import { AddContactComponent } from './components/contactbook/add-contact/add-contact.component';
import { UpdateContactComponent } from './components/contactbook/update-contact/update-contact.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ContactDetailsComponent } from './components/contactbook/contact-details/contact-details.component';
import { ContactListPaginationComponent } from './components/contactbook/contact-list-pagination/contact-list-pagination.component';
import { SignupsuccessComponent } from './components/auth/signupsuccess/signupsuccess.component';
import { AuthService } from './services/auth.service';
import { AuthInterceptor } from './interceptors/auth.interceptor';
import { FavouriteContactComponent } from './components/contactbook/favourite-contact/favourite-contact.component';
import { ForgotpasswordComponent } from './components/auth/forgotpassword/forgotpassword.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { ChangepasswordComponent } from './components/auth/changepassword/changepassword.component';
import { PasswordconfirmationComponent } from './components/auth/passwordconfirmation/passwordconfirmation.component';
import { UpdateuserprofileComponent } from './components/auth/updateuserprofile/updateuserprofile.component';
import { HeaderComponent } from './components/shared/header/header.component';
import { AddContactRfComponent } from './components/contactbook/add-contact-rf/add-contact-rf.component';
import { UpdateContactRfComponent } from './components/contactbook/update-contact-rf/update-contact-rf.component';
import { DatePipe } from '@angular/common';
import { ContactReportComponent } from './components/contactbook/contact-report/contact-report.component';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    PrivacyComponent,
    ContactbookListComponent,
    SigninComponent,
    SignupComponent,
    AddContactComponent,
    UpdateContactComponent,
    ContactDetailsComponent,
    ContactListPaginationComponent,
    SignupsuccessComponent,
    FavouriteContactComponent,
    ForgotpasswordComponent,
    ChangepasswordComponent,
    PasswordconfirmationComponent,
    UpdateuserprofileComponent,
    HeaderComponent,
    AddContactRfComponent,
    UpdateContactRfComponent,
    ContactReportComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule, 
    AppRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    NgbModule
  ],
  providers: [DatePipe,AuthService ,{provide: HTTP_INTERCEPTORS, useClass : AuthInterceptor  , multi: true}],
  bootstrap: [AppComponent]
})
export class AppModule { }
