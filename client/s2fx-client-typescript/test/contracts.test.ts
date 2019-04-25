import { HttpClient } from "../src/http"
import { MetaEntityContract } from "../src/contracts"

describe("MetaEntityContrct test", () => {
    let httpClient = new HttpClient('http://localhost:59129/Default')

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

