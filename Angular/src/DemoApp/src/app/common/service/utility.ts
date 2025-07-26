import { HttpErrorResponse } from '@angular/common/http';
import { AbstractControl, FormArray, FormGroup } from '@angular/forms';
import { SelectItem, TreeNode } from 'primeng/api';

import { AppKeyValue } from '@common/model/appKeyValue';
import { LoggerInfo } from '../model/LoggerInfo';
import { environment } from '@env/environment';

export class Utility {

  static isNullOrUndefined(value: any): boolean {
    return value === null || typeof value === 'undefined';
  }

  static defaultDate(): Date {
    return new Date("0001-01-01");
  }

  static isDefaultDate(value: Date): boolean {
    if (value) {
      return (Utility.defaultDate().getTime() === value.getTime());
    }
    return false;
  }

  static getAngularDate(offset: number): Date {
    let currentDate = new Date();
    currentDate.setDate(currentDate.getDate() + offset);
    return Utility.convertDateToDate(currentDate);
  }

  static getAngulaOffsetrDate(value: Date, offset: number): Date {
    let currentDate = Utility.convertDateToDate(value);
    currentDate.setDate(currentDate.getDate() + offset);
    return Utility.convertDateToDate(currentDate);
  }

  static getOffsetrDateString(value: Date, offset: number): string {
    let currentDate = Utility.convertDateToDate(value);

    currentDate.setDate(currentDate.getDate() + offset);
    currentDate = new Date(new Date(currentDate).toDateString());
    return Utility.convertDateToString(currentDate);
  }

  static convertStringToDate(value: string): Date {
    let returnValue: Date = Utility.defaultDate();
    if (value) {
      returnValue = new Date(new Date(value + 'T00:00:00').toDateString());
    }
    return returnValue;
  }

  static convertDateToDate(value: Date): Date {
    let returnValue: Date = Utility.defaultDate();
    if (value) {
      returnValue = new Date(new Date(value).toDateString());
    }
    return returnValue;
  }

  static convertToTreeNodeList(xmlString: string): TreeNode[] {
    const nodeList: TreeNode[] = [];
    if (xmlString != null && xmlString.length > 0) {
      const renderedInfo = JSON.parse(xmlString);
      Object.entries(renderedInfo).forEach(
        ([key, value]) => {
          const stringValue: string = value + "";
          if (stringValue.length > 0 && stringValue != "{}") {
            switch (key) {
              case 'renderedInfo':
                nodeList.push({
                  label: key,
                  children: Utility.convertToTreeNodeList(stringValue)
                });
                break;
              case 'id':
              case 'relationId':
              case 'rowVersion':
                break;
              case 'data':
                nodeList.push({
                  label: 'data',
                  children: Utility.convertValueToTreeNodeList(value)
                });
                break;
              case 'claimList':
                nodeList.push({
                  label: key,
                  children: Utility.convertKeyValueToTreeNodeList(value as AppKeyValue[])
                });
                break;
              default:
                if (stringValue.length < 128) {
                  nodeList.push({ label: key + ': ' + value });
                }
                else {
                  nodeList.push({
                    label: key,
                    children: [{ label: stringValue }]
                  });
                }
                break;
            }
          }
        });
    }
    return nodeList;
  }

  static convertKeyValueToTreeNodeList(keyValueList: AppKeyValue[]): TreeNode[] {
    const nodeList: TreeNode[] = [];
    console.log('keyValueList', keyValueList);
    if (keyValueList != null && keyValueList.length > 0) {
      keyValueList.forEach(
        (key, value) => {
          nodeList.push({
            label: key.key,
            children: [{ label: key.value }]
          });
        });
    }
    return nodeList;
  }

  static convertValueToTreeNodeList(valueList: any): TreeNode[] {
    const nodeList: TreeNode[] = [];
    if (valueList != null) {
      Object.entries(valueList).forEach(
        ([key, value]) => {
          nodeList.push({ label: key + ': ' + value });
        });
    };
    return nodeList;
  }

  static convertDateToString(value: Date): string {
    if (value) {
      value = Utility.convertDateToDate(value);
      let day: any = value.getDate();
      let month: any = value.getMonth() + 1;
      let year: any = value.getFullYear();
      if (day < 10) {
        day = '0' + day.toString();
      }
      if (month < 10) {
        month = '0' + month.toString();
      }
      return (year.toString() + '-' + month + '-' + day);
    }
    return "0001-01-01";
  }

  static convertToSelectItemList(value: AppKeyValue[], ignoreEmpty: boolean = false): SelectItem[] {
    let entityList: SelectItem[] = [];
    if (value != null && value.length > 0) {
      for (var item of value) {
        if (ignoreEmpty && item.key == '') {
          continue;
        }
        entityList.push({ label: item.value, value: item.key });
      }
    }
    return entityList;
  }

