const request = require('request-promise');

export class HttpClient {

    constructor(private readonly BASE_URI: string) {

    }

    async getAsJson<TResponseData>(path: string): Promise<TResponseData>; //有返回值无参数
    async getAsJson<TResponseData>(path: string, params: object): Promise<TResponseData>; //有返回值有参数
    async getAsJson<TResponseData>(path: string, params?: object): Promise<TResponseData> {
        let req = this.buildJsonRequest(path, params)
        let result = await request.get(req)
        return result
    }

    /*
    async postAsJson<TResponseData>(path: string): Promise<TResponseData>; //有返回值无参数
    async postAsJson<TResponseData>(path: string, params: object): Promise<TResponseData>; //有返回值有参数
    async postAsJson<TResponseData>(path: string, params?: object): Promise<TResponseData> {
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
    */

    private buildJsonRequest(path: string, params?: object): object {
        /*
        let headers = {
            "Content-Type": "application/json",
            'Accept': 'application/json'
        }
        */
        return {
            baseUrl:    this.BASE_URI,
            uri:        path,
            qs:         params,
            json:       true
        }
    }


}
