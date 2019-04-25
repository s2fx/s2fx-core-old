var request = require('request-promise');


export class HelloService {
    constructor() {

    }

    async get(): Promise<any> {
        let req = {
            uri: 'https://jsonplaceholder.typicode.com/todos/1',
            method: 'GET',
            json: true,
        }
        let x = await request(req)
        return x
    }
}

export class MetaEntityService {
    constructor(private host = 'http://localhost:59129/Default') {
    }

    async all(): Promise<any[]> {
        let req = {
            uri:    this.host + '/Metadata/MetaEntity/All',
            json:   true,
        }
        let mes = await request.get(req)
        return mes
    }

    async single(name: string): Promise<any> {
        let qs = {
            name: name
        }
        let req = {
            uri:    this.host + '/Metadata/MetaEntity/Single',
            json:   true,
            qs:     qs,
        }
        let me = await request.get(req)
        return me
    }

}
