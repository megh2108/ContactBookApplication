import { ChangeDetectorRef, Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { UserDetail } from 'src/app/models/userdetail.model';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-updateuserprofile',
  templateUrl: './updateuserprofile.component.html',
  styleUrls: ['./updateuserprofile.component.css']
})
export class UpdateuserprofileComponent implements OnInit {
  @ViewChild('imageInput') imageInput!: ElementRef;

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
  file: string = '';

  loading: boolean = false;


  constructor(
    private authService: AuthService,
    private router: Router,
    private route: ActivatedRoute,
    private cdr: ChangeDetectorRef
  ) { }

  ngOnInit(): void {
    this.route.params.subscribe((params) => {
      this.userId = params['userId'];
      this.getUserDetailsById(this.userId);
    });
  }

  getUserDetailsById(userId: number | undefined) {
    this.authService.getUserDetailById(userId).subscribe({
      next: (response) => {
        if (response.success) {
          this.userdetail = response.data;
        } else {
          console.error('Failed to fetch contact', response.message);
        }
      },
      error: (error) => {
        console.error('Failed to fetch contact', error);
      },
    });
  }

  signOut() {
    this.authService.signOut();
    this.cdr.detectChanges();
  }

  onFileChange(event: any): void {
    const file = event.target.files[0];
    if (file) {
      const fileType = file.type; // Get the MIME type of the file
      var fileSize = file.size; // Size in bytes

      if (fileType === 'image/jpeg' || fileType === 'image/png' || fileType === 'image/jpg') {

        if (fileSize > 10240) { // 10 KB in bytes
          alert('File size too large! Please upload an image smaller than 10KB.');
          this.imagefile = null;
          this.userdetail.file = '';
          this.userdetail.fileName = '';
          this.imageInput.nativeElement.value = '';
          return;
        }
        const reader = new FileReader();
        reader.onload = () => {
          this.userdetail.file = (reader.result as string).split(',')[1]; // Get only the base64 string part
          this.userdetail.fileName = file.name;
          this.imagefile = reader.result; // Store the data URL for displaying the image
        };
        reader.readAsDataURL(file);
      } else {
        // Alert user about invalid file format
        this.imageInput.nativeElement.value = '';
        alert('Invalid file format! Please upload an image in JPG, JPEG, or PNG format.');

      }
    }
  }

  removeImage(event: any) {
    if (event.target) {
      this.imagefile = null;
      this.userdetail.file = '';
      this.userdetail.fileName = '';
      this.imageInput.nativeElement.value = '';
    }
    else {
      this.userdetail.file = this.file;
      this.imagefile = 'data:image/jpeg;base64,' + this.userdetail.file;
    }
  }

  onSubmit(updateUserForm: NgForm): void {
    if (updateUserForm.valid) {
      this.loading = true;
      // console.log(updateUserForm.value);


      this.authService.updatedUser(this.userdetail).subscribe({
        next: (response) => {
          if (response.success) {
            console.log('User update successfully:', response);
            this.router.navigate(['/home']);
            this.signOut();
          } else {
            alert(response.message);
          }

          this.loading = false;
        },
        error: (err) => {
          console.error(err.error.message);
          this.loading = false;
          alert(err.error.message);
        },
        complete: () => {
          this.loading = false;
          console.log("completed");
        }

      });
    }
  }


}
