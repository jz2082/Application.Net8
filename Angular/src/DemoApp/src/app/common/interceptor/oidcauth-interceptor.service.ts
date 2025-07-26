import { Injectable } from '@angular/core';
import { HttpEvent, HttpInterceptor, HttpHandler, HttpRequest } from '@angular/common/http';
import { Observable } from 'rxjs';

import { LoggerService } from '@common/service/logger.service';
import { OidcAuthService } from '@common/service/oidcauth.service';

@Injectable()
export class OidcAuthInterceptorService implements HttpInterceptor {
  constructor(private authService: OidcAuthService, private loggerService: LoggerService) { }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    this.loggerService.logTrace('OidcAuthInterceptorService-intercept: ', req.url, req.method, this.authService.isLoggedIn);
    if (this.authService.isLoggedIn) {
      req = req.clone({
        setHeaders: {
          'Access-Control-Allow-Origin': '*',
          'Authorization': 'Bearer ' + this.authService.accessToken
        }
      });
    }
    return next.handle(req);
  }
}