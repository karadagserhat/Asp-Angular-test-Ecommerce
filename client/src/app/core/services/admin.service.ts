import { computed, inject, Injectable, signal } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Product } from '../../shared/models/product';

@Injectable({
  providedIn: 'root',
})
export class AdminService {
  baseUrl = environment.apiUrl;
  private http = inject(HttpClient);

  createProduct(product: Product) {
    return this.http.post<Product>(this.baseUrl + 'products', product);
  }

  updateProduct(product: Product) {
    return this.http.put(this.baseUrl + 'products/' + product.id, product);
  }

  deleteProduct(id: number) {
    return this.http.delete(this.baseUrl + 'products/' + id);
  }

  updateStock(productId: number, newQuantity: number) {
    return this.http.put(this.baseUrl + 'products/update-stock/' + productId, {
      productId,
      newQuantity,
    });
  }
}
