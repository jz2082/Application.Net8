import { Component, OnInit } from '@angular/core';
import { Router, RouterLink } from '@angular/router';

import { environment } from '@env/environment';
import { AppStorage } from '@common/service/app-storage';
import { OidcAuthService } from '@common/service/oidcauth.service';
import { LoggerService } from '@common/service/logger.service';

@Component({
  selector: 'common-oidclogin',
  standalone: true,
  imports: [
    RouterLink
  ],
  providers: [
  ],
  template: 
  `
  <div class="p-grid">
    <div class="p-col-12 p-md-12">
      <p>
        <b>Auth Callback</b> worked but you should redirect on from this page
      </p>
      <br />
      Is Authenticated: {{ isAuthenticated }}
      <br />
      <br />
      RedirectUrl: {{ redirectUrl }}
      <br />
      <a [routerLink]="['/dashboard']">Redirect</a>
    </div>
  </div>
  `
})
export class OidcloginCallbackComponent implements OnInit {

  constructor(private router: Router, private authService: OidcAuthService, private loggerService: LoggerService) {
  }

  ngOnInit(): void {
    this.loggerService.logTrace('OidcloginCallbackComponent.ngOnInit()');
    this.authService.loginComplete()
      .then(() => {
        this.loggerService.logDebug('OidcloginCallbackComponent.ngOnInit() - authService.loginComplete()');
        if (this.authService.isLoggedIn) {
          if (AppStorage.redirectUrl) {
            AppStorage.currentRouterLink = AppStorage.redirectUrl;
            this.router.navigate([AppStorage.redirectUrl]);
          }
          else {
            AppStorage.currentRouterLink = environment.appSetting.appDefaultRouteLink;
            this.router.navigate([environment.appSetting.appDefaultRouteLink]);
          }
        }
      })
      .catch(error => {
        this.loggerService.logError('OidcloginCallbackComponent.ngOnInit() - authService.loginComplete()', error)
      });
  }

  public get isAuthenticated(): boolean {
    return this.authService.isLoggedIn;
  }

  public get redirectUrl(): string {
    const redirectUrl = AppStorage.redirectUrl;
    if (redirectUrl == undefined) {
      return "/dashboard";
    }
    return redirectUrl
  }
}
