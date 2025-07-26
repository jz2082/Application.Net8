import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import { environment } from '@env/environment';
import { ApiResponse } from '@common/model/apiResponse';
import { LoggerService } from '@common/service/logger.service';
import { ApiSetting } from '@common/model/apiSetting';
import { HealthCheckResult } from '@common/model/healthCheckResult';

@Injectable()
export class ApiAboutService {
  private serviceUrl = environment.appSetting.apiBaseUrl + 'api/About';

  constructor(private http: HttpClient, private loggerService: LoggerService) {
  }

  getApiSeting(): Observable<ApiResponse<ApiSetting>> {
    this.loggerService.logTrace("ApiAboutService.getApiSeting()");
    return this.http.get<ApiResponse<ApiSetting>>(this.serviceUrl);
  }

  getApiDateTime(): Observable<ApiResponse<Date>> {
    this.loggerService.logTrace("ApiAboutService.getApiDateTime()");
    return this.http.get<ApiResponse<Date>>(this.serviceUrl + '/Health/DateTime');
  }

  getApiHealthCheckResult(): Observable<ApiResponse<HealthCheckResult[]>> {
    this.loggerService.logTrace("ApiAboutService.getApiHealthCheckResult()");
    return this.http.get<ApiResponse<HealthCheckResult[]>>(this.serviceUrl + '/Health/Check');
  }

  getApiException(): Observable<ApiResponse<ApiSetting>> {
    this.loggerService.logTrace("ApiAboutService.getApiSeting()");
    return this.http.get<ApiResponse<ApiSetting>>(this.serviceUrl + '/ApiException');
  }
}