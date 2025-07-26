import { Component, OnInit, AfterViewInit, OnDestroy } from '@angular/core';
import { FormBuilder, FormGroup, UntypedFormControl } from '@angular/forms';
import { HttpErrorResponse } from '@angular/common/http';
import { Subscription } from 'rxjs';
import { ConfirmationService, MessageService } from 'primeng/api';

import { Dictionary } from '../model/dictionary';
import { SearchRule } from '@common/model/searchRule';
import { Utility } from '@common/service/utility';
import { AdditionalInfo } from '@common/model/additionalInfo';
import { MessageSeverity } from '@common/model/messageSeverity';
import { AppInjectorService } from '@common/service/app-injector.service';
import { OidcAuthService } from '@common/service/oidcauth.service';
import { LoggerService } from '@common/service/logger.service';
import { LoggerInfoService } from '@common/service/loggerInfo.service';

@Component({
  selector: 'base-component',
  standalone: true,
  imports: [
  ],
  providers: [
  ],
  template: ``
})
export abstract class BaseComponent implements OnInit, AfterViewInit, OnDestroy {
  protected authService: OidcAuthService;
  protected loggerService: LoggerService;
  protected loggerInfoService: LoggerInfoService;
  protected messageService: MessageService;
  protected confirmationService: ConfirmationService;
  protected formBuilder: FormBuilder;
  protected application: string = 'Angular Demo App';
  protected module: string = 'BaseComponent';
  
  public isFormLoading: boolean = false;
  public isLoading: boolean = false;
  public isEditable: boolean = true;
  public isAddNew: boolean = false;
  public isFormEdit: boolean = false;
  public isSaving: boolean = false;
  public isDeleting: boolean = false;
  public isServerValidationPassed: boolean = true;
  public subscriptionList: Subscription[] = []; 
  public pageTitle: string = '';
  public defaultSearchRuleList: SearchRule[] = [];
  public entityForm: FormGroup;

  constructor() {
    const injector = AppInjectorService.getInjector();
    if (injector == null) console.error('BaseComponent - constructor - injector is null.');  
    this.authService = injector.get(OidcAuthService);
    this.loggerService = injector.get(LoggerService);
    this.loggerInfoService = injector.get(LoggerInfoService);
    this.messageService = injector.get(MessageService);
    this.confirmationService = injector.get(ConfirmationService);
    this.formBuilder = injector.get(FormBuilder);

    this.entityForm = this.formBuilder.group({
      summaryMessage: new Dictionary()
    });
  }

  ngOnInit() {
    this.loggerService.logTrace('BaseComponent.ngOnInit()');
    this.application = 'Angular Demo App';
    this.module = 'BaseComponent';
  }

  ngAfterViewInit() {
    this.loggerService.logTrace('BaseComponent.ngAfterViewInit()');
  }

  ngOnDestroy() {
    this.loggerService.logTrace('BaseComponent.ngOnDestroy()');   
    this.subscriptionList.forEach(a=>a.unsubscribe());
  }

  protected toastMessage(severity: string, message: string): void {
    setTimeout(() => {
      this.messageService.add({ key: 'componentMsg', severity: severity, summary: severity, detail: message });
    }, 1000);
  }

  protected confirmationMessage(): void {
    this.loggerService.logTrace('AppComponent.confirmationMessage()');
    this.confirmationService.confirm({
      key: "componentDialog",
      message: 'Do you want to delete this record?',
      header: 'Delete Confirmation',
      icon: 'pi pi-info-circle',
      accept: () => {
        this.toastMessage(MessageSeverity.info, 'Action accepted.');
      },
      reject: () => {
        this.toastMessage(MessageSeverity.info, 'Action rejected.');
      }
    });
  }

  protected clearDisplayMessage() {
    this.entityForm?.patchValue({ summaryMessage: new Dictionary() });
  }

  protected handleHttpError(httpError: HttpErrorResponse) {
    this.loggerService.logTrace('BaseComponent.handleHttpError(httpError: HttpErrorResponse): ', httpError);
    if (httpError) {
      let displayMessage = new Dictionary();
      displayMessage.setValue('summaryMessage', httpError.message);
      if (httpError.error) {
        displayMessage.setValue('summaryMessage', JSON.stringify(httpError.error));
      }
      this.entityForm?.patchValue({ summaryMessage: displayMessage })
    }
  }

  protected handleHttpResponseError(httpResponse: AdditionalInfo) {
    this.loggerService.logTrace('BaseComponent.handleHttpResponseError(httpResponse: AdditionalInfo): ', httpResponse);
    if (httpResponse.count > 0) {
      let displayMessage = new Dictionary();
      this.isServerValidationPassed = false;
      let additionalInfo = httpResponse.value;
      Object.entries(additionalInfo).forEach(([key, value]) => {
        const keyName = Utility.findFormControlName(this.entityForm, '', 0, '', key);
        let keyValue = value;
        displayMessage.setValue(keyName, keyValue);
      });
      this.entityForm?.patchValue({ summaryMessage: displayMessage });
    }
    else {
      this.isServerValidationPassed = true;
    }
  }

protected getFormControlErrorCount(formControl: UntypedFormControl): number {
		let count:number = 0;

		if (formControl) {
			for (let controlError in formControl.errors) {
				if (controlError) {
					count ++;
				}
			}
		}
		return count;
	}

	protected deleteFormControlError(formControl: UntypedFormControl, controlError:string): void {
		let count:number = 0;
		if (formControl && formControl.errors && formControl.errors[controlError]) {
			delete formControl.errors[controlError];
			for (let error in formControl.errors) {
				if (error) {
					count ++;
				}
			}
			if (count == 0) {
				formControl.setErrors(null);
			}
		}
	}
}
