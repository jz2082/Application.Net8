import { ApplicationConfig, provideZoneChangeDetection } from '@angular/core';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { provideRouter } from '@angular/router';
import { HTTP_INTERCEPTORS, provideHttpClient } from '@angular/common/http';
import { routes } from './app.routes';
import { providePrimeNG } from 'primeng/config';
import Aura from '@primeng/themes/aura';
import { ConfirmationService, MessageService } from 'primeng/api';

import { AppInjectorService } from '@app/common/service/app-injector.service';
import { LoggerService } from '@app/common/service/logger.service';
import { OidcAuthService } from '@app/common/service/oidcauth.service';
import { OidcAuthGuardService } from '@app/common/service/oidcauth-guard.service';
import { LoggerInfoService } from '@app/common/service/loggerInfo.service';
import { LoggerInterceptorService } from '@app/common/interceptor/logger-interceptor.service';
import { OidcAuthInterceptorService } from '@app/common/interceptor/oidcauth-interceptor.service';
import { CacheInterceptorService } from '@app/common/interceptor/cache-interceptor.service';

export const appConfig: ApplicationConfig = {
  providers: [
    provideZoneChangeDetection({ eventCoalescing: true }), 
    provideRouter(routes),
    provideAnimationsAsync(),
    providePrimeNG({ 
      theme: {
        preset: Aura,
        options: {
          prefix: 'p',
          darkModeSelector: 'system',
          cssLayer: false
        }
      }
    }),
    provideHttpClient(),
    ConfirmationService,
    MessageService,
    AppInjectorService,
    LoggerService,
    OidcAuthService,
    OidcAuthGuardService,
    LoggerInfoService,
    { provide: HTTP_INTERCEPTORS, useClass: LoggerInterceptorService, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: OidcAuthInterceptorService, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: CacheInterceptorService, multi: true }
  ]
};