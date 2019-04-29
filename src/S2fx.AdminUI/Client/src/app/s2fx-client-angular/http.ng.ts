import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { IS2HttpClient } from 's2fx-client'


export class NgS2HttpClient implements IS2HttpClient {

    constructor(private readonly http: HttpClient, public readonly baseUri: string) {
    }

    async getAsJson<TResponseData = any>(path: string): Promise<TResponseData> //有返回值无参数
    async getAsJson<TResponseData = any>(path: string, params: { [key: string]: any }): Promise<TResponseData> //有返回值有参数
    async getAsJson<TResponseData = any>(path: string, params?: { [key: string]: any }): Promise<TResponseData> {
        let url = this.baseUri + path
        var result = null
        await this.http.get<TResponseData>(url).forEach(response => {
            result = response
        })
        return result
    }

    async postAsJson<TResponseData>(path: string): Promise<TResponseData> //有返回值无参数
    async postAsJson<TResponseData>(path: string, params: { [key: string]: any }): Promise<TResponseData> //有返回值有参数
    async postAsJson<TResponseData>(path: string, params?: { [key: string]: any }): Promise<TResponseData> {
        let url = this.baseUri + path
        var result = null
        await this.http.post<TResponseData>(url, params).forEach(response => {
            result = response
        })
        return result
    }

    async putAsJson<TResponseData>(path: string): Promise<TResponseData> //有返回值无参数
    async putAsJson<TResponseData>(path: string, params: { [key: string]: any }): Promise<TResponseData> //有返回值有参数
    async putAsJson<TResponseData>(path: string, params?: { [key: string]: any }): Promise<TResponseData> {
        let url = this.baseUri + path
        var result = null
        await this.http.put<TResponseData>(url, params).forEach(response => {
            result = response
        })
        return result
    }

    async delete(path: string, params: { [key: string]: any }): Promise<void>
    async delete(path: string, params?: { [key: string]: any }): Promise<void> {
        let url = this.baseUri + path
        var result = null
        await this.http.delete(url, params).forEach(response => {
            result = response
        })
        return result
    }


}
