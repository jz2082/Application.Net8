import { LogLevel } from '@common/model/logLevel';

export const environment = {
  production: true,
  appSetting: {
    environment: '#{environment}#',
    logLevel: LogLevel.error,
    appDefaultRouteLink: '/dashboard',
    apiBaseUrl: '#{apiBaseUrl}#',
    loggerBaseUrl: '#{loggerBaseUrl}#',
    jsonServerUrl: '#{jsonServerUrl}#',
    sessionTimeout: 300,
    smallWidthBreakPoint: 1280,
    httpRetryCount: 3,
    logClientIpAddress: false,
    showLoggerInfoView: false,
    wakeupDataApi: false,
    aboutMessage: '#{aboutMessage}#'
  },
  azureAd: {
    authority: '#{authority}#',
    client_id: '#{client_id}#',
    response_type: 'id_token token',
    scope: '#{scope}#',
    redirect_uri: '#{redirect_uri}#',
    post_logout_redirect_uri: '#{post_logout_redirect_uri}#',
    filterProtocolClaims: true,
    loadUserInfo: false,
    automaticSilentRenew: false,
    silent_redirect_uri: '#{silent_redirect_uri}#'
  }
};