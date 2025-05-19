export interface IResultObject<TResponseData> {
  errors?: string[]
  data?: TResponseData
}

export interface IUserInfo {
  "jwt": string,
  "refreshToken": string,
}
export interface ILogoutInfo {
  "refreshToken": number
}

export type stringOrNull = string | null;
