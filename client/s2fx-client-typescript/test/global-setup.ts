import { S2fxServer } from './s2fx-server'

/*
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

module.exports = async () => {
    let server = new S2fxServer();
    await server.prepare();
    await server.start();
    await server.initTenant();
    (global as any).S2FX_SERVER = server;
};
