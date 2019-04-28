import { IS2HttpClient } from './http'
import * as contracts from './contracts'

export class S2fxClient {
    constructor(private readonly httpClient: IS2HttpClient) {
        this.viewContract = new contracts.ViewContract(this.httpClient)
    }

    readonly viewContract: contracts.ViewContract
}


