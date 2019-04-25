const request = require('request-promise');

export class HttpClient {

    constructor(private readonly BASE_URI: string) {

    }

    async getAsJson<TResponseData>(path: string): Promise<TResponseData> //有返回值无参数
    async getAsJson<TResponseData>(path: string, params: {[key: string]: any}): Promise<TResponseData> //有返回值有参数
    async getAsJson<TResponseData>(path: string, params?: {[key: string]: any}): Promise<TResponseData> {
        let req = this.buildJsonRequest(path, params)
        let result = await request.get(req) as TResponseData
        return result
    }

    async postAsJson<TResponseData>(path: string): Promise<TResponseData> //有返回值无参数
    async postAsJson<TResponseData>(path: string, params:  {[key: string]: any}): Promise<TResponseData> //有返回值有参数
    async postAsJson<TResponseData>(path: string, params?: {[key: string]: any}): Promise<TResponseData> {
        let req = this.buildJsonRequest(path, params)
        req.body = params
        let result = await request.post(req) as TResponseData
        return result
    }

    async delete(path: string, params: {[key: string]: any}): Promise<void>
    async delete(path: string, params?: {[key: string]: any}): Promise<void> {
        let req = this.buildJsonRequest(path, params)
        let result = await request.delete(req)
        return result
    }

    private buildJsonRequest(path: string, params?: {[key: string]: any}): {[key: string]: any} {
        return {
            baseUrl:    this.BASE_URI,
            uri:        path,
            qs:         params,
            json:       true
        }
    }


}
