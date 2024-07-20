import { Component, ElementRef, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { User } from 'src/app/models/user.model';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.css']
})
export class SignupComponent {
  @ViewChild('imageInput') imageInput!: ElementRef;

  imageUrl: string | ArrayBuffer | null = null; // Property to hold the data URL of the uploaded image

  user : User = {
    userId: 0,
    firstName: '',
    lastName: '',
    loginId: '',
    email: '',
    contactNumber: '',
    password: '',
    confirmPassword: '',
    fileName: '',
    file: '',
  }

  loading: boolean = false;
  file: string = '';


  constructor(private authService: AuthService, private router: Router) { }

  checkPasswords(form: NgForm): void {
    const password = form.controls['password'];
    const confirmPassword = form.controls['confirmPassword'];

    if (password && confirmPassword && password.value !== confirmPassword.value) {
      confirmPassword.setErrors({ passwordMismatch: true });
    } else {
      confirmPassword.setErrors(null);
    }
  }

  onFileChange(event: any): void {
    const file = event.target.files[0];
    if (file) {
      const fileType = file.type; // Get the MIME type of the file
      if (fileType === 'image/jpeg' || fileType === 'image/png' || fileType === 'image/jpg') {
        const reader = new FileReader();
        reader.onload = () => {
          this.user.file = (reader.result as string).split(',')[1]; // Get only the base64 string part
          this.user.fileName = file.name;
          this.imageUrl = reader.result; // Store the data URL for displaying the image
        };
        reader.readAsDataURL(file);
      } else {
        this.imageInput.nativeElement.value = '';
        alert('Invalid file format! Please upload an image in JPG, JPEG, or PNG format.');

      }
    }
  }
  removeImage(event: any) {
    if (event.target) {
      this.imageUrl = null;
      this.user.file = '';
      this.user.fileName = '';
      this.imageInput.nativeElement.value = '';
    }
    else {
      this.user.file = this.file;
      this.imageUrl = 'data:image/jpeg;base64,' + this.user.file;
    }
  }

  onSubmit(signUpForm: NgForm): void {
    if (signUpForm.valid) {
      this.loading = true;

      console.log(signUpForm.value);

      this.authService.signUp(this.user).subscribe({
        next: (response) => {
          if (response.success) {
            console.log('User added successfully:', response);
            this.router.navigate(['/signupsuccess']);
            signUpForm.resetForm();
          }else{
            alert(response.message);
          }

          this.loading = false;
        },
        error: (err) => {
          console.error(err.error.message);
          this.loading = false;
          alert(err.error.message);
        },
        complete:() =>{
          this.loading = false;
          console.log("completed");
        }

      });
    }
  }
}