  static getSelectItemListItemText(itemList: SelectItem[], key: string): string {
    if (itemList != null && itemList.length > 0) {
      for (var item of itemList) {
        if (item.value == key) {
          return item.label ?? '';
        }
      }
    }
    return '';
  }

  static replaceAll(stringValue: string, find: string, replace: string): string {
    var escapedFind = find.replace(/([.*+?^=!:${}()|\[\]\/\\])/g, "\\$1");
    return stringValue.replace(new RegExp(escapedFind, 'g'), replace);
  }

  static transformToLowerCase(value: string): string {
    let first = value.substr(0, 1).toLowerCase();
    return first + value.substr(1);
  }

  static findFormControl(fromGroup: FormGroup, arrayName: string, index: number, groupName: string, controlName: string): AbstractControl | null {
    if (fromGroup) {
      // check if controlName is combined name
      const nameList = controlName.split("-");
      if (nameList.length == 3) {
        const formArray = fromGroup.get(nameList[0]) as FormArray;
        const arrayIndex = parseInt(nameList[1]);
        const angularControlName = Utility.transformToLowerCase(nameList[2]);
        if (formArray && formArray.controls[arrayIndex]) {
          return formArray.controls[arrayIndex].get(angularControlName);
        }
      }
      else if (nameList.length == 2) {
        const groupControl = fromGroup.get(nameList[0]) as FormGroup;
        const angularControlName = Utility.transformToLowerCase(nameList[1]);
        if (groupControl) {
          return groupControl.controls[angularControlName];
        }
      }
      else {
        const angularControlName = Utility.transformToLowerCase(controlName);
        if (arrayName && controlName) {
          // find form array control
          const formArray = fromGroup.get(arrayName) as FormArray;
          if (formArray && formArray.controls[index]) {
            return formArray.controls[index].get(angularControlName);
          }
        }
        else if (groupName && controlName) {
          // find form group control
          const groupControl = fromGroup.get(groupName) as FormGroup;
          return groupControl.controls[angularControlName];
        }
        else if (controlName) {
          // find form control
          return fromGroup.controls[angularControlName];
        }
      }
    }
    return null;
  }

  static findFormControlName(fromGroup: FormGroup, arrayName: string, index: number, groupName: string, controlName: string): string {
    let returnValue: string = "summaryMessage";
    if (Utility.findFormControl(fromGroup, arrayName, index, groupName, controlName)) {
      const nameList = controlName.split("-");
      if (nameList.length == 3) {
        const angularControlName = Utility.transformToLowerCase(nameList[2]);
        returnValue = nameList[0] + "-" + nameList[1] + "-" + angularControlName;
      }
      else if (nameList.length == 2) {
        const angularControlName = Utility.transformToLowerCase(nameList[1]);
        returnValue = nameList[0] + "-" + angularControlName;
      }
      else {
        returnValue = Utility.transformToLowerCase(controlName);
      }
    }
    return (returnValue);
  }

  static getClientLoggerInfo(): LoggerInfo {
    const returnValue: LoggerInfo = new LoggerInfo();
    returnValue.environment = environment.appSetting.environment;
    returnValue.application = "Angular Application";
    returnValue.module = "";
    returnValue.method = "";
    returnValue.message = "";
    returnValue.level = "INFO";
    returnValue.loggerName = "Client";
    returnValue.threadName = "Client";
    returnValue.machineName = "Client";
    returnValue.isJsonMessage = false;
    returnValue.renderedType = "";
    returnValue.renderedInfo = "";
    returnValue.exceptionInfo = "";
    returnValue.exceptionType = "";
    returnValue.exceptionMessage = "";
    returnValue.dateCreated = new Date();
    returnValue.timeStamp = new Date();
    returnValue.dateModified = new Date();
    returnValue.userName = "Anonymous";
    return returnValue;
  }

  static getLoggerInfo(error: any): LoggerInfo {
    const returnValue: LoggerInfo = Utility.getClientLoggerInfo();
    returnValue.module = "client side error";
    returnValue.level = "ERROR";
    if (error) {
      if (error instanceof HttpErrorResponse) {
        returnValue.module = "HttpErrorResponse";
        if (error.error instanceof ErrorEvent) {
          // client side error
          returnValue.message = error.error.message;
          returnValue.exceptionInfo = JSON.stringify(error.error)
        }
        else {
          // api server side error
          returnValue.message = `${error.status}: ${error.message}`;
          returnValue.exceptionInfo = JSON.stringify(error)
        }
      }
      else {
        returnValue.module = "client side error";
        returnValue.message = (<Error>error).name || "Undefined client side error name";
        returnValue.exceptionMessage = (<Error>error).message || "Undefined client side error message";
        returnValue.exceptionInfo = (<Error>error).stack || "Undefined client side error stack";
      }
    }
    return returnValue;
  }
}
