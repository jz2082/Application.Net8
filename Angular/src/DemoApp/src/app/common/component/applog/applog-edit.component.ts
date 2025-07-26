import { CommonModule } from '@angular/common';
import { Component, Input, OnInit } from '@angular/core';
import { Validators } from '@angular/forms';
import { PrimengModule } from '@common/primeng.module';

import { Dictionary } from '@common/model/dictionary';
import { LoggerInfo } from '@common/model/LoggerInfo';
import { Utility } from '@common/service/utility';
import { MessageSeverity } from '@common/model/messageSeverity';
import { BaseComponent } from '@common/component/base-component.component';
import { ControlMessageComponent } from '@common/component/control-message.component';
import { LoadingComponent } from '@common/component/loading/loading.component';

@Component({
  selector: 'common-applog-edit',
  standalone: true,
  imports: [
    CommonModule,
    PrimengModule,
    ControlMessageComponent,
    LoadingComponent
  ],
  providers: [
  ],
  templateUrl: './applog-edit.component.html',
  styleUrls: ['./applog-edit.component.css']
})
export class ApplogEditComponent extends BaseComponent implements OnInit {
  @Input() entity: LoggerInfo;
  @Input() refreshFunc: (forceRefresh: boolean) => void;

  originalEntity = {} as LoggerInfo;

  constructor() { 
    super();
    this.loggerService.logTrace('ApplogEditComponent.constructor(private loggerInfoService: LoggerInfoService, private confirmationService: ConfirmationService)');
  }

  override ngOnInit(): void {
    super.ngOnInit();
    this.loggerService.logTrace('ApplogEditComponent.ngOnInit()');
    this.module = 'ApplogEditComponent';
    this.pageTitle = 'Edit LoggerInfo';
    this.entityForm = this.formBuilder.group({
      environment: [''],
      machineName: [''],
      loggerName: [''],
      level: [''],
      timeStamp: [''],
      userName: [''],
      application: [''],
      module: ['', Validators.required],
      method: ['', Validators.required],
      message: ['', Validators.required],
      summaryMessage: new Dictionary()
    });
    this.getEntity(this.entity.id);
  }
  
  getEntity(entityId: string) {
    this.loggerService.logTrace('ApplogEditComponent.getEntity(entityId: string)', entityId);
    this.isLoading = true;
    this.subscriptionList.push(this.loggerInfoService.getEntity(entityId).subscribe({
      next: (entityResponse) => {
        this.isLoading = false;
        this.entity = entityResponse.data;
        this.loggerService.logTrace('ApplogEditComponent.getEntity()', this.entity);
        this.entityForm.controls['summaryMessage'].enable();
        this.handleHttpResponseError(entityResponse.ruleViolation);
        if (this.isServerValidationPassed) {
          this.entity.timeStamp = Utility.convertDateToDate(this.entity.timeStamp);
          Object.assign(this.originalEntity, this.entity);
          this.entityForm.patchValue(this.entity);
          this.entityForm.disable();
          this.loggerService.logTrace('ApplogEditComponent.getEntity() - assign', this.entity.timeStamp);
        }
      },
      error: (e) => {
        this.isLoading = false;
        this.entityForm.controls['summaryMessage'].enable();
        this.handleHttpError(e);
        this.entity = undefined;
        this.entityForm.disable();
      }
    }));
  }

  edit() {
    this.isFormEdit = true;
    this.entityForm.enable();
    this.entityForm.controls["environment"].disable();
  }

  cancel() {
    this.loggerService.logTrace('ApplogEditComponent.cancel()');
    if (this.entityForm.dirty && this.entityForm.touched) {
      this.toastMessage(MessageSeverity.warn, 'Your changes have been canceled.');
    }
    else
    {
      this.toastMessage(MessageSeverity.warn, 'No changes.');
    }
    this.clearDisplayMessage();
    Object.assign(this.entity, this.originalEntity);
    this.entityForm.reset(this.entity);
    this.isFormEdit = false;
    this.entityForm.disable();
  }
  
  onSubmit() {
    if (!this.entityForm.dirty) {
      this.cancel();
      return;
    }
    this.entity = { ...this.entity, ...this.entityForm.value };
    this.loggerService.logTrace('ApplogEditComponent.onSubmit()', this.entityForm.invalid, this.entity);
    if (!this.entityForm.invalid) {
      this.clearDisplayMessage();
      this.saveEntity();
    }
    else {
      this.entityForm.markAllAsTouched();
    }
  }

  onDelete() {
    this.loggerService.logTrace('ApplogEditComponent.onDelete()');
    this.confirmationService.confirm({
      key: "componentDialog",
      message: 'Do you want to delete this record?',
      header: 'Delete Confirmation',
      icon: 'pi pi-info-circle',
      accept: () => {
        this.deleteEntity();
      },
      reject: () => {
        this.toastMessage(MessageSeverity.info, 'Action rejected.');
      }
    });
  }

  saveEntity = () => {
    this.loggerService.logTrace('ApplogEditComponent.saveEntity()');
    this.toastMessage(MessageSeverity.warn, 'Save Entity.');
    this.isFormEdit = false;
    this.entityForm.disable();
  }

  deleteEntity = () => {
    this.loggerService.logTrace('ApplogEditComponent.deleteEntity()');
    this.toastMessage(MessageSeverity.warn, 'Delete Entity.');
  }
}