import { BaseEntity } from "./baseEntity";

export class LoggerInfo extends BaseEntity {
  environment: string | undefined;
  machineName: string | undefined;
  loggerName: string | undefined;
  level: string | undefined;
  threadName: string | undefined;
  timeStamp: Date | undefined;
  application: string | undefined;
  module: string | undefined;
  method: string | undefined;
  userName: string | undefined;
  executionTime: number | undefined;
  isJsonMessage: boolean | undefined;
  message: string | undefined;
  renderedType: string | undefined;
  renderedInfo: string | undefined;
  exceptionMessage: string | undefined;
  exceptionType: string | undefined;
  exceptionInfo: string | undefined;
}
