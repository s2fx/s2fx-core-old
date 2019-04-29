import { Injectable, Inject } from '@angular/core'
import { HttpClient } from '@angular/common/http'
import {S2fxClient} from 's2fx-client'
import { NgS2HttpClient } from './http.ng'
import { ViewContract } from 's2fx-client'

export class NgS2fxClient extends S2fxClient {

    constructor(public readonly httpClient: NgS2HttpClient,
        public readonly tenant: string) {
        super(httpClient)
    }

}
