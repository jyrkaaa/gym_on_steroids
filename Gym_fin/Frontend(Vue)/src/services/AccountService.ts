import type { ILogoutInfo, IResultObject } from '@/types.ts'
import type { IUserInfo } from "@/types.ts";
import axios from "axios";
import { BaseService } from '@/services/BaseService.ts'



export default class AccountService extends BaseService {


  static async refreshAsyncJWT(jwt: string, refreshToken: string): Promise<IResultObject<IUserInfo>> {
    const loginData = {
      jwt: jwt,
      refreshToken: refreshToken,
    }
    const url = "Account/RenewRefreshToken"
    try {
      const response = await this.axios.post<IUserInfo>(url, loginData);
      if (response.status < 300) {
        return {
          data: response.data
        }
      }
      return {
        errors: [response.status.toString() + " " + response.statusText]
      }
    } catch (error: any) {
      return {
        errors: [JSON.stringify(error)]
      };
    }
  }
  static async loginAsync(email: string, pwd: string): Promise<IResultObject<IUserInfo>> {
    const loginData = {
      email: email,
      password: pwd
    }
    const url = "Account/Login"
    try {
      const response = await this.axios.post<IUserInfo>(url, loginData);
      if (response.status < 300) {
        return {
          data: response.data
        }
      }
      return {
        errors: [response.status.toString() + " " + response.statusText]
      }
    } catch (error: any) {
      return {
        errors: [JSON.stringify(error)]
      };
    }
  }
  static async logoutAsync(jwt : string, refreshToken : string): Promise<IResultObject<ILogoutInfo>> {
    const logoutData = {
      refreshToken: refreshToken,
    }
    const options = {
      headers: {
        Authorization: `Bearer ${jwt}`,
      },
    }
    const url = "Account/Logout"
    try {
      const response = await this.axios.post<ILogoutInfo>(url, logoutData, options);
      if (response.status < 300) {
        return {
          data: response.data
        }
      }
      return {
        errors: [response.status.toString() + " " + response.statusText]
      }
    } catch (error: any) {
      return {
        errors: [JSON.stringify(error)]
      };
    }
  }
  static async registerAsync(email: string, pwd: string): Promise<IResultObject<IUserInfo>> {
    const registerData = {
      email: email,
      password: pwd
    }
    const url = "Account/Register";
    try {
      const response = await this.axios.post<IUserInfo>(url, registerData);
      if (response.status < 300) {
        return {
          data: response.data
        }
      }
      return {
        errors: [response.status.toString() + " " + response.statusText]
      }
    } catch (error: any) {
      return {
        errors: [JSON.stringify(error)]
      };
    }
  }

}
