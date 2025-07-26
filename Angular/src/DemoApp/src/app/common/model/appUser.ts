import { AppClaim } from "./appClaim";

export class AppUser {
  userId: string | undefined;
  isAdmin: boolean | undefined;
  email: string | undefined;
  userName: string | undefined;
  familyName: string | undefined;
  givenName: string | undefined;
  idToken: string | undefined;
  accessToken: string | undefined;
  refreshToken: string | undefined;
  claimList: AppClaim[];

  constructor() {
    this.claimList = [];
  }
}
