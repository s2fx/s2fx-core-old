import { HttpClient } from "../src/http"
import { MetaEntityContract, DynamicRestEntityContract } from "../src/contracts"

let httpClient = new HttpClient('http://localhost:59129/Default')

describe("MetaEntityContrct test", () => {

    it("MetaEntityContract can get all meta entities", async () => {
        let contract = new MetaEntityContract(httpClient)
        let metaEntities = await contract.all()
        expect(metaEntities.length).toBeGreaterThan(0)
    })

    it("MetaEntityContract can get single meta entitity", async () => {
        let contract = new MetaEntityContract(httpClient)
        let me = await contract.single('Core.User')
        expect(me.Name).toEqual('Core.User')
    })

})


describe("DynamicEntityContract test", async () => {

    it("DynamicEnttiyContract can query", async () => {
        let contract = new DynamicRestEntityContract(httpClient)
        let queryParam = {
            filter: 'it.UserName == "admin"',
            select: 'new(Id,UserName,Email)'
        }
        let queryResult = await contract.query('Core.User', queryParam)
        expect(queryResult.Total).toEqual(1)
        expect(queryResult.Count).toEqual(1)
        expect(queryResult.Offset).toEqual(0)
        expect(queryResult.Entity).toEqual('Core.User')
        expect(queryResult.Entities).toBeDefined()
        expect(queryResult.Entities.length).toEqual(1)
    })

    /*
    it("DynamicEntityContract can get single", async() => {
        let contract = new DynamicRestEntityContract(httpClient)
        let record = await contract.single('Core.User', 1)
        expect(record.Id).toEqual(1)
        expect(record.UserName).toEqual('admin')
    })
    */
})
