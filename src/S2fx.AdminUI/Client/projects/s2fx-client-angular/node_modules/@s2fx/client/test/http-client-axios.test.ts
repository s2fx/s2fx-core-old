import { S2AxiosHttpClient } from "../src/http.axios"

describe("S2AxiosHttpClient test", () => {

    it("S2AxiosHttpClient is instantiable", () => {
        expect(new S2AxiosHttpClient('http://localhost:59129')).toBeInstanceOf(S2AxiosHttpClient)
    })

})

