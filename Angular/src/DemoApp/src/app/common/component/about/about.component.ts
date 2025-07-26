import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { TabsModule } from 'primeng/tabs';
import { AccordionModule } from 'primeng/accordion';
import { TableModule } from 'primeng/table';

import { AppSetting } from '@common/model/appSetting';
import { environment } from '@env/environment';
import { AppStorage } from '@common/service/app-storage';
import { AppUser } from '@common/model/appUser';
import { AppClaim } from '@common/model/appClaim';
import { BaseComponent } from '@common/component/base-component.component';

@Component({
  selector: 'common-about',
  standalone: true,
  imports: [
    CommonModule,
    TabsModule,
    AccordionModule,
    TableModule
  ],
  providers: [
  ],
  templateUrl: './about.component.html',
  styleUrl: './about.component.scss'
})
export class AboutComponent extends BaseComponent implements OnInit {
  override pageTitle = 'About';
  appSetting: AppSetting = environment.appSetting;

  constructor() {
    super();
  }

  override ngOnInit(): void {
    super.ngOnInit();
    this.loggerService.logTrace('AboutComponent.ngOnInit()');
    this.module = 'AboutComponent';
  }

  override ngAfterViewInit() {
    this.loggerService.logTrace('AboutComponent.ngAfterViewInit()');
    // interval(100000).subscribe(() => {
    //   this.loggerService.logTrace('AboutComponent.ngAfterViewInit() - isAuthenticated, user: ', this.isAuthenticated, this.user);
    // });
  }

  public get isAuthenticated(): boolean {
    return this.authService.isLoggedIn;
  }

  public get redirectAppUrl(): string {
    return AppStorage.redirectUrl;
  }

  public get user(): AppUser {
    return this.authService.appUser;
  }

  public get claimList(): AppClaim[] {
    return this.authService.appUser.claimList;
  }
}

