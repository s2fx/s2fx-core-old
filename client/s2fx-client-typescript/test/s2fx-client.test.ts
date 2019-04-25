import { HelloService, MetaEntityService } from "../src/public-api"

describe("HelloService test", async () => {

    it("HelloService is instantiable", async () => {
        expect(new HelloService()).toBeInstanceOf(HelloService)
    })

})

describe("MetaEntityService test", async () => {

    it("MetaEntityService can get all meta entities", async () => {
        let s = new MetaEntityService()
        let metaEntities = await s.all()
        expect(metaEntities.length).toBeGreaterThan(0)
    })

    it("MetaEntityService can get single meta entity", async () => {
        let s = new MetaEntityService()
        let me = await s.single('Core.User')
        expect(me.Name).toEqual('Core.User')
        expect(me.Fields['UserName'].IsRequired).toBeTruthy()
    })

})

