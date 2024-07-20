import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { PrivacyComponent } from './components/privacy/privacy.component';
import { ContactbookListComponent } from './components/contactbook/contactbook-list/contactbook-list.component';
import { SigninComponent } from './components/auth/signin/signin.component';
import { SignupComponent } from './components/auth/signup/signup.component';
import { AddContactComponent } from './components/contactbook/add-contact/add-contact.component';
import { UpdateContactComponent } from './components/contactbook/update-contact/update-contact.component';
import { ContactDetailsComponent } from './components/contactbook/contact-details/contact-details.component';
import { ContactListPaginationComponent } from './components/contactbook/contact-list-pagination/contact-list-pagination.component';
import { SignupsuccessComponent } from './components/auth/signupsuccess/signupsuccess.component';
import { authGuard } from './guards/auth.guard';
import { FavouriteContactComponent } from './components/contactbook/favourite-contact/favourite-contact.component';
import { ForgotpasswordComponent } from './components/auth/forgotpassword/forgotpassword.component';
import { NgbDropdownModule } from '@ng-bootstrap/ng-bootstrap';
import { ChangepasswordComponent } from './components/auth/changepassword/changepassword.component';
import { PasswordconfirmationComponent } from './components/auth/passwordconfirmation/passwordconfirmation.component';
import { UpdateuserprofileComponent } from './components/auth/updateuserprofile/updateuserprofile.component';
import { AddContactRfComponent } from './components/contactbook/add-contact-rf/add-contact-rf.component';
import { UpdateContactRfComponent } from './components/contactbook/update-contact-rf/update-contact-rf.component';
import { ContactReportComponent } from './components/contactbook/contact-report/contact-report.component';


const routes: Routes = [
  {path:'',redirectTo:'home',pathMatch:'full'},
  {path:'home',component:HomeComponent},
  {path:'privacy',component:PrivacyComponent},
  {path:'contacts',component:ContactbookListComponent},
  {path:'contacts-pagination',component:ContactListPaginationComponent},
  {path:'favourite-contacts-pagination',component:FavouriteContactComponent},
  {path:'signin',component:SigninComponent},
  {path:'signup',component:SignupComponent},
  {path:'signupsuccess',component:SignupsuccessComponent},
  {path:'forgotpassword',component:ForgotpasswordComponent},  
  {path:'changepassword',component:ChangepasswordComponent},  
  {path:'update-user/:userId',component:UpdateuserprofileComponent},
  {path:'passwordconfirmation',component:PasswordconfirmationComponent},  
  {path:'add-contact',component:AddContactComponent, canActivate: [authGuard]},
  {path:'add-contact-rf',component:AddContactRfComponent, canActivate: [authGuard]},
  {path:'update-contact/:contactId',component:UpdateContactComponent, canActivate: [authGuard]},
  {path:'update-contact-rf/:contactId',component:UpdateContactRfComponent, canActivate: [authGuard]},
  {path:'contact-detail/:contactId',component:ContactDetailsComponent},
  {path:'contact-report',component:ContactReportComponent},



];

@NgModule({
  imports: [RouterModule.forRoot(routes),NgbDropdownModule],
  exports: [RouterModule]
})
export class AppRoutingModule { }
