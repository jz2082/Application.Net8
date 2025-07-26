import { Routes } from '@angular/router';
import { PageNotFoundComponent } from '@common/component/page-not-found/page-not-found.component';

export const routes: Routes = [
    {
        path: '',
        data: { preload: false },
        loadChildren: () => import('@common/common.module').then(m => m.CommonModule)
    },
    {
        path: 'pluralsight',
        data: { preload: false },
        loadChildren: () => import('@pluralsight/pluralsight.module').then(m => m.PluralsightModule)
    },
    {
        path: 'demo',
        data: { preload: false },
        loadChildren: () => import('@demo/demo.module').then(m => m.DemoModule)
    },
    { path: '**', component: PageNotFoundComponent }
];
