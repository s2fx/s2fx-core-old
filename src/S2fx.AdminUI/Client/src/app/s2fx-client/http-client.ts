import { Injectable } from '@angular/core'
import { Http, Headers, RequestOptions } from '@angular/http'
import { Observable } from 'rxjs';

@Injectable()
export class S2fxHttpClient {

    private readonly Server = location.origin

    constructor(private http: Http) {

    }

    //获取
    /*
    public async callPostMethod<TArgs, TReturn>(path: string, args?: TArgs): Promise<TReturn> {
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });
        let url = this.Server + path
        let argsJson = args != null ? JSON.stringify(args) : null
        let response = await this.http.post(url, argsJson, options).toPromise()
        let resultText = response.text()
        if(resultText != null && resultText.length > 0) {
            return JSON.parse(response.text()) as TReturn
        }
        else {
            return null
        }
    }
    */

    //获取
    /*
    public async getAsJson<TReturn>(path: string): Promise<TReturn> {
        let headers = new Headers({ 'Accept': 'application/json' });
        let options = new RequestOptions({ headers: headers });
        let url = path
        var result = null
        await this.http.get(url, options).forEach(response => {
            let resultText = response.text()
            if (resultText != null && resultText.length > 0) {
                result = JSON.parse(response.text()) as TReturn
            }
        })
        return result
    }
    */

    public async getAsJson<TReturn>(path: string): Promise<TReturn>; //有返回值无参数
    public async getAsJson<TParams, TReturn>(path: string, params: TParams): Promise<TReturn>; //有返回值有参数
    public async getAsJson<TParams, TReturn>(path: string, params?: TParams): Promise<TReturn> {
        let headers = new Headers({ 'Accept': 'application/json' });
        let options = new RequestOptions({
            headers: headers,
            params: params
        });
        let url = path
        var result = null
        await this.http.get(url, options).forEach(response => {
            let resultText = response.text()
            if (resultText != null && resultText.length > 0) {
                result = JSON.parse(response.text()) as TReturn
            }
        })
        return result
    }
}
