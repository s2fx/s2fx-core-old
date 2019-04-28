import { S2fxClient } from "../src/public-api"
import { S2AxiosHttpClient } from "../src/http.axios";

let httpClient = new S2AxiosHttpClient('http://localhost:59129/Default')

describe("S2fxClient test", () => {

    it("S2fxClient can be created", async () => {
        expect(new S2fxClient(httpClient)).toBeInstanceOf(S2fxClient)
    })

    it("S2fxClient can access its property", async () => {
        let client = new S2fxClient(httpClient)
        let menus = await client.viewContract.getMainMenu()
        expect(menus.length).toBeGreaterThan(0)
    })

})
