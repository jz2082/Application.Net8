import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { TreeNode } from 'primeng/api';
import { PrimengModule } from '@common/primeng.module';

import { Dictionary } from '@common/model/dictionary';
import { AppKeyValue } from '@common/model/appKeyValue';
import { ApiTokenInfo } from '@common/model/apiTokenInfo';
import { Utility } from '@common/service/utility';
import { ApiAboutService } from '@common/service/apiAbout.service';
import { BaseComponent } from '@common/component/base-component.component';
import { ControlMessageComponent } from '@common/component/control-message.component';

@Component({
  selector: 'common-api-about',
  standalone: true,
  imports: [
    CommonModule,
    PrimengModule, 
    ControlMessageComponent
  ],
  providers: [
    ApiAboutService
  ],
  templateUrl: './api-about.component.html',
  styleUrl: './api-about.component.scss'
})
export class ApiAboutComponent extends BaseComponent implements OnInit {
  panelIndex: number = 0;
  apiSettingList: TreeNode[] = [];
  apiUserInfoList: TreeNode[] = [];
  apiUserClaimList: AppKeyValue[] = [];
  apiTokenInfo = {} as ApiTokenInfo;
  
  constructor(private apiAboutService: ApiAboutService) { 
    super();
    this.loggerService.logTrace('ApiAboutComponent.constructor()');
  }

  override ngOnInit(): void {
    super.ngOnInit();
    this.loggerService.logTrace('ApiAboutComponent.ngOnInit()');
    this.module = 'ApiAboutComponent';
    this.pageTitle = 'Api About';
    this.entityForm = this.formBuilder.group({
      summaryMessage: new Dictionary()
    });
    this.getApiSeting();
  }

  handleChange(event: { index: number; }) {
    this.loggerService.logTrace('ApiAboutComponent.handleChange(event)', event);
    this.panelIndex = event.index;
    switch (event.index) {
      case 0:
        this.loggerService.logTrace('ApiAboutComponent.handleChange(event) - ApiSetting');
        this.getApiSeting();
        break;
      default:
        this.loggerService.logTrace('ApiAboutComponent.handleChange(event) - ApiSetting');
        this.getApiSeting();
        break;
    }
  }

  getApiSeting() {
    this.isLoading = true;
    this.subscriptionList.push(this.apiAboutService.getApiSeting().subscribe({
      next: (entityResponse) => {
        this.isLoading = false;
        this.apiSettingList = Utility.convertToTreeNodeList(JSON.stringify(entityResponse.data));
        this.loggerService.logTrace('ApiAboutComponent.getApiSeting()', entityResponse);
        this.entityForm.controls['summaryMessage'].enable();
        this.handleHttpResponseError(entityResponse.ruleViolation);
      },
      error: (e) => {
        this.isLoading = false;
        this.entityForm.controls['summaryMessage'].enable();
        this.handleHttpError(e);
      }
    }));
  }
}