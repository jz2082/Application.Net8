import { environment } from '@env/environment';
import { AppUser } from '@common/model/appUser';

export class AppStorage {

  static get appUser(): AppUser | undefined {
    const value = sessionStorage.getItem('appUser');
    if (value) {
      return JSON.parse(value);
    }
    return undefined;
  }

  static set appUser(value: AppUser) {
    if (value) {
      sessionStorage.setItem('appUser', JSON.stringify(value));
    }
  }

  static get accessToken(): string | undefined {
    const value = sessionStorage.getItem('appUser');
    if (value) {
      const appUser: AppUser = JSON.parse(value);
      return appUser.accessToken;
    }
    return undefined;
  }

  static get isLoggedIn(): boolean {
    return sessionStorage.getItem('isLoggedIn') == 'Y' ? true : false;
  }

  static set isLoggedIn(value: boolean) {
    if (value) {
      sessionStorage.setItem('isLoggedIn', value ? 'Y' : 'N');
    }
  }

  static get redirectUrl(): string | undefined {
    const value = sessionStorage.getItem('redirectUrl');
    return value ? value : undefined;
  }

  static set redirectUrl(value: string) {
    if (value) {
      sessionStorage.setItem('redirectUrl', value);
    }
  }

  static get clientIpAddressUrl(): string | undefined {
    const value = sessionStorage.getItem('clientIpAddressUrl');
    return value ? value : undefined;
  }

  static set clientIpAddressUrl(value: string) {
    if (value) {
      sessionStorage.setItem('clientIpAddressUrl', value);
    }
  }

  static get currentRouterLink(): string | undefined {
    const value = sessionStorage.getItem('currentRouterLink');
    return value ? value : undefined;
  }

  static set currentRouterLink(value: string) {
    if (value) {
      sessionStorage.setItem('currentRouterLink', value);
    }
  }

  static get isSessionExpired(): boolean {
    const lastAccessTime = sessionStorage.getItem('currentAccessTime');
    if (lastAccessTime) {
      const accessTime: Date = JSON.parse(lastAccessTime);
      const currentTime = new Date();
      if (accessTime && accessTime instanceof Date) {
        if (currentTime.getSeconds() - accessTime.getSeconds() > environment.appSetting.sessionTimeout) {
          return true;
        }
        else {
          return false;
        }
      }
    }
    return true;
  }

  static clear() {
    sessionStorage.clear();
  }

  static setCurrentAccessTime() {
    sessionStorage.setItem('currentAccessTime', JSON.stringify(new Date()));
  }

}
