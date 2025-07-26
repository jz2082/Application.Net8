import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { environment } from '@env/environment';
import { AppStorage } from '@common/service/app-storage';
import { OidcAuthService } from '@common/service/oidcauth.service';
import { LoggerService } from '@common/service/logger.service';

@Component({
  selector: 'common-oidclogout',
  standalone: true,
  providers: [
  ],
  template: 
  `
  `
})
export class OidclogoutCallbackComponent implements OnInit {

  constructor(private router: Router, private authService: OidcAuthService, private loggerService: LoggerService) {
  }

  ngOnInit(): void {
    this.loggerService.logTrace('OidclogoutCallbackComponent.ngOnInit()');
    this.authService.logoutComplete()
      .then(() => {
        this.loggerService.logTrace('OidclogoutCallbackComponent.ngOnInit() - authService.logoutComplete()');
        AppStorage.currentRouterLink = environment.appSetting.appDefaultRouteLink;
        this.router.navigate([environment.appSetting.appDefaultRouteLink]);
      })
      .catch(error => {
        this.loggerService.logError('OidclogoutCallbackComponent.ngOnInit()', error)
      });
  }
}
