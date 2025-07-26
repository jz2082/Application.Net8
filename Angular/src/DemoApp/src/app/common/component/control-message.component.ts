import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormGroup } from '@angular/forms';
import { Subscription } from 'rxjs';
import { MessageService } from 'primeng/api';
import { MessageModule } from 'primeng/message';

import { Dictionary } from '@common/model/dictionary';
import { Utility } from '@common/service/utility';
import { LoggerService } from '@common/service/logger.service';
import { ContentService } from '@common/service/content.service';

@Component({
  selector: 'control-message',
  standalone: true,
  imports: [
    CommonModule,
    MessageModule
  ],
  providers: [
    ContentService
  ],
  template: 
  `
  <p-message severity="error" [text]="errorMessage" [escape]="false"  [style]="{'justify-content': 'left'}" *ngIf="errorMessage"></p-message>
  `
})
export class ControlMessageComponent implements OnInit, OnDestroy {
  @Input() form: FormGroup | undefined;
  @Input() arrayName: string | undefined;
  @Input() index: number | undefined;
  @Input() groupName: string | undefined;
  @Input() controlName: string | undefined;
  @Input() firendlyName: string | undefined;

  private subscriptionList:Subscription[] = []; 
  private validationMessage: Dictionary;

  constructor(private contentService: ContentService, private messageService: MessageService, private loggerService: LoggerService) {
    this.validationMessage = new Dictionary();
  }

  ngOnInit(): void {
    this.loggerService.logTrace('ControlMessageComponent.ngOnInit()', this.arrayName, this.index, this.controlName, this.firendlyName);
    this.subscriptionList.push(this.contentService.getValidatorMessage().subscribe({
      next: (messageResponse) => {
        // [{"value":{
        // "required":"$firendlyName is required.",
        // "minlength":"$firendlyName must be at least $requiredLength characters.",
        // "maxlength":"$firendlyName cannot exceed $requiredLength characters.",
        // "numberRange":"$firendlyName must between $minimumRange (lowest) and $maximumRange (highest)."
        // }}]
        this.validationMessage.value = messageResponse.value;
        this.loggerService.logTrace("validationMessage", messageResponse.value);
      },
      error: (e) => this.loggerService.logError('ControlMessageComponent.ngOnInit().getvalidatorMessage', e)
    }));
  }

  ngOnDestroy() {
    this.loggerService.logTrace('ControlMessageComponent.ngOnDestroy()');
    this.subscriptionList.forEach(a=>a.unsubscribe());
  }

  get errorMessage() {
    let returnValue = '';
    if (this.form) {
      // get server side validation message
      if (this.form.contains("summaryMessage")) {
        const displayMessage = <Dictionary>this.form.controls['summaryMessage'].value;
        if (displayMessage) {
          let angularControlName = this.controlName;
          if (this.arrayName) {
            angularControlName = this.arrayName + "-" + this.index + "-" + this.controlName;
          }
          if (angularControlName) {
            returnValue = displayMessage.getValue(angularControlName);
          }
          if (returnValue) {
            returnValue = Utility.replaceAll(returnValue, "\r\n", "<br />");
          }
        }
      }
      // get client side validation message
      const formControl = Utility.findFormControl(
        this.form, 
        this.arrayName ? this.arrayName : '', 
        this.index ? this.index : 0, 
        this.groupName ? this.groupName : '', 
        this.controlName ? this.controlName : ''
      );
      if (formControl && formControl.touched && formControl.errors) {
        let message = '';
        for (let controlError in formControl.errors) {
          message = this.validationMessage.getValue(controlError).replace('$firendlyName', this.firendlyName ? this.firendlyName : '');
          if (formControl.errors.hasOwnProperty(controlError)) {
            if (message == '') {
              message = controlError + ': ' + this.validationMessage.getValue(controlError);
              for (const key in formControl.errors[controlError]) {
                message += ' (key = ' + key + ' value = ' + formControl.errors[controlError][key] + ')';
              }
            }
            else {
              for (const key in formControl.errors[controlError]) {
                message = message.replace('$' + key, formControl.errors[controlError][key]);
              }
            }
          }
          if (returnValue == '') {
            returnValue = message;
          }
          else {
            returnValue += '<br />' + message;
          }
        }
      }
    }
    return (returnValue);
  }
}