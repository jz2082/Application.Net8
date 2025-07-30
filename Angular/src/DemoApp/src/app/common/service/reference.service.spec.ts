import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { ReferenceService } from './reference.service';
import { LoggerService } from '@common/service/logger.service';
import { environment } from '@env/environment';
import { ApiResponse } from '@common/model/apiResponse';
import { AppKeyValue } from '@common/model/appKeyValue';
import { AdditionalInfo } from '@common/model/additionalInfo';

describe('ReferenceService', () => {
  let service: ReferenceService;
  let httpMock: HttpTestingController;
  let loggerSpy: jasmine.SpyObj<LoggerService>;
  const apiBaseUrl = environment.appSetting.apiBaseUrl + 'api/Reference';

  beforeEach(() => {
    loggerSpy = jasmine.createSpyObj('LoggerService', ['logTrace']);
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [
        ReferenceService,
        { provide: LoggerService, useValue: loggerSpy }
      ]
    });
    service = TestBed.inject(ReferenceService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should get country list', () => {
    const mockResponse: ApiResponse<AppKeyValue[]> = { data: [], hasViolation: false, hasException: false, ruleViolation: new AdditionalInfo() };
    service.GetCountryList().subscribe(res => {
      expect(res).toEqual(mockResponse);
    });
    const req = httpMock.expectOne(apiBaseUrl + '/list/country');
    expect(req.request.method).toBe('GET');
    req.flush(mockResponse);
    expect(loggerSpy.logTrace).toHaveBeenCalled();
  });

  it('should get province list', () => {
    const mockResponse: ApiResponse<AppKeyValue[]> = { data: [], hasViolation: false, hasException: false, ruleViolation: new AdditionalInfo() };
    service.GetProvinceList('CN').subscribe(res => {
      expect(res).toEqual(mockResponse);
    });
    const req = httpMock.expectOne(apiBaseUrl + '/list/province/CN');
    expect(req.request.method).toBe('GET');
    req.flush(mockResponse);
    expect(loggerSpy.logTrace).toHaveBeenCalled();
  });

  it('should get future list', () => {
    const mockResponse: ApiResponse<AppKeyValue[]> = { data: [], hasViolation: false, hasException: false, ruleViolation: new AdditionalInfo() };
    service.GetFutureList().subscribe(res => {
      expect(res).toEqual(mockResponse);
    });
    const req = httpMock.expectOne(apiBaseUrl + '/list/future/24');
    expect(req.request.method).toBe('GET');
    req.flush(mockResponse);
    expect(loggerSpy.logTrace).toHaveBeenCalled();
  });

  it('should get stock future list', () => {
    const mockResponse: ApiResponse<AppKeyValue[]> = { data: [], hasViolation: false, hasException: false, ruleViolation: new AdditionalInfo() };
    service.GetStockFutureList('STK', '20250101', 10).subscribe(res => {
      expect(res).toEqual(mockResponse);
    });
    const req = httpMock.expectOne(apiBaseUrl + '/list/future/STK/20250101/10');
    expect(req.request.method).toBe('GET');
    req.flush(mockResponse);
    expect(loggerSpy.logTrace).toHaveBeenCalled();
  });

  it('should get stock list', () => {
    const mockResponse: ApiResponse<AppKeyValue[]> = { data: [], hasViolation: false, hasException: false, ruleViolation: new AdditionalInfo() };
    service.GetStockList().subscribe(res => {
      expect(res).toEqual(mockResponse);
    });
    const req = httpMock.expectOne(apiBaseUrl + '/list/stock');
    expect(req.request.method).toBe('GET');
    req.flush(mockResponse);
    expect(loggerSpy.logTrace).toHaveBeenCalled();
  });

  it('should get stock holding list', () => {
    const mockResponse: ApiResponse<AppKeyValue[]> = { data: [], hasViolation: false, hasException: false, ruleViolation: new AdditionalInfo() };
    service.GetStockHoldingList().subscribe(res => {
      expect(res).toEqual(mockResponse);
    });
    const req = httpMock.expectOne(apiBaseUrl + '/list/stockHolding');
    expect(req.request.method).toBe('GET');
    req.flush(mockResponse);
    expect(loggerSpy.logTrace).toHaveBeenCalled();
  });

  it('should get reference list', () => {
    const mockResponse: ApiResponse<AppKeyValue[]> = { data: [], hasViolation: false, hasException: false, ruleViolation: new AdditionalInfo() };
    service.GetReferenceList('testKey').subscribe(res => {
      expect(res).toEqual(mockResponse);
    });
    const req = httpMock.expectOne(apiBaseUrl + '/list/reference/testKey');
    expect(req.request.method).toBe('GET');
    req.flush(mockResponse);
    expect(loggerSpy.logTrace).toHaveBeenCalled();
  });

});