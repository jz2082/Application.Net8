import { AppKeyValue } from './appKeyValue';

export class HealthCheckResult {
    status: number = 0;
    description: number = 0;
    data: AppKeyValue[];

    constructor() {
        this.data = [];
    }
}
