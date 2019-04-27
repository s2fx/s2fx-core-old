import * as viewSchemas from './view.schemas'

export interface MetaEntity {
    Name:           string
    DisplayName:    string
    Action:         string
    Dependencies:   string[]
    Feature:        string
    Fields:         { [prop: string]: MetaField }
}

export interface MetaField {
    IsRequired?:    boolean
    Name:           string
    DisplayName:    string
    IsReadOnly:     boolean
    IsUnique?:      boolean
    IsLazy:         boolean
    IsSelect:       boolean
    Type:           string
    DbName:         string
}

/*
{
    "Entity":"Core.User",
    "Filter":null,
    "Select":"new(Id)",
    "SortBy":null,
    "Offset":0,
    "Limit":50,
    "Count":1,
    "Total":1,
    "Entities":[{"Id":1}]
}
*/

export interface MenuItem {
    Id:             number
    Name:           string
    Text:           string
    Order:          number
    ParentId?:      number
    Children?:      MenuItem[]
    Icon?:          string
    BackgroundColor:string
    ActionId?:      string
    ActionName?:    string
}

/**
 * View information for entity
 */
export interface ViewInfo {
    Name:           string
    View:           viewSchemas.EntityViewDefinition
    MetaFields:     MetaField[]
}


export interface EntityQueryParameter {
    filter?:        string
    select?:        string
    sort?:          string
    offset?:        number
    limit?:         number
}

export interface EntityQueryResult {
    Entity:         string
    Filter:         string
    Select:         string
    SortBy:         string
    Offset:         number
    Limit:          number
    Count:          number
    Total:          number
    Entities:       any[]
}
