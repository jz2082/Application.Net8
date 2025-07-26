import { NgModule } from '@angular/core';
import { Routes, RouterModule, PreloadAllModules } from '@angular/router';

import { ProductListComponent } from '@demo/component/product/product-list.component';

const routes: Routes = [
  { path: 'productlist', component: ProductListComponent},
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

export class DemoModule { }