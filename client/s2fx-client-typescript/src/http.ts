import axios, { AxiosRequestConfig, AxiosInstance } from 'axios'

export class HttpClient {

    private readonly axiosInstance: AxiosInstance

    constructor(readonly baseUri: string) {
        this.axiosInstance = axios.create({
            baseURL: baseUri
        })
    }

    async getAsJson<TResponseData=any>(path: string): Promise<TResponseData> //有返回值无参数
    async getAsJson<TResponseData=any>(path: string, params: {[key: string]: any}): Promise<TResponseData> //有返回值有参数
    async getAsJson<TResponseData=any>(path: string, params?: {[key: string]: any}): Promise<TResponseData> {
        let req = this.buildJsonRequest(params)
        let rep = await this.axiosInstance.get<TResponseData>(path, req)
        return rep.data
    }

    async postAsJson<TResponseData>(path: string): Promise<TResponseData> //有返回值无参数
    async postAsJson<TResponseData>(path: string, params:  {[key: string]: any}): Promise<TResponseData> //有返回值有参数
    async postAsJson<TResponseData>(path: string, params?: {[key: string]: any}): Promise<TResponseData> {
        let req = this.buildJsonRequest()
        req.data = params
        let result = await this.axiosInstance.post<TResponseData>(path, req)
        return result.data
    }

    async putAsJson<TResponseData>(path: string): Promise<TResponseData> //有返回值无参数
    async putAsJson<TResponseData>(path: string, params:  {[key: string]: any}): Promise<TResponseData> //有返回值有参数
    async putAsJson<TResponseData>(path: string, params?: {[key: string]: any}): Promise<TResponseData> {
        let req = this.buildJsonRequest()
        req.data = params
        let result = await this.axiosInstance.put<TResponseData>(path, req)
        return result.data
    }

    async delete(path: string, params: {[key: string]: any}): Promise<void>
    async delete(path: string, params?: {[key: string]: any}): Promise<void> {
        let req = this.buildJsonRequest(params)
        await this.axiosInstance.delete(path, req)
    }

    private buildJsonRequest(params?: {[key: string]: any}): AxiosRequestConfig  {
        return {
            responseType: 'json',
            params:        params,
        }
    }

}
