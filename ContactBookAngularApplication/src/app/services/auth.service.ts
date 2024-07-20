import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ApiResponse } from '../models/ApiResponse{T}';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { LocalstorageService } from './helpers/localstorage.service';
import { localStorageKeys } from './helpers/localstoragekeys';
import { UserDetail } from '../models/userdetail.model';
import { User } from '../models/user.model';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private apiUrl = 'http://localhost:5191/api/Auth/';

  private authState = new BehaviorSubject<boolean>(this.localStorageHelper.hasItem(localStorageKeys.TokenName));
  private usernameSubject = new BehaviorSubject<string | null | undefined>(this.localStorageHelper.getItem(localStorageKeys.LoginId));
  private useridSubject = new BehaviorSubject<string | null | undefined>(this.localStorageHelper.getItem(localStorageKeys.UserId));

  constructor(private http: HttpClient, private localStorageHelper: LocalstorageService) { }
  signUp(user: User): Observable<ApiResponse<string>> {
    const body = user;
    return this.http.post<ApiResponse<string>>(this.apiUrl + "Register", body)
  }

  signIn(username: string, password: string): Observable<ApiResponse<string>> {
    const body = { username, password };
    return this.http.post<ApiResponse<string>>(this.apiUrl + "Login", body).pipe( //pipe and tap is for change detection
      tap(response => {
        if (response.success) {
          const token = response.data;

          const payload = token.split('.')[1];
          const decodedPayload = JSON.parse(atob(payload));
          const userid = decodedPayload.UserId;

          this.localStorageHelper.setItem(localStorageKeys.TokenName, token);
          this.localStorageHelper.setItem(localStorageKeys.LoginId, username);
          this.localStorageHelper.setItem(localStorageKeys.UserId, userid);
          // this.authState.next(true);
          this.authState.next(this.localStorageHelper.hasItem(localStorageKeys.TokenName));
          this.usernameSubject.next(username);
          this.useridSubject.next(userid);
        }
      })
    );
  }

  getUserDetailById(userId: number | undefined): Observable<ApiResponse<UserDetail>> {
    return this.http.get<ApiResponse<UserDetail>>(this.apiUrl + 'GetUserDetailById?id=' + userId);
  }

  forgetpassword(username: string, password: string,confirmPassword:string): Observable<ApiResponse<string>> {
    const body = { username, password,confirmPassword };
    return this.http.post<ApiResponse<string>>(this.apiUrl + "PasswordService", body)
  }

  changepassword(username: string|null|undefined, password: string,confirmPassword:string): Observable<ApiResponse<string>> {
    const body = { username, password,confirmPassword };
    return this.http.post<ApiResponse<string>>(this.apiUrl + "PasswordService", body)
  }

  updatedUser(updatedUser: UserDetail): Observable<ApiResponse<string>> {
    return this.http.put<ApiResponse<string>>(this.apiUrl + 'EditUserDetail', updatedUser);
  }

  signOut() {
    this.localStorageHelper.removeItem(localStorageKeys.TokenName);
    this.localStorageHelper.removeItem(localStorageKeys.LoginId);
    this.localStorageHelper.removeItem(localStorageKeys.UserId);
    this.authState.next(false);
    this.usernameSubject.next(null);
    this.useridSubject.next(null);
  }


  isAuthenticated() {
    return this.authState.asObservable();
  }

  getUserName(): Observable<string | null | undefined> {
    return this.usernameSubject.asObservable();
  }

  getUserId(): Observable<string | null | undefined> {
    return this.useridSubject.asObservable();
  }

}
