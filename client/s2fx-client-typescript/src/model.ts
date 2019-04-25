
export interface IMetaEntity {
    Name:           string
    DisplayName:    string
    Action:         string
    Dependencies:   string[]
    Feature:        string
    Fields:         Record<string, IMetaField>
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
