import { CommonModule } from '@angular/common';
import { Component, AfterViewInit, OnDestroy, OnInit } from '@angular/core';
import { NavigationEnd, Router } from '@angular/router';
import { BreakpointObserver, BreakpointState } from '@angular/cdk/layout';
import { Subscription } from 'rxjs';
import { MenuItem } from 'primeng/api';
import { PrimengModule } from '@common/primeng.module';

import { AppStorage } from '@common/service/app-storage';
import { environment } from '@env/environment';
import { OidcAuthService } from '@common/service/oidcauth.service';
import { LoggerService } from '@common/service/logger.service';

@Component({
  selector: 'common-menu',
  standalone: true,
  imports: [
    CommonModule,
    PrimengModule
  ],
  providers: [
  ],
  templateUrl: './app-menu.component.html',
  styleUrl: './app-menu.component.scss'
})
export class AppMenuComponent implements OnInit, AfterViewInit, OnDestroy {
  subscriptionList: Subscription[] = [];
  smallWidthBreakPoint = environment.appSetting.smallWidthBreakPoint;
  routerSubscription: any;
  menuItemList: MenuItem[] | undefined;
  lastUrl: string = '';

  constructor(private router: Router, private breakpointObserver: BreakpointObserver, private authService: OidcAuthService, private loggerService: LoggerService) {
    this.loggerService.logTrace('AppMenuComponent - constructor()');
    this.subscriptionList.push(this.router.events.subscribe(event => {
      if (event instanceof NavigationEnd) {
        this.setupMenuItemList(this.authService.isLoggedIn);
        this.lastUrl = event.urlAfterRedirects;
        this.setSelectedMenuItem(this.lastUrl != '' ? this.lastUrl : AppStorage.currentRouterLink);
        this.loggerService.logTrace('AppMenuComponent.constructor() - lastUrl', this.lastUrl);
      }
    }));
  }

  ngOnInit(): void {
    this.loggerService.logTrace('AppMenuComponent.ngOnInit()');
    this.setupMenuItemList(this.authService.isLoggedIn);
    this.subscriptionList.push(this.authService.isUserLoggedIn().subscribe(data => {
      this.setupMenuItemList(this.authService.isLoggedIn);
    }));
    this.subscriptionList.push(this.breakpointObserver.observe([`(max-width: ${this.smallWidthBreakPoint}px)`]).subscribe((state: BreakpointState) => {
      this.setSelectedMenuItem(this.lastUrl != '' ? this.lastUrl : AppStorage.currentRouterLink);
    }));
  }

  ngAfterViewInit() {
    this.loggerService.logTrace('AppMenuComponent.ngAfterViewInit() - currentRouterLink: ', AppStorage.currentRouterLink);
    if (this.lastUrl && this.lastUrl != '' && this.lastUrl != AppStorage.currentRouterLink) {
      this.setSelectedMenuItem(this.lastUrl);
    }
  }

  ngOnDestroy() {
    this.loggerService.logTrace('AppMenuComponent.ngOnDestroy()');
    this.subscriptionList.forEach(a => a.unsubscribe());
  }

