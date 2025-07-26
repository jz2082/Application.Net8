import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import { environment } from '@env/environment';
import { ApiResponse } from '@common/model/apiResponse';
import { LoggerInfo } from '@common/model/LoggerInfo';
import { LoggerService } from '@common/service/logger.service';

@Injectable()
export class LoggerInfoService {
  private serviceUrl = environment.appSetting.apiBaseUrl + 'api/LoggerInfoInMemory';

  constructor(private http: HttpClient, private loggerService: LoggerService) { 
  }

  getEntityList(): Observable<ApiResponse<LoggerInfo[]>> {
    this.loggerService.logTrace("LoggerInfoService.getEntityList()");
    return this.http.get<ApiResponse<LoggerInfo[]>>(this.serviceUrl + '/list');
  }

  getEntity(id: string): Observable<ApiResponse<LoggerInfo>> {
    this.loggerService.logTrace("LoggerInfoService.getEntity(id: string): ", id);
    return this.http.get<ApiResponse<LoggerInfo>>(`${this.serviceUrl}/entity/${id}`);
  }
}