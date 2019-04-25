import {HttpClient} from './http'
import * as model from './model'

export abstract class AbstractContract {

    constructor(protected readonly httpClient: HttpClient) {

    }
}

export class MetaEntityContract extends AbstractContract {

    async all(): Promise<model.IMetaEntity[]> {
        return await this.httpClient.getAsJson('/Metadata/MetaEntity/All')
    }

    async single(name: string): Promise<model.IMetaEntity> {
        return await this.httpClient.getAsJson('/Metadata/MetaEntity/Single', {name: name})
    }

}
