import { Injectable } from '@angular/core';
import { HttpResponse } from '@angular/common/http';

@Injectable()
export class HttpCacheService {
  private requests: any = { };
  
  constructor() { }

  setValue(url: string, response: HttpResponse<any>): void {
    this.requests[url] = response;
  }

  getValue(url: string): HttpResponse<any> {
    return this.requests[url];
  }

  deleteValue(url: string): void {
    this.requests[url] = undefined;
  }

  clear(): void {
    this.requests = { };
  }
}