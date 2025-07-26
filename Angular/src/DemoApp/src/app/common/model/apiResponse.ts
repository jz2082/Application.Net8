import { AdditionalInfo } from './additionalInfo';

export class ApiResponse<T> {
    hasViolation: boolean = false;
    hasException: boolean = false;
    ruleViolation : AdditionalInfo = new AdditionalInfo();
    data: T;
}