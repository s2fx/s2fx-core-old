import { Injectable } from '@angular/core';
import { Http, Headers, RequestOptions } from '@angular/http';
import { S2fxHttpClient } from './http-client'
import { ViewOdfDocumentInput, SettingValue, IEntityQueryResult } from './models'


@Injectable({
  providedIn: 'root'
})
export class S2fxClient {

    constructor(
        private client: S2fxHttpClient) {
    }

    /// 登录
    public async login(login: string, password: string) {
        throw new Error("not implemented");
    }

    public async logout(isForced: boolean = false) {
        throw new Error("not implemented");
    }


}


