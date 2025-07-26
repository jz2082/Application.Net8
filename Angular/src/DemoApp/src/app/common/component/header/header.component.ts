import { CommonModule } from '@angular/common';
import { AfterViewInit, Component, OnDestroy, OnInit } from '@angular/core';
import { BreakpointObserver, BreakpointState } from '@angular/cdk/layout';
import { Subscription } from 'rxjs';
import { PrimengModule } from '@common/primeng.module';

import { environment } from '@env/environment';
import { LoggerService } from '@common/service/logger.service';
import { AppMenuComponent } from '@common/component/app-menu/app-menu.component';

@Component({
  selector: 'common-header',
  standalone: true,
  imports: [
    CommonModule,
    PrimengModule,
    AppMenuComponent
  ],
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss'
})
export class HeaderComponent implements OnInit, AfterViewInit, OnDestroy {
  subscriptionList:Subscription[] = []; 
  displayMenu = false;
  isScreenSmall = false;
  smallWidthBreakPoint = environment.appSetting.smallWidthBreakPoint;

  constructor(private breakpointObserver: BreakpointObserver, private loggerService: LoggerService) { 
    this.loggerService.logTrace('HeaderComponent.constructor()');
  }

  ngOnInit(): void {
    this.loggerService.logTrace('HeaderComponent.ngOnInit()');
    this.subscriptionList.push(this.breakpointObserver.observe([`(max-width: ${this.smallWidthBreakPoint}px)`]).subscribe((state: BreakpointState) => {
      this.isScreenSmall = state.matches;
    })); 
  }

  ngAfterViewInit() {
    this.loggerService.logTrace('HeaderComponent.ngAfterViewInit()');
  }

  ngOnDestroy() {
    this.loggerService.logTrace('HeaderComponent.ngOnDestroy()');
    this.subscriptionList.forEach(a=>a.unsubscribe());
  }

}