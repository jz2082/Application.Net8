import { TestBed } from '@angular/core/testing';
import { OidcAuthService } from './oidcauth.service';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { LoggerService } from '@common/service/logger.service';
import { AdditionalInfo } from '@common/model/additionalInfo';

describe('OidcAuthService', () => {
  let service: OidcAuthService;
  let loggerSpy: jasmine.SpyObj<LoggerService>;

  beforeEach(() => {
    loggerSpy = jasmine.createSpyObj('LoggerService', ['logTrace', 'logDebug', 'logError']);
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [
        OidcAuthService,
        { provide: LoggerService, useValue: loggerSpy }
      ]
    });
    service = TestBed.inject(OidcAuthService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  // Example: test login method if exists
  //it('should call logger on login', () => {
  //  if (service.login) {
  //    spyOn(service, 'login').and.callThrough();
  //    service.login();
  //    expect(service.login).toHaveBeenCalled();
  //    expect(loggerSpy.logTrace).toHaveBeenCalled();
  //  }
  //});

  // Example: test logout method if exists
  //it('should call logger on logout', () => {
  //  if (service.logout) {
  //    spyOn(service, 'logout').and.callThrough();
  //    service.logout();
  //    expect(service.logout).toHaveBeenCalled();
  //    expect(loggerSpy.logTrace).toHaveBeenCalled();
  //  }
  //});

});