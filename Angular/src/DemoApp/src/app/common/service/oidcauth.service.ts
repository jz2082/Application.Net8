import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';
import { share } from 'rxjs/operators';
import { UserManager, UserManagerSettings, User, Log } from 'oidc-client';

import { environment } from '@env/environment';
import { AppUser } from '@common/model/appUser';
import { AppClaim } from '@common/model/appClaim';
import { LoggerService } from '@common/service/logger.service';
import { AppStorage } from '@common/service/app-storage';

@Injectable()
export class OidcAuthService {
  private _manager: UserManager;
  private _userLoginSubject = new Subject<boolean>();

  constructor(private loggerService: LoggerService) {
    this.loggerService.logTrace('OidcAuthService.constructor(private loggerService: LoggerService)');
    Log.logger = console;
    Log.level = Log.ERROR;
    this._manager = new UserManager(environment.azureAd);
    this._manager.events.addUserLoaded(
      user => {
        this.loggerService.logTrace('OidcAuthService.constructor() - addUserLoaded');
        if (user) {
          this.setAppUser(user);
        }
      });
    this._manager.events.addAccessTokenExpiring(
      () => {
        this.loggerService.logTrace('OidcAuthService.constructor() - addAccessTokenExpiring');
        if (AppStorage.isSessionExpired) {
          this.logout();
        }
        else {
          this.silentLogin();
        }
      });
    this._manager.events.addAccessTokenExpired(
      () => {
        this.loggerService.logTrace('OidcAuthService.constructor() - addAccessTokenExpired');
        this.logout();
      });
    this._manager.events.addUserSignedOut(
      () => {
        this.loggerService.logTrace('OidcAuthService.constructor() - addUserSignedOut');
      });
    this._manager.events.addSilentRenewError(
      (error) => {
        this.loggerService.logTrace('OidcAuthService.constructor() - addSilentRenewError');
        this.loggerService.logError('addSilentRenewError', error);
      });
  }

  public get appUser(): AppUser | undefined {
    return AppStorage.appUser;
  }

  public get isLoggedIn(): boolean {
    return AppStorage.isLoggedIn;
  }

  public get accessToken(): string | undefined {
    return AppStorage.accessToken;
  }

  isUserLoggedIn(): Observable<boolean> {
    this.loggerService.logTrace('OidcAuthService.isUserLoggedIn(): Observable<boolean>');
    this._manager.getUser().then(user => {
      if (user) {
        this.loggerService.logTrace('OidcAuthService.isUserLoggedIn(): Observable<boolean> - getUser()');
        this.setAppUser(user);
      }
    });
    return this._userLoginSubject.asObservable().pipe(share());
  }

  authenticatedUser(): Promise<User | null> {
    this.loggerService.logTrace('OidcAuthService.authenticatedUser(): Promise<User>');
    this._manager.getUser().then(user => {
      if (user) {
        this.loggerService.logTrace('OidcAuthService.authenticatedUser(): Promise<User> - getUser()');
        this.setAppUser(user);
      }
    });
    return this._manager.getUser();
  }

  login(): Promise<void> {
    this.loggerService.logTrace('OidcAuthService.login(): Promise<void>');
    return this._manager.signinRedirect();
  }

  async loginComplete(): Promise<void> {
    this.loggerService.logTrace('OidcAuthService.async loginComplete(): Promise<void>');
    return await this._manager.signinRedirectCallback().then(user => {
      this.loggerService.logTrace('OidcAuthService.async loginComplete() - signinRedirectCallback()');
      this.setAppUser(user);
      this._userLoginSubject.next(user ? !user.expired : false);
    });
  }

  silentLogin(): Promise<User> {
    this.loggerService.logTrace('OidcAuthService.silentLogin(): Promise<User>');
    return this._manager.signinSilent();
  }

  async silentLoginComplete(): Promise<void> {
    this.loggerService.logTrace('OidcAuthService.async silentLoginComplete(): Promise<void>');
    return await this._manager.signinSilentCallback().then(user => {
      this.loggerService.logDebug('OidcAuthService.async silentLoginComplete() - signinSilentCallback()');
      this.setAppUser(user);
      this._userLoginSubject.next(user ? !user.expired : false);
    });
  }

  logout(): Promise<void> {
    this.loggerService.logTrace('OidcAuthService.logout(): Promise<void>');
    return this._manager.signoutRedirect();
  }

  async logoutComplete(): Promise<void> {
    this.loggerService.logTrace('OidcAuthService - async logoutComplete()');
    return await this._manager.signoutRedirectCallback().then(() => {
      this.loggerService.logTrace('OidcAuthService - async logoutComplete() - signoutRedirectCallback()');
      AppStorage.clear();
      this._userLoginSubject.next(false);
    });
  }

  protected setAppUser(user: User | undefined) {
    if (user) {
      let appUser = new AppUser();
      appUser.idToken = user.id_token;
      appUser.accessToken = user.access_token;
      appUser.refreshToken = user.refresh_token;
      if (user.profile) {
        for (let item in user.profile) {
          let appClaim = {} as AppClaim;
          appClaim.type = item;
          appClaim.value = user.profile[item];
          appUser.claimList.push(appClaim);
          switch (item) {
            case 'email':
              appUser.userId = user.profile[item];
              break;
            case 'family_name':
              appUser.familyName = user.profile[item];
              break;
            case 'given_name':
              appUser.givenName = user.profile[item];
              break;
            case 'name':
              appUser.userName = user.profile[item];
              break;
          }
        }
      }
      AppStorage.isLoggedIn = user ? !user.expired : false;
      AppStorage.appUser = appUser;
    }
  }
}
