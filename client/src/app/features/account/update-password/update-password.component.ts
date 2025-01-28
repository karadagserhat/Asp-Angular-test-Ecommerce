import { Component, inject, OnInit } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { MatButton } from '@angular/material/button';
import { MatCard } from '@angular/material/card';
import { MatFormField, MatLabel } from '@angular/material/form-field';
import { MatInput } from '@angular/material/input';
import { AccountService } from '../../../core/services/account.service';
import { ActivatedRoute, Router } from '@angular/router';
import { SnackbarService } from '../../../core/services/snackbar.service';

@Component({
  selector: 'app-update-password',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    MatCard,
    MatFormField,
    MatInput,
    MatLabel,
    MatButton,
  ],
  templateUrl: './update-password.component.html',
  styleUrl: './update-password.component.scss',
})
export class UpdatePasswordComponent implements OnInit {
  accountService = inject(AccountService);
  activatedRoute = inject(ActivatedRoute);
  snackbarService = inject(SnackbarService);
  router = inject(Router);
  state: boolean = true;

  ngOnInit(): void {
    this.activatedRoute.params.subscribe({
      next: (params) => {
        const userId: string = params['userId'];
        const resetToken: string = params['resetToken'];
        this.accountService
          .verifyResetToken(resetToken, userId)
          .subscribe((state: any) => {
            this.state = state.state;
          });
      },
    });
  }

  updatePassword(password: string, passwordConfirm: string) {
    if (password !== passwordConfirm) {
      this.snackbarService.error('Password doesnt match!');
      return;
    }

    this.activatedRoute.params.subscribe({
      next: (params) => {
        const userId: string = params['userId'];
        const resetToken: string = params['resetToken'];
        this.accountService
          .updatePassword(userId, resetToken, password, passwordConfirm)
          .subscribe({
            next: (params) => {
              this.snackbarService.success('Success!');
              this.router.navigateByUrl('/account/login');
            },
            error: () => {
              this.snackbarService.error('ERROR!');
            },
          });
      },
    });
  }
}
