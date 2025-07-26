import { Injectable } from '@angular/core';
import { environment } from '@env/environment';
import { LogLevel } from '@common/model/logLevel';

@Injectable()
export class LoggerService {

  static logTrace(messageText: string, ...messageObject: any): void {
    if (environment.appSetting.logLevel <= LogLevel.trace) {
      let message = '';
      if (messageObject != null && messageObject.length !== 0) {
        message = LoggerService.getLogMessage('TRACE', messageText, messageObject);
      } else {
        message = LoggerService.getLogMessage('TRACE', messageText);
      }
      console.info(message);
    }
  }

  static logDebug(messageText: string, ...messageObject: any): void {
    if (environment.appSetting.logLevel <= LogLevel.debug) {
      let message = '';
      if (messageObject != null && messageObject.length !== 0) {
        message = LoggerService.getLogMessage('DEBUG', messageText, messageObject);
      } else {
        message = LoggerService.getLogMessage('DEBUG', messageText);
      }
      console.info(message);
    }
   }

  static logInformation(messageText: string,  ...messageObject: any): void {
    if (environment.appSetting.logLevel <= LogLevel.information) {
      let message = '';
      if (messageObject != null && messageObject.length !== 0) {
        message = LoggerService.getLogMessage('INFO', messageText, messageObject);
      } else {
        message = LoggerService.getLogMessage('INFO', messageText);
      }
      console.info(message);
    }
  }

  static logError(messageText: string, ...messageObject: any): void {
    if (environment.appSetting.logLevel <= LogLevel.error) {
      let message = '';
      if (messageObject != null && messageObject.length !== 0) {
        message = LoggerService.getLogMessage('ERROR', messageText, messageObject);
      } else {
        message = LoggerService.getLogMessage('ERROR', messageText);
      }
      console.error(message);
    }
  }
  
  private static getLogMessage(level: string, messageText: string, ...messageObject: any): string {
    let message = `${level}: ${messageText}`;
    let itemMessage = '';
    if (messageObject != null && messageObject.length !== 0) {
      for (const item of messageObject) {
        if (itemMessage === '') {
          itemMessage = LoggerService.getObjectStringValue(item);
        } else {
          itemMessage = itemMessage + ', ' + LoggerService.getObjectStringValue(item);
        }
      }
      message = message + ' - ' + itemMessage;
    }
    return (message);
  }

  private static getObjectStringValue(messageObject: any): string {
    let message = '';
    if (messageObject) {
      if (typeof(messageObject) === 'string') {
        message = messageObject;
      } else {
        message = JSON.stringify(messageObject);
      }
    } else {
      message = 'null';
    }
    return (message);
  }

  constructor() { }

  logTrace(messageText: string, ...messageObject: any): void {
    if (environment.appSetting.logLevel <= LogLevel.trace) {
      let message = '';
      if (messageObject != null && messageObject.length !== 0) {
        message = this.getLogMessage('TRACE', messageText, messageObject);
      } else {
        message = this.getLogMessage('TRACE', messageText);
      }
      console.info(message);
    }
  }

  logDebug(messageText: string, ...messageObject: any): void {
    if (environment.appSetting.logLevel <= LogLevel.debug) {
      let message = '';
      if (messageObject != null && messageObject.length !== 0) {
        message = this.getLogMessage('DEBUG', messageText, messageObject);
      } else {
        message = this.getLogMessage('DEBUG', messageText);
      }
      console.info(message);
    }
   }

  logInformation(messageText: string,  ...messageObject: any): void {
    if (environment.appSetting.logLevel <= LogLevel.information) {
      let message = '';
      if (messageObject != null && messageObject.length !== 0) {
        message = this.getLogMessage('INFO', messageText, messageObject);
      } else {
        message = this.getLogMessage('INFO', messageText);
      }
      console.info(message);
    }
  }

  logError(messageText: string, ...messageObject: any): void {
    if (environment.appSetting.logLevel <= LogLevel.error) {
      let message = '';
      if (messageObject != null && messageObject.length !== 0) {
        message = this.getLogMessage('ERROR', messageText, messageObject);
      } else {
        message = this.getLogMessage('ERROR', messageText);
      }
      console.error(message);
    }
  }

  private getLogMessage(level: string, messageText: string, ...messageObject: any): string {
    let message = `${level}: ${messageText}`;
    let itemMessage = '';
    if (messageObject != null && messageObject.length !== 0) {
      for (const item of messageObject) {
        if (itemMessage === '') {
          itemMessage = this.getObjectStringValue(item);
        } else {
          itemMessage = itemMessage + ', ' + this.getObjectStringValue(item);
        }
      }
      message = message + ' - ' + itemMessage;
    }
    return (message);
  }

  private getObjectStringValue(messageObject: any): string {
    let message = '';
    if (messageObject) {
      if (typeof(messageObject) === 'string') {
        message = messageObject;
      } else {
        message = JSON.stringify(messageObject);
      }
    } else {
      message = 'null';
    }
    return (message);
  }
}
