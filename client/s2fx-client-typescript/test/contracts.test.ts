import { S2AxiosHttpClient } from "../src/http.axios"
import { MetaEntityContract, DynamicRestEntityContract, ViewContract } from "../src/contracts"

let httpClient = new S2AxiosHttpClient('http://localhost:59129/Default')

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


describe("ViewContract test", () => {
    let contract = new ViewContract(httpClient)

    it("ViewContract can get main menu", async () => {
        let menus = await contract.getMainMenu()
        expect(menus.length).toBeGreaterThan(0)
        expect(menus[0].Id).toBeGreaterThan(0)
    })

    it('ViewContract can get user list view', async () => {
        const VIEW_NAME = 'View.Core.User.List'
        let viewInfo = await contract.singleView(VIEW_NAME)
        expect(viewInfo.View.ViewType).toEqual('ListView')
        expect(viewInfo.Name).toEqual(VIEW_NAME)
        expect(viewInfo.MetaFields.length).toBeGreaterThan(0)
    })

})


describe("DynamicEntityContract test", () => {

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
