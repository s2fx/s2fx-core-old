
export interface IMetaEntity {
    Name:           string
    DisplayName:    string
    Action:         string
    Dependencies:   string[]
    Feature:        string
    Fields:         { [prop: string]: IMetaField }
}

export interface IMetaField {
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
