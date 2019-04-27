import {HttpClient} from './http'
import * as contracts from './contracts'

export class S2fxClient {
    private httpClient: HttpClient

    constructor(baseUri: string) {
        this.httpClient = new HttpClient(baseUri)
        this.viewContract = new contracts.ViewContract(this.httpClient)
    }

    readonly viewContract: contracts.ViewContract
}


