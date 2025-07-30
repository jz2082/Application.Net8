import { LoggerService } from './logger.service';
import { environment } from '@env/environment';
import { LogLevel } from '@common/model/logLevel';

describe('LoggerService', () => {
  let service: LoggerService;

  beforeEach(() => {
    service = new LoggerService();
    // Set log level to trace for all tests to ensure logging occurs
    environment.appSetting.logLevel = LogLevel.trace;
  });

  afterEach(() => {
    // Optionally reset log level if needed
  });

  describe('static methods', () => {
    it('should log trace message (static)', () => {
      spyOn(console, 'info');
      LoggerService.logTrace('trace message');
      expect(console.info).toHaveBeenCalledWith('TRACE: trace message');
    });

    it('should log debug message (static)', () => {
      spyOn(console, 'info');
      LoggerService.logDebug('debug message');
      expect(console.info).toHaveBeenCalledWith('DEBUG: debug message');
    });

    it('should log info message (static)', () => {
      spyOn(console, 'info');
      LoggerService.logInformation('info message');
      expect(console.info).toHaveBeenCalledWith('INFO: info message');
    });

    it('should log error message (static)', () => {
      spyOn(console, 'error');
      LoggerService.logError('error message');
      expect(console.error).toHaveBeenCalledWith('ERROR: error message');
    });

    //it('should format object in log message (static)', () => {
    //  spyOn(console, 'info');
    //  LoggerService.logTrace('trace with object', { foo: 'bar' });
    //  expect(console.info).toHaveBeenCalledWith(jasmine.stringMatching('TRACE: trace with object - {"foo":"bar"}'));
    //});
  });

  describe('instance methods', () => {
    it('should log trace message', () => {
      spyOn(console, 'info');
      service.logTrace('trace message');
      expect(console.info).toHaveBeenCalledWith('TRACE: trace message');
    });

    it('should log debug message', () => {
      spyOn(console, 'info');
      service.logDebug('debug message');
      expect(console.info).toHaveBeenCalledWith('DEBUG: debug message');
    });

    it('should log info message', () => {
      spyOn(console, 'info');
      service.logInformation('info message');
      expect(console.info).toHaveBeenCalledWith('INFO: info message');
    });

    it('should log error message', () => {
      spyOn(console, 'error');
      service.logError('error message');
      expect(console.error).toHaveBeenCalledWith('ERROR: error message');
    });

    //it('should format object in log message', () => {
    //  spyOn(console, 'info');
    //  service.logTrace('trace with object', { foo: 'bar' });
    //  expect(console.info).toHaveBeenCalledWith(jasmine.stringMatching('TRACE: trace with object - [{"foo":"bar"}]'));
    //});
  });

  describe('private helpers', () => {
    it('should stringify object', () => {
      // @ts-ignore
      const result = service['getObjectStringValue']({ foo: 'bar' });
      expect(result).toBe('{"foo":"bar"}');
    });

    it('should return string as is', () => {
      // @ts-ignore
      const result = service['getObjectStringValue']('hello');
      expect(result).toBe('hello');
    });

    it('should return "null" for falsy', () => {
      // @ts-ignore
      const result = service['getObjectStringValue'](null);
      expect(result).toBe('null');
    });
  });
});