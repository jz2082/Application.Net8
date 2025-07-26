import { NgModule } from '@angular/core';
import { Routes, RouterModule, PreloadAllModules } from '@angular/router';

import { DashboardComponent } from '@common/component/dashboard/dashboard.component';
import { PageNotFoundComponent } from '@common/component/page-not-found/page-not-found.component';
import { UnauthorizedAccessComponent } from '@common/component/unauthorized-access/unauthorized-access.component';
import { OidcloginCallbackComponent } from './component/oidclogin-callback.component';
import { OidcsilentCallbackComponent } from './component/oidcsilent-callback.component';
import { OidclogoutCallbackComponent } from './component/oidclogout-callback.component';
import { AboutComponent } from './component/about/about.component';
import { OidcAuthGuardService } from './service/oidcauth-guard.service';
import { ApiAboutComponent } from './component/api-about/api-about.component';
import { ApplogListComponent } from './component/applog/applog-list.component';

const routes: Routes = [
  { path: 'dashboard', component: DashboardComponent},
  { path: 'oidclogin-callback', component: OidcloginCallbackComponent },
  { path: 'oidcsilent-callback', component: OidcsilentCallbackComponent },
  { path: 'oidclogout-callback', component: OidclogoutCallbackComponent },
  { path: 'apploglist', component: ApplogListComponent, canActivate: [OidcAuthGuardService] },
  { path: 'about', component: AboutComponent, canActivate: [OidcAuthGuardService] },
  { path: 'api-about', component: ApiAboutComponent, canActivate: [OidcAuthGuardService] },
  { path: 'not-found', component: PageNotFoundComponent },
  { path: 'unauthorized-access', component: UnauthorizedAccessComponent },
  { path: '', redirectTo: '/dashboard', pathMatch: 'full' }
];

@NgModule({
  declarations: [
  ],
  imports: [
    RouterModule.forChild(routes)
  ],
  providers: [
  ],
  exports: [
    RouterModule
  ]
})

export class CommonModule { }