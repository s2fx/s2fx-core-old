

export class ViewOdfDocumentInput {
    public content: string
    constructor(content: string) {
        this.content = content
    }
}

export class SettingValue {

    constructor(public name: string, public value: string) {

    }

}

export interface IEntityQueryResult {
    Entity: string
    Filter: string
    Select: string
    SortBy: string
    Offset: number
    Limit: number
    Count: number
    Total: number
    Entities: any[]
}


export interface ISetupOptions {
    AdminPassword: string
    DbName: string
    IsDemo: boolean
    EnabledFeatures: string[]
}
