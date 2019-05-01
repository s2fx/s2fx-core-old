import { Injectable, Inject } from '@angular/core'
import { HttpClient } from '@angular/common/http'
import { S2fxClient, IS2fxClient } from '@s2fx/client'
import { NgS2HttpClient } from './http.ng'

export class NgS2fxClient extends S2fxClient implements IS2fxClient {

    constructor(
        public readonly httpClient: NgS2HttpClient,
        public readonly tenant: string) {
        super(httpClient)
    }

}
