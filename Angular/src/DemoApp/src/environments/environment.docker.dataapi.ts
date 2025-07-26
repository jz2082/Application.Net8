import { LogLevel } from '@common/model/logLevel';

export const environment = {
  production: true,
  appSetting: {
    environment: 'Production',
    logLevel: LogLevel.debug,
    appDefaultRouteLink: '/dashboard',
    apiBaseUrl: 'https://localhost:44813/',
    loggerBaseUrl: 'https://localhost:44813/api/LoggerInfo',
    jsonServerUrl: 'http://localhost:4233/assets/data/',
    sessionTimeout: 300,
    smallWidthBreakPoint: 1280,
    httpRetryCount: 3,
    logClientIpAddress: false,
    showLoggerInfoView: false,
    wakeupDataApi: false,
    aboutMessage: 'Production Docker Configuration (http://localhost:4233)'
  },
  azureAd: {
    authority: 'https://login.microsoftonline.com/peiyan65hotmail.onmicrosoft.com/v2.0',
    client_id: 'fdef9023-f9e8-4af1-9922-e2c9d010b11a',
    response_type: 'id_token token',
    scope: 'openid profile api://09f5077c-913a-41d7-a0e3-63df07f986fd/access_as_user',
    redirect_uri: 'http://localhost:4233/oidclogin-callback',
    post_logout_redirect_uri: 'http://localhost:4233/oidclogout-callback',
    filterProtocolClaims: true,
    loadUserInfo: false,
    automaticSilentRenew: false,
    silent_redirect_uri: 'http://localhost:4233/oidcsilent-callback'
  }
};