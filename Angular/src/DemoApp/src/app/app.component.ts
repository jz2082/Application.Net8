import { CommonModule } from '@angular/common';
import { Component, OnInit, AfterViewInit, OnDestroy} from '@angular/core';
import { BreakpointObserver, BreakpointState } from '@angular/cdk/layout';
import { RouterOutlet } from '@angular/router';
import { Subscription } from 'rxjs';
import { ConfirmationService, MessageService } from 'primeng/api';
import { PrimengModule } from '@common/primeng.module';

import { environment } from '@env/environment';
import { AppStorage } from '@common/service/app-storage';
import { MessageSeverity } from '@common/model/messageSeverity';
import { LoggerService } from '@common/service/logger.service';
import { HeaderComponent } from '@common/component/header/header.component';
import { FooterComponent } from '@common/component/footer/footer.component';
import { AppMenuComponent } from '@common/component/app-menu/app-menu.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    CommonModule, 
    RouterOutlet, 
    PrimengModule,
    HeaderComponent,
    FooterComponent,
    AppMenuComponent
  ],
  providers: [
  ],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})

export class AppComponent implements OnInit, AfterViewInit, OnDestroy {
  subscriptionList:Subscription[] = []; 
  isScreenSmall = false;
  smallWidthBreakPoint = environment.appSetting.smallWidthBreakPoint;

  constructor(private breakpointObserver: BreakpointObserver, 
    private messageService: MessageService, 
    private confirmationService: ConfirmationService,
    private loggerService: LoggerService) {
    this.loggerService.logTrace('AppComponent - constructor()');
  }
  
  ngOnInit() {
    this.loggerService.logTrace('AppComponent.ngOnInit()');
    this.subscriptionList.push(
      this.breakpointObserver.observe([`(max-width: ${this.smallWidthBreakPoint}px)`]).subscribe((state: BreakpointState) => {
        this.isScreenSmall = state.matches;
      })
    );
    //this.toastMessage(MessageSeverity.info, 'AppComponent.ngOnInit()');
  }

  ngAfterViewInit() {
    this.loggerService.logTrace('AppComponent.ngAfterViewInit() - currentRouterLink: ', AppStorage.currentRouterLink);
  }

  ngOnDestroy() {
    this.loggerService.logTrace('AppComponent.ngOnDestroy()');
    this.subscriptionList.forEach(a => a.unsubscribe());
    //this.toastMessage(MessageSeverity.info, 'AppComponent.ngOnDestroy()');
  }

  toastMessage(severity: string, message: string): void {
    this.loggerService.logTrace('AppComponent.toastMessage(severity: string, message: string)');
    setTimeout(() => {
      this.messageService.add({ key: 'componentMsg', severity: severity, summary: severity, detail: message });
    }, 1000);
  }

  confirmationMessage(): void {
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
}
