import { Injectable, Inject } from '@angular/core'
import { Http, Headers, RequestOptions } from '@angular/http'
import { Observable } from 'rxjs';

@Injectable()
export class S2fxHttpClient {

    private readonly Server = location.origin

    constructor(private http: Http) {

    }

    public async getAsJson<TResponseData>(path: string): Promise<TResponseData>; //有返回值无参数
    public async getAsJson<TResponseData>(path: string, params: object): Promise<TResponseData>; //有返回值有参数
    public async getAsJson<TResponseData>(path: string, params?: object): Promise<TResponseData> {
        let options = this.buildRequestOptions(params)
        let url = path
        var result = null
        await this.http.get(url, options).forEach(response => {
            let resultText = response.text()
            if (resultText != null && resultText.length > 0) {
                result = JSON.parse(response.text()) as TResponseData
            }
        })
        return result
    }

    public async postAsJson<TResponseData>(path: string): Promise<TResponseData>; //有返回值无参数
    public async postAsJson<TResponseData>(path: string, params: object): Promise<TResponseData>; //有返回值有参数
    public async postAsJson<TResponseData>(path: string, params?: object): Promise<TResponseData> {
        let jsonBody = params != null ? JSON.stringify(params) : null
        let options = this.buildRequestOptions(params)
        let url = path
        var result = null
        await this.http.post(url, jsonBody, options).forEach(response => {
            let resultText = response.text()
            if (resultText != null && resultText.length > 0) {
                result = JSON.parse(response.text()) as TResponseData
            }
        })
        return result
    }

    private buildRequestOptions(params?: object): RequestOptions {
        let headers = new Headers({
            "Content-Type": "application/json",
            'Accept': 'application/json'
        });
        return new RequestOptions({
            headers: headers,
            params: params
        });
    }

}
