import { Component } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';
import { LocalstorageService } from 'src/app/services/helpers/localstorage.service';
import { localStorageKeys } from 'src/app/services/helpers/localstoragekeys';

@Component({
  selector: 'app-changepassword',
  templateUrl: './changepassword.component.html',
  styleUrls: ['./changepassword.component.css']
})
export class ChangepasswordComponent {
  loading: boolean = false;
  username : string | null | undefined = '';
  password: string = '';
  confirmPassword : string = '';


  constructor(
    private authService: AuthService,
    private router: Router,
    private localStorageHelper: LocalstorageService,

  ) { }

  checkPasswords(form: NgForm): void {
    const password = form.controls['password'];
    const confirmPassword = form.controls['confirmPassword'];

    if (password && confirmPassword && password.value !== confirmPassword.value) {
      confirmPassword.setErrors({ passwordMismatch: true });
    } else {
      confirmPassword.setErrors(null);
    }
  }

  changepassword(): void {
    this.username = this.localStorageHelper.getItem(localStorageKeys.LoginId);
    this.authService.changepassword(this.username, this.password,this.confirmPassword).subscribe({
      next: (response) => {
        if (response.success) {
          this.router.navigate(['/passwordconfirmation']);
          this.authService.signOut();

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
