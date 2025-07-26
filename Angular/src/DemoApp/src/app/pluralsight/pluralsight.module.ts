import { NgModule } from '@angular/core';
import { Routes, RouterModule, PreloadAllModules } from '@angular/router';

import { DashboardComponent } from '@pluralsight/component/dashboard/dashboard.component';
import { ProfileComponent } from '@pluralsight/component/profile/profile.component';
import { ProjectComponent } from '@pluralsight/component/project/project.component';
import { SettingComponent } from '@pluralsight/component/setting/setting.component';
import { StatisticComponent } from '@pluralsight/component/statistic/statistic.component';

const routes: Routes = [
  { path: 'dashboard', component: DashboardComponent},
  { path: 'profile', component: ProfileComponent},
  { path: 'project', component: ProjectComponent},
  { path: 'setting', component: SettingComponent},
  { path: 'statistic', component: StatisticComponent}  
];

@NgModule({
  declarations: [],
  imports: [
    RouterModule.forChild(routes)
  ],
  exports: [
    RouterModule
  ]
})

export class PluralsightModule { }