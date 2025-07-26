import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { OidcAuthService } from '@common/service/oidcauth.service';
import { LoggerService } from '@common/service/logger.service';

@Component({
  selector: 'common-oidcsilent',
  standalone: true,
  providers: [
  ],
  template: 
  `
  `
})
export class OidcsilentCallbackComponent implements OnInit {

  constructor(private router: Router, private authService: OidcAuthService, private loggerService: LoggerService) {
  }

  ngOnInit(): void {
    this.loggerService.logDebug('OidcsilentCallbackComponent.ngOnInit()');
    this.authService.silentLoginComplete()
     .then(() => {
       this.loggerService.logDebug('OidcsilentCallbackComponent.ngOnInit() - authService.silentLoginComplete()');
     })
     .catch (error => {
       this.loggerService.logDebug('OidcsilentCallbackComponent.ngOnInit() - authService.silentLoginComplete(): ', error)
     });
  }
}
