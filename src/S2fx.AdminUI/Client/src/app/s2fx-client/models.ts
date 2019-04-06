

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