import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { LoggerInfoService } from './loggerInfo.service';
import { LoggerService } from '@common/service/logger.service';
import { environment } from '@env/environment';
import { LoggerInfo } from '@common/model/LoggerInfo';
import { ApiResponse } from '@common/model/apiResponse';
import { AdditionalInfo } from '@common/model/additionalInfo';

describe('LoggerInfoService', () => {
    let service: LoggerInfoService;
    let httpMock: HttpTestingController;
    let loggerSpy: jasmine.SpyObj<LoggerService>;
    const apiBaseUrl = environment.appSetting.apiBaseUrl + 'api/LoggerInfoInMemory';
    let mockLoggerInfo: LoggerInfo = {
        id: 'testid',
        environment: 'test environment',
        machineName: 'test machine',
        loggerName: 'test logger',
        level: 'info',
        threadName: 'test thread',
        timeStamp: new Date('2025-01-01'),
        application: 'test application',
        module: 'test module',
        method: 'test method',
        userName: 'test user',
        executionTime: 100,
        isJsonMessage: true,
        message: 'Test message',
        renderedType: 'test type',
        renderedInfo: 'test info',
        exceptionMessage: 'test exception message',
        exceptionType: 'test exception type',
        exceptionInfo: 'test exception info',
        dateCreated: new Date('2025-01-01'),
        dateModified: new Date('2025-01-01'),
        rowVersion: []
    };

    beforeEach(() => {
        loggerSpy = jasmine.createSpyObj('LoggerService', ['logTrace', 'logDebug']);
        TestBed.configureTestingModule({
            imports: [HttpClientTestingModule],
            providers: [
                LoggerInfoService,
                { provide: LoggerService, useValue: loggerSpy }
            ]
        });
        service = TestBed.inject(LoggerInfoService);
        httpMock = TestBed.inject(HttpTestingController);
    });

    afterEach(() => {
        httpMock.verify();
    });

    it('should be created', () => {
        expect(service).toBeTruthy();
    });

    it('should get entity list', () => {
        const mockResponse: ApiResponse<LoggerInfo[]> = { data: [], hasViolation: false, hasException: false, ruleViolation: new AdditionalInfo() };
        service.getEntityList().subscribe(res => {
            expect(res).toEqual(mockResponse);
        });
        const req = httpMock.expectOne(apiBaseUrl + '/list');
        expect(req.request.method).toBe('GET');
        req.flush(mockResponse);
        expect(loggerSpy.logTrace).toHaveBeenCalled();
    });

    it('should get entity by id', () => {
        mockLoggerInfo = { ...mockLoggerInfo, id: 'testid' };
        const mockResponse: ApiResponse<LoggerInfo> = { data: mockLoggerInfo, hasViolation: false, hasException: false, ruleViolation: new AdditionalInfo() };
        service.getEntity('testid').subscribe(res => {
            expect(res).toEqual(mockResponse);
        });
        const req = httpMock.expectOne(`${apiBaseUrl}/entity/testid`);
        expect(req.request.method).toBe('GET');
        req.flush(mockResponse);
        expect(loggerSpy.logTrace).toHaveBeenCalledWith("LoggerInfoService.getEntity(id: string): ", 'testid');
    });

});