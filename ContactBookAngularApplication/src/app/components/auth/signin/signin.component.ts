import { ChangeDetectorRef, Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';
import { LocalstorageService } from 'src/app/services/helpers/localstorage.service';
import { localStorageKeys } from 'src/app/services/helpers/localstoragekeys';

@Component({
  selector: 'app-signin',
  templateUrl: './signin.component.html',
  styleUrls: ['./signin.component.css']
})
export class SigninComponent {
  loading: boolean = false;
  username: string = '';
  password: string = '';


  constructor(
    private authService: AuthService,
    private localStorageHelper: LocalstorageService,
    private router: Router,
    private cdr: ChangeDetectorRef
  ) { }

  login(): void {
    this.authService.signIn(this.username, this.password).subscribe({
      next: (response) => {
        if (response.success) {
          this.cdr.detectChanges();
          this.router.navigate(['/home']);
        } else {
          alert(response.message);
        }
        this.loading = false;
      },
      error: (err) => {
        this.loading = false;
        alert(err.error.message);
      }
    });
  }
}
