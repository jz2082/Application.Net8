import { Injectable } from '@angular/core';
import { HttpEvent, HttpInterceptor, HttpHandler, HttpRequest, HttpEventType, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { tap, catchError, retry } from 'rxjs/operators';

import { LoggerService } from '@common/service/logger.service';
import { AppStorage } from '@common/service/app-storage';

@Injectable()
export class LoggerInterceptorService implements HttpInterceptor {
  constructor(private loggerService: LoggerService) { }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    this.loggerService.logTrace('LoggerInterceptorService-HttpRequest: ', req.method, req.url);
    AppStorage.setCurrentAccessTime();
    if (!req.headers.has("Content-Type")) {
      req = req.clone({
        setHeaders: { 
          'Access-Control-Allow-Origin': '*',
          'Content-Type': 'application/json' 
        }
      });
    }
    return next.handle(req).pipe(
      retry(2),
      tap(event => {
        if (event.type === HttpEventType.Response) {
          this.loggerService.logTrace('LoggerInterceptorService-HttpResponse: ', event.body);
        }
      }),
      catchError((error: HttpErrorResponse) => {
        this.loggerService.logDebug('LoggerInterceptorService-HttpRequest: ', req);
        this.loggerService.logError('LoggerInterceptorService-HttpErrorResponse: ', error);
        return throwError(() => error);
      })
    );
  }
}