import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, retry } from 'rxjs';

import { environment } from '@env/environment';
import { ApiResponse } from '@common/model/apiResponse';
import { Product } from '@demo/model/product';
import { LoggerService } from '@common/service/logger.service';

@Injectable()
export class ProductService {
  private httpRetryCount = environment.appSetting.httpRetryCount;
  private serviceUrl = environment.appSetting.apiBaseUrl + 'api/Product';

  constructor(private http: HttpClient, private loggerService: LoggerService) {
  }

  getEntityList(): Observable<ApiResponse<Product[]>> {
    this.loggerService.logTrace("ProductService.getEntityList()");
    return this.http.get<ApiResponse<Product[]>>(this.serviceUrl + '/list').pipe(retry(this.httpRetryCount));
  }

  getEntity(productId: number): Observable<ApiResponse<Product>> {
    this.loggerService.logTrace("ProductService.getEntity(productId: number): ", productId);
    return this.http.get<ApiResponse<Product>>(`${this.serviceUrl}/entity/${productId}`).pipe(retry(this.httpRetryCount));
  }

  createEntity(entity: Product): Observable<ApiResponse<Product>> {
    this.loggerService.logDebug("ProductService.createEntity(entity: Product): ", entity);
    return this.http.post<ApiResponse<Product>>(`${this.serviceUrl}/entity`, entity).pipe(retry(this.httpRetryCount));
  }

  updateEntity(entity: Product): Observable<ApiResponse<Product>> {
    this.loggerService.logTrace("ProductService.updateEntity(entity: Product): ", entity);
    return this.http.put<ApiResponse<Product>>(`${this.serviceUrl}/entity`, entity).pipe(retry(this.httpRetryCount));
  }

  deleteEntity(productId: number): Observable<ApiResponse<Product>> {
    this.loggerService.logTrace("ProductService.deleteEntity(productId: number): ", productId);
    return this.http.delete<ApiResponse<Product>>(`${this.serviceUrl}/entity/${productId}`).pipe(retry(this.httpRetryCount));
  }
}