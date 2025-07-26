import { CommonModule } from '@angular/common';
import { Component, OnInit, ViewChild } from '@angular/core';
import { Table, TableRowSelectEvent } from 'primeng/table';
import { PrimengModule } from '@common/primeng.module';

import { Dictionary } from '@common/model/dictionary';
import { Product } from '@demo/model/product';
import { ProductService } from '@demo/service/product.service';
import { BaseComponent } from '@common/component/base-component.component';
import { ControlMessageComponent } from '@common/component/control-message.component';
import { NodeListViewComponent } from '@common/component/nodelist-view.component';
import { LoadingComponent } from '@common/component/loading/loading.component';
import { ProductEditComponent } from './product-edit.component';

@Component({
  selector: 'demo-product-list',
  standalone: true,
  imports: [
    CommonModule,
    PrimengModule,
    ProductEditComponent,
    NodeListViewComponent,
    ControlMessageComponent,
    LoadingComponent
  ],
  providers: [
    ProductService
  ],
  templateUrl: './product-list.component.html',
  styleUrl: './product-list.component.scss'
})
export class ProductListComponent extends BaseComponent implements OnInit {
  @ViewChild('dt') table: Table;
  productList: Product[];
  selectedEntity = {} as Product;
  displayDialog: boolean;

  constructor(private productService: ProductService) {
    super();
    this.loggerService.logTrace('ProductListComponent.constructor(private productService: ProductService)');
  }

  override ngOnInit(): void {
    super.ngOnInit();
    this.loggerService.logTrace('ProductListComponent.ngOnInit()');
    this.module = 'ProductListComponent';
    this.pageTitle = 'Product List';
    this.entityForm = this.formBuilder.group({
      summaryMessage: new Dictionary()
    });
    this.LoadEntityList();
  }

  LoadEntityList = () => {
    this.loggerService.logTrace('ProductListComponent.LoadEntityList()');
    this.isLoading = true;
    this.subscriptionList.push(this.productService.getEntityList().subscribe({
      next: (entityListResponse) => {
        this.isLoading = false;
        this.productList = entityListResponse.data;
        this.loggerService.logTrace('ProductListComponent.LoadEntityList()', this.productList);
        this.handleHttpResponseError(entityListResponse.ruleViolation);
      },
      error: (e) => {
        this.isLoading = false;
        this.entityForm.controls["summaryMessage"].enable();
        this.handleHttpError(e);
      }
    }));
    return false;
  }

  onDialogShowToAdd(event: any) {
    this.loggerService.logTrace('ProductListComponent.onDialogShowToAdd(event)', event);
    this.isAddNew = true;
    this.displayDialog = true;
  }

  onDialogHide(event: any) {
    this.loggerService.logTrace('ProductListComponent.onDialogHide(event)', event);
    this.isAddNew = false;
  }

  onRowSelect(event: TableRowSelectEvent) {
    this.loggerService.logTrace('ProductListComponent.onRowSelect(event)', event);
    this.isAddNew = false;
    this.selectedEntity = { ...event.data };
    this.displayDialog = true;
  }

  refreshDataList = (forceRefresh: boolean = false) => {
    this.loggerService.logDebug('ProductListComponent.refreshDataList(forceRefresh: boolean = false)', forceRefresh);
    this.displayDialog = false;
    this.isAddNew = false;
    if (forceRefresh) {
      this.LoadEntityList();
    }
  };

  applyFilterGlobal($event: any, stringVal: any) {
    this.table.filterGlobal(($event.target as HTMLInputElement).value, stringVal);
  }
}
