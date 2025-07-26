import { AppKeyValue } from "./appKeyValue";

export class ApiUserInfo {
  userId: string | undefined;
  userName: string | undefined;
  userPrincipalName: string | undefined;
  userType: string | undefined;
  isAuthenticated: Boolean = false;
  lastName: string | undefined;
  firstName: string | undefined;
  displayName: string | undefined;
  title: string | undefined;
  birthday: string | undefined;
  email: string | undefined;
  alternateEmail: string | undefined;
  phone: string | undefined;
  alternatePhone: string | undefined;
  streetAddress: string | undefined;
  city: string | undefined;
  province: string | undefined;
  country: string | undefined;
  postalCode: string | undefined;
  userEnabled: Boolean | undefined;
  claimList: AppKeyValue[];
  groupList: string[];
  roleList: string[];

  constructor() {
    this.claimList = [];
    this.groupList = [];
    this.roleList = [];
  }
}
