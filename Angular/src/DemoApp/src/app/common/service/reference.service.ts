import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import { LoggerService } from '@common/service/logger.service';

import { ApiResponse } from '@common/model/apiResponse';
import { environment } from '@env/environment';
import { AppKeyValue } from '@common/model/appKeyValue';


@Injectable()
export class ReferenceService {
    private serviceUrl = environment.appSetting.apiBaseUrl + 'api/Reference';

    constructor(private http: HttpClient, private loggerService: LoggerService) {
    }

    GetCountryList(): Observable<ApiResponse<AppKeyValue[]>> {
        this.loggerService.logTrace("ReferenceService-GetCountryList()");
        return this.http.get<ApiResponse<AppKeyValue[]>>(this.serviceUrl + '/list/country');
    }

    GetProvinceList(key: string): Observable<ApiResponse<AppKeyValue[]>> {
        this.loggerService.logTrace("ReferenceService-GetProvinceList(key: string)");
        return this.http.get<ApiResponse<AppKeyValue[]>>(this.serviceUrl + '/list/province/' + key);
    }

    GetFutureList(): Observable<ApiResponse<AppKeyValue[]>> {
        this.loggerService.logTrace("ReferenceService-GetFutureList()");
        return this.http.get<ApiResponse<AppKeyValue[]>>(this.serviceUrl + '/list/future/24');
    }

    GetStockFutureList(stockCode: string, tradeDate: string, takeCount: number): Observable<ApiResponse<AppKeyValue[]>> {
        this.loggerService.logTrace("ReferenceService-GetFutureList()");
        return this.http.get<ApiResponse<AppKeyValue[]>>(`${this.serviceUrl}/list/future/${stockCode}/${tradeDate}/${takeCount}`);
    }

    GetStockList(): Observable<ApiResponse<AppKeyValue[]>> {
        this.loggerService.logTrace("ReferenceService-GetStockList()");
        return this.http.get<ApiResponse<AppKeyValue[]>>(this.serviceUrl + '/list/stock');
    }

    GetStockHoldingList(): Observable<ApiResponse<AppKeyValue[]>> {
        this.loggerService.logTrace("ReferenceService-GetStockList()");
        return this.http.get<ApiResponse<AppKeyValue[]>>(this.serviceUrl + '/list/stockHolding');
    }

    GetReferenceList(key: string): Observable<ApiResponse<AppKeyValue[]>> {
        this.loggerService.logTrace("ReferenceService-GetReferenceList(key: string)");
        return this.http.get<ApiResponse<AppKeyValue[]>>(this.serviceUrl + '/list/reference/' + key);
    }
}