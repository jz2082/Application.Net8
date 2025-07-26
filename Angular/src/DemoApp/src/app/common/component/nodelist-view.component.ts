import { CommonModule } from '@angular/common';
import { Component, Input, OnInit } from '@angular/core';
import { TreeNode } from 'primeng/api';
import { PrimengModule } from '@common/primeng.module';
import { Utility } from '@common/service/utility';
import { LoggerService } from '@common/service/logger.service';

@Component({
  selector: 'common-nodelist-view',
  standalone: true,
  imports: [
    CommonModule,
    PrimengModule,
  ],
  providers: [
  ],
  template: 
  `
    <div class="grid">
        <div class="field col-12 md:col-8">
            &nbsp;
        </div>
        <div class="field col-12 md:col-2">
            <button pButton type="button" label="Expand all" (click)="expandAll()" style="margin-right: .5rem"></button>
         </div>
        <div class="field col-12 md:col-2">
           <button pButton type="button" label="Collapse all" (click)="collapseAll()"></button>
        </div>
        <div class="col-12 md:col-12">
            <p-tree [value]="nodeList" selectionMode="single">
                <ng-template let-node pTemplate="default">
                    {{ node.label }} 
                </ng-template>
            </p-tree>
        </div>
    </div>
  `
})
export class NodeListViewComponent implements OnInit {
  @Input() entity: any;

  nodeList: TreeNode[] = [];

  constructor(private loggerService: LoggerService) {
    this.loggerService.logTrace('NodeListViewComponent.constructor()');
  }

  ngOnInit(): void {
    this.loggerService.logTrace('NodeListViewComponent.ngOnInit()', this.entity);
    if (this.entity != null) {
      this.nodeList = Utility.convertToTreeNodeList(JSON.stringify(this.entity));
    }
  }

  expandAll() {
    this.nodeList.forEach(
      node => {
        this.expandRecursive(node, true);
      });
  }

  collapseAll() {
    this.nodeList.forEach(
      node => {
        this.expandRecursive(node, false);
      });
  }

  private expandRecursive(node: TreeNode, isExpand: boolean) {
    node.expanded = isExpand;
    if (node.children) {
      node.children.forEach(
        childNode => {
          this.expandRecursive(childNode, isExpand);
        });
    }
  }
}