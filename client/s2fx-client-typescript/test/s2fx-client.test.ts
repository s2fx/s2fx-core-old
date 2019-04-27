import { S2fxClient } from "../src/public-api"

const BASE_URI = 'http://localhost:59129/Default'

describe("S2fxClient test", () => {

    it("S2fxClient can be created", async () => {
        expect(new S2fxClient(BASE_URI)).toBeInstanceOf(S2fxClient)
    })

})
