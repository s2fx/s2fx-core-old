import { HttpClient } from "../src/public-api"

describe("HelloService test", () => {

    it("HelloService is instantiable", () => {
        expect(new HttpClient('http://localhost:59129')).toBeInstanceOf(HttpClient)
    })

})

