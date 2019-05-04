import { S2fxServer } from "./s2fx-server";

/*
import { S2fxServer } from './s2fx-server'

let server = new S2fxServer()

//jest.setTimeout(30000)

beforeAll(async () => {
    await server.prepare()
    await server.start()
});

afterAll(async () => {
    await server.stop()
    await server.clean()
})

*/
module.exports = async function() {
    console.log("Shuting down server...");
    let server = (global as any).S2FX_SERVER as S2fxServer;
    await server.close();
};
