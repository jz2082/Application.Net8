import { CommonModule } from '@angular/common';
import { Component, Input, OnInit } from '@angular/core';
import { Validators } from '@angular/forms';
import { PrimengModule } from '@common/primeng.module';
import { IftaLabelModule } from 'primeng/iftalabel';

import { Dictionary } from '@common/model/dictionary';
import { Product } from '@demo/model/product';
import { MessageSeverity } from '@common/model/messageSeverity';
import { ProductService } from '@demo/service/product.service';
import { Utility } from '@common/service/utility';
import { BaseComponent } from '@common/component/base-component.component';
import { ControlMessageComponent } from '@common/component/control-message.component';
import { LoadingComponent } from '@common/component/loading/loading.component';

@Component({
  selector: 'demo-product-edit',
  standalone: true,
  imports: [
    CommonModule,
    PrimengModule,
    IftaLabelModule,
    ControlMessageComponent,
    LoadingComponent
  ],
  providers: [
    ProductService
  ],
  templateUrl: './product-edit.component.html',
  styleUrl: './product-edit.component.scss'
})
export class ProductEditComponent extends BaseComponent implements OnInit {
  @Input() entity: Product;
  @Input() isNew: boolean = false;
  @Input() refreshFunc: (forceRefresh: boolean) => void;

  originalEntity = {} as Product;

  constructor(private productService: ProductService) {
    super();
    this.loggerService.logTrace('ProductEditComponent.constructor(private productService: ProductService)');
  }

  override ngOnInit(): void {
    super.ngOnInit();
    this.loggerService.logTrace('ProductEditComponent.ngOnInit()');
    this.module = 'ProductEditComponent';
    if (this.isNew) {
      this.pageTitle = 'Add Product';
      this.entity = { productId: 0, tagList: [] } as Product;
    }
    else {
      this.pageTitle = 'Edit Product';
    }
    this.isAddNew = this.isNew;
    this.entityForm = this.formBuilder.group({
      productId: [''],
      productName: ['', Validators.required],
      productCode: ['', Validators.required],
      tagList: [{}],
      releaseDate: ['', Validators.required],
      price: ['', Validators.required],
      description: [''],
      starRating: [''],
      imageUrl: [''],
      summaryMessage: new Dictionary()
    });
    this.getEntity(this.entity.productId);
  }

  getEntity(entityId: number) {
    this.loggerService.logTrace('ProductEditComponent.getEntity(entityId: number)', entityId);
    if (this.isAddNew) {
      Object.assign(this.originalEntity, this.entity);
      this.entityForm.patchValue(this.entity);
      this.edit();
    }
    else {
      this.isLoading = true;
      this.subscriptionList.push(this.productService.getEntity(entityId).subscribe({
        next: (entityResponse) => {
          this.isLoading = false;
          this.entity = entityResponse.data;
          this.loggerService.logTrace('ProductEditComponent.getEntity()', this.entity);
          this.entityForm.controls['summaryMessage'].enable();
          this.handleHttpResponseError(entityResponse.ruleViolation);
          if (this.isServerValidationPassed) {
            this.entity.releaseDate = Utility.convertDateToDate(this.entity.releaseDate);
            Object.assign(this.originalEntity, this.entity);
            this.entityForm.patchValue(this.entity);
            this.entityForm.disable();
            this.loggerService.logTrace('ProductEditComponent.getEntity() - assign', this.entity.releaseDate);
          }
        },
        error: (e) => {
          this.isLoading = false;
          this.entityForm.controls['summaryMessage'].enable();
          this.handleHttpError(e);
          this.entity = undefined;
          this.entityForm.disable();
        }
      }));
    }
  }

  edit() {
    this.isFormEdit = true;
    this.entityForm.enable();
    this.entityForm.controls["productId"].disable();
  }

  cancel() {
    this.loggerService.logTrace('ProductEditComponent.cancel()');
    if (this.entityForm.dirty && this.entityForm.touched) {
      this.toastMessage(MessageSeverity.warn, 'Your changes have been canceled.');
    }
    else {
      this.toastMessage(MessageSeverity.warn, 'No changes.');
    }
    this.refreshFunc(false);
  }

  onSubmit() {
    if (!this.entityForm.dirty) {
      this.cancel();
      return;
    }
    this.entity = { ...this.entity, ...this.entityForm.value };
    this.loggerService.logTrace('ProductEditComponent.onSubmit()', this.entityForm.invalid, this.entity);
    if (!this.entityForm.invalid) {
      this.clearDisplayMessage();
      this.saveEntity();
    }
    else {
      this.entityForm.markAllAsTouched();
    }
  }

  onDelete() {
    this.loggerService.logTrace('ProductEditComponent.onDelete()');
    this.confirmationService.confirm({
      key: "componentDialog",
      message: 'Do you want to delete this record?',
      header: 'Delete Confirmation',
      icon: 'pi pi-info-circle',
      accept: () => {
        this.deleteEntity();
      },
      reject: () => {
        this.toastMessage(MessageSeverity.info, 'Action rejected.');
      }
    });
  }

  saveEntity = () => {
    this.loggerService.logTrace('ProductEditComponent.saveEntity()');
    this.isSaving = true;
    if (this.isAddNew) {
      this.subscriptionList.push(this.productService.createEntity(this.entity).subscribe({
        next: (entityResponse) => {
          this.isSaving = false;
          this.loggerService.logTrace('ProductEditComponent.addNewEntity()', entityResponse);
          this.handleHttpResponseError(entityResponse.ruleViolation);
          if (this.isServerValidationPassed) {
            this.isFormEdit = false;
            this.entityForm.markAsUntouched();
            this.entityForm.disable();
            this.refreshFunc(true);
            this.toastMessage(MessageSeverity.success, 'Your changes have been saved.');
          }
        },
        error: (e) => {
          this.isSaving = false;
          this.entityForm.controls['summaryMessage'].enable();
          this.handleHttpError(e);
        }
      }));
    }
    else {
      this.subscriptionList.push(this.productService.updateEntity(this.entity).subscribe({
        next: (entityResponse) => {
          this.isSaving = false;
          this.loggerService.logTrace('ProductEditComponent.updateEntity()', entityResponse);
          this.handleHttpResponseError(entityResponse.ruleViolation);
          if (this.isServerValidationPassed) {
            this.isFormEdit = false;
            this.entityForm.markAsUntouched();
            this.entityForm.disable();
            this.refreshFunc(true);
            this.toastMessage(MessageSeverity.success, 'Your changes have been saved.');
          }
        },
        error: (e) => {
          this.isSaving = false;
          this.entityForm.controls['summaryMessage'].enable();
          this.handleHttpError(e);
        }
      }));
    }
  }

  deleteEntity = () => {
    this.loggerService.logTrace('ProductEditComponent.deleteEntity()');
    this.isDeleting = true;
    this.subscriptionList.push(this.productService.deleteEntity(this.entity.productId).subscribe({
      next: (entityResponse) => {
        this.isDeleting = false;
        this.loggerService.logTrace('ProductEditComponent.deleteEntity()', entityResponse);
        this.handleHttpResponseError(entityResponse.ruleViolation);
        if (this.isServerValidationPassed) {
          this.refreshFunc(true);
          this.toastMessage(MessageSeverity.success, 'Entity have been deleted.');
        }
      },
      error: (e) => {
        this.isDeleting = false;
        this.entityForm.controls['summaryMessage'].enable();
        this.handleHttpError(e);
      }
    }));
  }
}