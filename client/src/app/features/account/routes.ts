import { Route } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { PasswordResetComponent } from './password-reset/password-reset.component';
import { UpdatePasswordComponent } from './update-password/update-password.component';
export const accountRoutes: Route[] = [
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'password-reset', component: PasswordResetComponent },
  {
    path: 'update-password/:userId/:resetToken',
    component: UpdatePasswordComponent,
  },
];
