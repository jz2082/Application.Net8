import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';

import { OidcAuthService } from '@common/service/oidcauth.service';
import { LoggerService } from '@common/service/logger.service';
import { AppStorage } from '@common/service/app-storage';

@Injectable()
export class OidcAuthGuardService implements CanActivate {

    constructor(private authService: OidcAuthService, private loggerService: LoggerService) { }
    
    canActivate(next: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
        this.loggerService.logDebug('OidcAuthGuardService.canActivate(next: ActivatedRouteSnapshot, state: RouterStateSnapshot): state.url', state.url);
        AppStorage.redirectUrl = state.url;
        if (!this.authService.isLoggedIn) {
            this.authService.login();
        }
        return this.authService.isLoggedIn;
    }
}
