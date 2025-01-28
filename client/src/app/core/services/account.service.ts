import { computed, inject, Injectable, signal } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Address, User } from '../../shared/models/user';
import { map, Observable, tap } from 'rxjs';
@Injectable({
  providedIn: 'root',
})
export class AccountService {
  baseUrl = environment.apiUrl;
  private http = inject(HttpClient);
  currentUser = signal<User | null>(null);
  isAdmin = computed(() => {
    const roles = this.currentUser()?.roles;
    return Array.isArray(roles) ? roles.includes('Admin') : roles === 'Admin';
  });

  login(values: any) {
    let params = new HttpParams();
    params = params.append('useCookies', true);
    return this.http.post<User>(this.baseUrl + 'login', values, { params });
  }
  register(values: any) {
    return this.http.post(this.baseUrl + 'account/register', values);
  }
  getUserInfo() {
    return this.http.get<User>(this.baseUrl + 'account/user-info').pipe(
      map((user) => {
        this.currentUser.set(user);
        return user;
      })
    );
  }
  logout() {
    return this.http.post(this.baseUrl + 'account/logout', {});
  }
  updateAddress(address: Address) {
    return this.http.post(this.baseUrl + 'account/address', address).pipe(
      tap(() => {
        this.currentUser.update((user) => {
          if (user) user.address = address;
          return user;
        });
      })
    );
  }
  getAuthState() {
    return this.http.get<{ isAuthenticated: boolean }>(
      this.baseUrl + 'account/auth-status'
    );
  }

  passwordReset(email: string) {
    return this.http.post(this.baseUrl + 'account/password-reset', { email });
  }

  verifyResetToken(resetToken: string, userId: string) {
    return this.http.post(this.baseUrl + 'account/verify-reset-token', {
      resetToken,
      userId,
    });
  }

  updatePassword(
    userId: string,
    resetToken: string,
    password: string,
    passwordConfirm: string
  ) {
    return this.http.post(this.baseUrl + 'account/update-password', {
      resetToken,
      userId,
      password,
      passwordConfirm,
    });
  }
}
