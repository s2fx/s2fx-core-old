import { IS2HttpClient } from './http'
import * as contracts from './contracts'

export interface IS2fxClient {
    readonly metadataContract:      contracts.IMetadataContract
    readonly entityContract:        contracts.IDynamicRestEntityContract
}

export class S2fxClient implements IS2fxClient {
    public readonly metadataContract:      contracts.IMetadataContract
    public readonly entityContract:        contracts.IDynamicRestEntityContract

    constructor(private readonly httpClient: IS2HttpClient) {
        this.metadataContract = new contracts.MetadataContract(this.httpClient)
        this.entityContract = new contracts.DynamicRestEntityContract(this.httpClient)
    }

}


