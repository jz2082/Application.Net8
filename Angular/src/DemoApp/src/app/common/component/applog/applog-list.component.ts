import { CommonModule } from '@angular/common';
import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { Table, TableRowSelectEvent } from 'primeng/table';
import { PrimengModule } from '@common/primeng.module';

import { Dictionary } from '@common/model/dictionary';
import { LoggerInfo } from '@common/model/LoggerInfo';
import { LoggerInfoService } from '@common/service/loggerInfo.service';
import { BaseComponent } from '@common/component/base-component.component';
import { ControlMessageComponent } from '@common/component/control-message.component';
import { NodeListViewComponent } from '@common/component/nodelist-view.component';
import { ApplogEditComponent } from '@common/component/applog/applog-edit.component';

@Component({
  selector: 'common-applog-list',
  standalone: true,
  imports: [
    CommonModule,
    PrimengModule,
    NodeListViewComponent,
    ControlMessageComponent,
    ApplogEditComponent,
  ],
  providers: [
    LoggerInfoService
  ],
  templateUrl: './applog-list.component.html',
  styleUrls: ['./applog-list.component.scss']
})
export class ApplogListComponent extends BaseComponent implements OnInit {
  @ViewChild('dt') table: Table;
  loggerInfoList: LoggerInfo[];
  selectedEntity = {} as LoggerInfo;
  displayDialog: boolean;

  constructor() {
    super();
    this.loggerService.logTrace('ApplogListComponent.constructor(private loggerInfoService: LoggerInfoService)');
  }

  override ngOnInit(): void {
    super.ngOnInit();
    this.loggerService.logTrace('ApplogListComponent.ngOnInit()');
    this.module = 'ApplogListComponent';
    this.pageTitle = 'LoggerInfo List';
    this.entityForm = this.formBuilder.group({
      summaryMessage: new Dictionary()
    });
    this.LoadEntityList();
  }

  LoadEntityList = () => {
    this.loggerService.logTrace('ApplogListComponent.LoadEntityList()');
    this.isLoading = true;
    this.subscriptionList.push(this.loggerInfoService.getEntityList().subscribe({
      next: (entityListResponse) => {
        this.isLoading = false;
        this.loggerInfoList = entityListResponse.data;
        this.loggerService.logTrace('ApplogListComponent.lazyLoadEntityList()', this.loggerInfoList);
        this.handleHttpResponseError(entityListResponse.ruleViolation);
      },
      error: (e) => {
        this.isLoading = false;
        this.entityForm.controls["summaryMessage"].enable();
        this.handleHttpError(e);
      }
    }));
    return false;
  }

  onDialogHide(event: any) {
    this.loggerService.logTrace('ApplogListComponent.onDialogHide(event)', event);
    this.isAddNew = false;
  }

  onRowSelect(event: TableRowSelectEvent) {
    this.loggerService.logTrace('ApplogListComponent.onRowSelect(event)', event);
    this.isAddNew = false;
    this.selectedEntity = { ...event.data };
    this.displayDialog = true;
  }

  refreshDataList = (forceRefresh: boolean = false) => {
    this.loggerService.logTrace('ApplogListComponent.refreshDataList(forceRefresh: boolean = false)', forceRefresh);
    this.displayDialog = false;
    if (forceRefresh) {
    }
  };

}