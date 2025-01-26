import { Component, inject, OnInit } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import {
  MAT_DIALOG_DATA,
  MatDialogModule,
  MatDialogRef,
} from '@angular/material/dialog';
import {
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { environment } from '../../../../environments/environment.development';
import { FileUploader, FileUploadModule } from 'ng2-file-upload';
import { Product } from '../../../shared/models/product';
import {
  MatProgressSpinnerModule,
  MatSpinner,
} from '@angular/material/progress-spinner';
import { NgClass, NgIf } from '@angular/common';

@Component({
  selector: 'app-product-photo',
  standalone: true,
  imports: [
    MatButtonModule,
    MatDialogModule,
    ReactiveFormsModule,
    FileUploadModule,
    MatProgressSpinnerModule,
    NgIf,
  ],
  templateUrl: './product-photo.component.html',
  styleUrl: './product-photo.component.scss',
})
export class ProductPhotoComponent implements OnInit {
  baseUrl = environment.apiUrl;
  uploader!: FileUploader;
  productForm!: FormGroup;
  isLoading = false;
  data = inject(MAT_DIALOG_DATA);
  private fb = inject(FormBuilder);
  private dialogRef = inject(MatDialogRef<ProductPhotoComponent>);
  imageUrl: string | null = null;

  ngOnInit(): void {
    this.initializeForm();

    this.uploader = new FileUploader({
      url: this.baseUrl + `products/add-photo`,
      isHTML5: true,
      allowedFileType: ['image'],
      removeAfterUpload: true,
      autoUpload: false,
    });

    this.uploader.onAfterAddingFile = (file) => {
      const reader = new FileReader();
      reader.onload = (e: any) => {
        this.imageUrl = e.target.result;
      };
      reader.readAsDataURL(file._file);
    };
  }

  initializeForm() {
    this.productForm = this.fb.group({
      pictureUrl: ['', [Validators.required]],
    });
  }

  onSubmit(): void {
    if (this.productForm.valid && this.uploader.queue.length > 0) {
      const fileItem = this.uploader.queue[0];

      if (!this.data || !this.data.product || !this.data.product.id) {
        console.error('Geçersiz ürün verisi: product veya product.id eksik.');
        return;
      }

      this.isLoading = true;
      fileItem.url = `${this.baseUrl}products/add-photo?productId=${this.data.product.id}`;

      this.uploader.onCompleteItem = (item, response, status, headers) => {
        this.isLoading = false;

        if (status === 200) {
          this.dialogRef.close({ success: true });
        } else {
          console.error('Dosya yüklenirken bir hata oluştu:', response);
        }
      };

      fileItem.upload();
    } else {
      console.error('Lütfen bir dosya seçin.');
    }
  }
}
