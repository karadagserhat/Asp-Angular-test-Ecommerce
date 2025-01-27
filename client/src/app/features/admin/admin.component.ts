import { Component, inject, OnInit } from '@angular/core';
import { MatTabsModule } from '@angular/material/tabs';
import { AdminCatalogComponent } from './admin-catalog/admin-catalog.component';
import { SignalRService } from '../../core/services/signalr.service';
import { HubUrls } from '../../constants/hub-urls';
import { ReceiveFunctions } from '../../constants/receive-functions';
import { SnackbarService } from '../../core/services/snackbar.service';

@Component({
  selector: 'app-admin',
  standalone: true,
  imports: [MatTabsModule, AdminCatalogComponent],
  templateUrl: './admin.component.html',
  styleUrl: './admin.component.scss',
})
export class AdminComponent implements OnInit {
  private signalRService = inject(SignalRService);
  private snackbar = inject(SnackbarService);

  ngOnInit(): void {
    this.signalRService.on(
      HubUrls.ProductHub,
      ReceiveFunctions.ProductAddedMessageReceiveFunction,
      (message) => {
        this.snackbar.success(message);
      }
    );
  }
}
