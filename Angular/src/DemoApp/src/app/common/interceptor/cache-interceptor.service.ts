import { Injectable } from '@angular/core';
import { HttpEvent, HttpInterceptor, HttpHandler, HttpRequest, HttpResponse } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { tap } from 'rxjs/operators';

import { HttpCacheService } from '@common/service/http-cache.service';
import { LoggerService } from '@common/service/logger.service';

@Injectable()
export class CacheInterceptorService {
  constructor(private cacheService: HttpCacheService, private loggerService: LoggerService) { }
  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    // pass along non-cacheable requests and invalidate cache
    if (req.method !== 'GET') {
      this.loggerService.logTrace('Clear cache: ', req.method, req.url);
      this.cacheService.clear();
      return next.handle(req);
    }
    // attempt to retrieve a cached response
    const cachedResponse: HttpResponse<any> = this.cacheService.getValue(req.url);
    // return cached response
    if (cachedResponse) {
      this.loggerService.logTrace('Returning a cached response: ', cachedResponse.url, cachedResponse);
      return of(cachedResponse);
    }
    // send request to server and add response to cache
    return next.handle(req)
      .pipe(
        tap(event => {
          if (event instanceof HttpResponse) {
            this.loggerService.logTrace("Adding item to cache:", req.url);
            this.cacheService.setValue(req.url, event);
          }
        })
      );
  }
}
