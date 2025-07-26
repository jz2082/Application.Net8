import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { TabViewChangeEvent, TabViewModule } from 'primeng/tabview';
import { PrimengModule } from '@common/primeng.module';

import { Dictionary } from '@common/model/dictionary';
import { AppSetting } from '@common/model/appSetting';
import { AppKeyValue } from '@common/model/appKeyValue';
import { HealthCheckResult } from '@common/model/healthCheckResult';
import { environment } from '@env/environment';
import { AppStorage } from '@app/common/service/app-storage';
import { AppInjectorService } from '@app/common/service/app-injector.service';
import { ApiAboutService } from '@app/common/service/apiAbout.service';
import { BaseComponent } from '@app/common/component/base-component.component';
import { ControlMessageComponent } from '@app/common/component/control-message.component';
import { NodeListViewComponent } from '@app/common/component/nodelist-view.component';

@Component({
  selector: 'common-dashboard',
  standalone: true,
  imports: [
    CommonModule,
    PrimengModule,
    TabViewModule,
    NodeListViewComponent,
    ControlMessageComponent
  ],
  providers: [
    ApiAboutService
  ],
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.scss'
})
export class DashboardComponent extends BaseComponent implements OnInit {
  panelIndex: number = 0;
  appSetting: AppSetting = environment.appSetting;
  settingData: AppKeyValue[];
  count: number = -1;
  clientIpAddress: string = '';
  healthCheckResultList: HealthCheckResult[] = [];

  constructor(private apiAboutService: ApiAboutService) {
    super();
    const injector = AppInjectorService.getInjector();
    if (injector == null) console.error('DashboardComponent - constructor - injector is null.');
    this.loggerService.logTrace('DashboardComponent.constructor(private apiAboutService: ApiAboutService)');
    this.module = 'DashboardComponent';
    this.pageTitle = 'Dashboard';
    this.entityForm = this.formBuilder.group({
      summaryMessage: new Dictionary()
    });
    this.settingData = [
      { key: 'appSetting.environment', value: this.appSetting.environment },
      { key: 'appSetting.apiBaseUrl', value: this.appSetting.apiBaseUrl },
      { key: 'appSetting.logLevel', value: this.appSetting.logLevel == 0 ? '0 - trace' :
          (this.appSetting.logLevel == 1 ? '1 - debug' :
            (this.appSetting.logLevel == 2 ? '2 - information' :
              (this.appSetting.logLevel == 3 ? '3 - warning' :
                (this.appSetting.logLevel == 4 ? '4 - error' : '5 - none'))))
      },
      { key: 'isAuthenticated', value: this.authService.isLoggedIn ? 'True' : 'False' },
      { key: 'redirectUrl', value: AppStorage.redirectUrl },
      { key: 'appSetting.aboutMessage', value: this.appSetting.aboutMessage }
    ];
  }

  override ngOnInit(): void {
    super.ngOnInit();
    this.loggerService.logTrace('DashboardComponent.ngOnInit()');
  }

  handleChange(event: TabViewChangeEvent) {
    this.loggerService.logTrace('DashboardComponent.handleChange(event)', event);
    this.panelIndex = event.index;
    switch (event.index) {
      case 0:
        this.loggerService.logTrace('DashboardComponent.handleChange(event) - Dashboard');
        break;
      case 1:
        this.loggerService.logTrace('DashboardComponent.handleChange(event) - ApiHealthCheck');
        this.isLoading = true;
        this.subscriptionList.push(this.apiAboutService.getApiHealthCheckResult().subscribe({
          next: (entityResponse) => {
            this.isLoading = false;
            this.loggerService.logTrace('DashboardComponent.getApiHealthCheckResult()', entityResponse.data);
            this.entityForm.controls['summaryMessage'].enable();
            this.handleHttpResponseError(entityResponse.ruleViolation);
            if (this.isServerValidationPassed) {
              this.healthCheckResultList = entityResponse.data;
            }
          },
          error: (e) => {
            this.isLoading = false;
            this.entityForm.controls['summaryMessage'].enable();
            this.handleHttpError(e);
          }
        }));
        break;
    }
  }
}
