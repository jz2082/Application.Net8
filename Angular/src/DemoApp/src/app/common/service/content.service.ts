import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import { LoggerService } from '@common/service/logger.service';
import { Dictionary } from '@common/model/dictionary';


@Injectable()
export class ContentService {
    private serviceUrl = 'data/custom-validator-message.json';

    constructor(private http: HttpClient, private loggerService: LoggerService) {
    }

    getValidatorMessage(): Observable<Dictionary> {
        return this.http.get<Dictionary>(this.serviceUrl);
    }
}