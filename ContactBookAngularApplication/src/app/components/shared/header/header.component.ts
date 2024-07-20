import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { UserDetail } from 'src/app/models/userdetail.model';
import { AuthService } from 'src/app/services/auth.service';
import { LocalstorageService } from 'src/app/services/helpers/localstorage.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit  {
  isAuthenticated: boolean = false;
  username: string | undefined | null;
  userIdString: string | undefined | null;
  userId: number | undefined;
  imagefile: string | ArrayBuffer | null = null;


  userdetail: UserDetail = {
    userId: 0,
    firstName: '',
    lastName: '',
    loginId: '',
    email: '',
    contactNumber: '',
    fileName: '',
    file: ''
  };



  constructor(private authService: AuthService, private cdr: ChangeDetectorRef, private localStorageHelper: LocalstorageService) { }

  ngOnInit(): void {

    this.authService.getUserId().subscribe((userIdString: string | null | undefined) => {
      this.userIdString = userIdString;
      if (userIdString != null && userIdString != undefined) {
        this.userId = Number(userIdString);
        this.getUserDetailsById(this.userId);
      }
      this.cdr.detectChanges();  //it code trigger change detection automatically
    });

    this.authService.isAuthenticated().subscribe((authState: boolean) => {
      this.isAuthenticated = authState;
      this.cdr.detectChanges();   //it code trigger change detection automatically
    });

    this.authService.getUserName().subscribe((username: string | null | undefined) => {
      this.username = username;
      this.cdr.detectChanges();  //it code trigger change detection automatically
    });


  }

 
  signOut() {
    this.authService.signOut();
    this.cdr.detectChanges();
  }

  getUserDetailsById(userId: number | undefined) {
    this.authService.getUserDetailById(userId).subscribe({
      next: (response) => {
        if (response.success) {
          this.userdetail = response.data;
          this.imagefile = this.userdetail.file;
        } else {
          console.error('Failed to fetch contact', response.message);
        }
      },
      error: (error) => {
        console.error('Failed to fetch contact', error);
      },
    });
  }
}
