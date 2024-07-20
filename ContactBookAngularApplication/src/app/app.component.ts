import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { AuthService } from './services/auth.service';
import { LocalstorageService } from './services/helpers/localstorage.service';
import { localStorageKeys } from './services/helpers/localstoragekeys';
import { UserDetail } from './models/userdetail.model';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent {
  title = 'ContactBookAngularApplication';

 


}