  setupMenuItemList(isLoggedIn: boolean) {
    this.loggerService.logTrace('AppMenuComponent.getMenuItemList(isLoggedIn: boolean) - isLoggedIn: ', isLoggedIn);
    this.menuItemList = [
      { id: "dashboard", label: 'Dashboard', icon: 'pi pi-home', routerLink: ['/dashboard'], command: (event) => this.handleSelected(event) }
    ] as MenuItem[];
    const pluralsightItemList = [
      { id: "dashboard", label: 'Dashboard', routerLink: ['/pluralsight/dashboard'], command: (event) => this.handleSelected(event) },
      { id: "project", label: 'Project', routerLink: ['/pluralsight/project'], command: (event) => this.handleSelected(event) },
      { id: "profile", label: 'Profile', routerLink: ['/pluralsight/profile'], command: (event) => this.handleSelected(event) },
      { id: "setting", label: 'Setting', routerLink: ['/pluralsight/setting'], command: (event) => this.handleSelected(event) }
    ] as MenuItem[];
    let item = { id: "pluralsight", label: 'Pluralsight', items: pluralsightItemList, command: (event) => this.handleSelected(event) } as MenuItem;
    this.menuItemList.push(item);
    const subItemList = [
      { id: "productlist", label: 'Product', routerLink: ['/demo/productlist'], command: (event) => this.handleSelected(event) },
    ] as MenuItem[];
    item = { id: "demoapp", label: 'Demo App', items: subItemList, command: (event) => this.handleSelected(event) } as MenuItem;
    this.menuItemList.push(item);
    if (isLoggedIn) {
      const subAboutList = [
        { id: "applog", label: 'App Log', routerLink: ['/apploglist'], command: (event) => this.handleSelected(event) },
        { id: "about", label: 'About', routerLink: ['/about'], command: (event) => this.handleSelected(event) },
        { id: "apiabout", label: 'Api About', routerLink: ['/api-about'], command: (event) => this.handleSelected(event) }
      ] as MenuItem[];
      let item = { label: 'About', items: subAboutList, command: (event) => this.handleSelected(event) } as MenuItem;
      this.menuItemList.push(item);
      item = { label: 'Logout', icon: 'pi pi-fw pi-sign-out', routerLink: [''], command: (event) => this.logOut(event) } as MenuItem;
      this.menuItemList.push(item);
    }
    else {
      let item = { label: 'Login', icon: 'pi pi-fw pi-sign-in', routerLink: [''], command: (event) => this.logIn(event) } as MenuItem;
      this.menuItemList.push(item);
    }
  }

  setSelectedMenuItem(selectedRouterLink: string | undefined) {
    this.loggerService.logTrace('AppMenuComponent.setSelectedMenuItem(selectedRouterLink)', selectedRouterLink);
    this.clearMenuSelection(this.menuItemList);
    if ((selectedRouterLink != AppStorage.currentRouterLink) && (selectedRouterLink && 
        (selectedRouterLink.includes('oidclogin') || selectedRouterLink.includes('oidclogout') || selectedRouterLink.includes('signin') || selectedRouterLink.includes('signgout'))
      )) {
      selectedRouterLink = environment.appSetting.appDefaultRouteLink;
    }
    this.setMenuSelection(this.menuItemList, selectedRouterLink);
  }

   handleSelected = (event: any) => {
    this.loggerService.logTrace('AppMenuComponent.handleSelected(event)', event);
    if (event) {
      const item = event.item as MenuItem;
      if (item) {
        if (item.routerLink) {
          if (item.routerLink != AppStorage.currentRouterLink) {
            AppStorage.currentRouterLink = item.routerLink;
          }
          this.router.navigate([AppStorage.currentRouterLink]);
        }
        else {
          this.clearMenuSelection(this.menuItemList);
          item.styleClass = 'menu-selected';
        }
      }
    }
  }

  setMenuSelection(menuItemList: MenuItem[] | undefined, selectedRouterLink: string | undefined): boolean {
    this.loggerService.logTrace('AppMenuComponent.setMenuSelection(menuItemList: MenuItem[], selectedRouterLink: string): boolean', menuItemList, selectedRouterLink);
    let returnValue = false;
    if (menuItemList && selectedRouterLink && selectedRouterLink != '') {
      menuItemList.forEach(x => {
        x.expanded = false;
        if (x.routerLink && x.routerLink == selectedRouterLink) {
          x.styleClass = 'menu-selected';
          AppStorage.currentRouterLink = selectedRouterLink;
          returnValue = true;
        }
        else if (!returnValue && x.items) {
            x.expanded = this.setMenuSelection(x.items, selectedRouterLink);
          }  
      });
    }
    return returnValue;
  }

  clearMenuSelection(menuItemList: MenuItem[] | undefined) {
    if (menuItemList) {
      menuItemList.forEach(x => {
        x.styleClass = '';
        if (x.items) this.clearMenuSelection(x.items);
      });
    }
  }

  public logIn(event: any): void {
    this.loggerService.logTrace('AppMenuComponent.login(event: any)');
    this.authService.login();
  }

  public logOut(event: any): void {
    this.loggerService.logTrace('AppMenuComponent.logout(event: any))');
    this.authService.logout();
  }
}
