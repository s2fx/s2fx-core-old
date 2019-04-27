import { Injectable } from '@angular/core';
import { Http, Headers, RequestOptions } from '@angular/http';
import { S2fxHttpClient } from './http-client'
import { ViewOdfDocumentInput, SettingValue, IEntityQueryResult, ISetupOptions } from './models'


@Injectable()
export class MetadataContract {

    constructor(private client: S2fxHttpClient) {

    }

    public async getAllEntities(): Promise<any[]> {
        return await this.client.getAsJson<any[]>('/Metadata/MetaEntity/All')
    }

    public async getSingleEntity(): Promise<any> {
        return await this.client.getAsJson<any>('/Metadata/MetaEntity/Single', )
    }
}


@Injectable()
export class EntityContract {

    constructor(private client: S2fxHttpClient) {

    }

    public async query(entity: string, filter?: string, select?: string, sortBy?: string): Promise<IEntityQueryResult> {
        let params = {}
        if (filter != null) {
            params['filter'] = filter
        }
        if (select != null) {
            params['select'] = select
        }
        if (sortBy != null) {
            params['sort'] = sortBy
        }
        return await this.client.getAsJson<IEntityQueryResult>(`/Entity/${entity}/Query`, params)
    }

}


@Injectable()
export class SetupContract {

    constructor(private client: S2fxHttpClient) {
    }

    public async startSetup(options: ISetupOptions): Promise<void> {
        return await this.client.postAsJson(`/System/Setup/StartSetup`, options)
    }

}
