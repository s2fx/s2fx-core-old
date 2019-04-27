
export interface VisualElement {
    Name:           string
    IsVisible?:     boolean
}


export interface ViewDefinition {
    Name:           string
    Feature:        string
}



export interface EntityViewDefinition extends VisualElement, ViewDefinition {
    ViewType:       string
    Title:          string
    Entity:         string
    Priority:       number
    Toolbar:        object
}

export interface EntityListViewDefinitionColumn {

}

export interface Field extends VisualElement, EntityListViewDefinitionColumn {
    Name:           string
}

export interface ListView extends EntityViewDefinition {
    Columns:        EntityListViewDefinitionColumn[]
}
