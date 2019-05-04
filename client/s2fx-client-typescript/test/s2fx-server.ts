const Util = require('util');
const Fs = require('fs')
const Path = require('path')
const ChildProcess = require('child_process')
const Rimraf = require('rimraf')
const { Client } = require('pg')
const axios = require('axios')

const exec = Util.promisify(ChildProcess.exec);
const sleep = Util.promisify(setTimeout)

const PROJECT_DIRECTORY = Path.resolve('../../samples/S2fx.Demo.Web')
const PROJECT_PATH = Path.resolve(PROJECT_DIRECTORY + '/S2fx.Demo.Web.csproj')
const APP_DATA_DIRECTORY = Path.resolve(PROJECT_DIRECTORY + '/App_Data')
const APP_DATA_SITES_DIRECTORY = Path.resolve(PROJECT_DIRECTORY + '/App_Data/Sites')

const REQUEST_PERIOD = 5000

const TENANTS = {
    "Default": {
        "State": "Running",
        //"RequestUrlHost": "",
        "RequestUrlPrefix": "Default",
        "Features": [ "Module1", "Module2", "OrchardCore.Mvc" ]
    },
    "Tenant1": {
        "State": "Running",
        "TablePrefix": "",
        // "RequestUrlHost": "acme.com, acme.org",
        "RequestUrlPrefix": "tenant1",
        "Features": [ "Module1", "Module2", "OrchardCore.Mvc" ]
    }
}

const DEFAULT_TENANT_SETTINGS = {
    "ConnectionString": "Host=localhost;Database=s2fx_test;Username=s2fx;Password=s2fx"
}

export class S2fxServer {
    private server: any = null
    private readonly axiosInstance = axios.create({
        baseURL: 'http://localhost:59129/Default',
        timeout: REQUEST_PERIOD
    })
    private readonly longTimeoutAxiosInstance = axios.create({
        baseURL: 'http://localhost:59129/Default',
        timeout: 30000
    })

    constructor() {
    }

    async prepare(): Promise<void> {
        await this.dropDatabase()
        console.log('Creating database...')
        await this.executeSql('CREATE DATABASE s2fx_test OWNER s2fx ENCODING UTF8')

        const write = Util.promisify(Fs.writeFile)

        if (!Fs.existsSync(APP_DATA_DIRECTORY)) {
            Fs.mkdirSync(APP_DATA_DIRECTORY);
        }

        if (!Fs.existsSync(APP_DATA_SITES_DIRECTORY)) {
            Fs.mkdirSync(APP_DATA_SITES_DIRECTORY);
        }

        await write(APP_DATA_DIRECTORY + '/tenants.json', JSON.stringify(TENANTS))

        if (!Fs.existsSync(APP_DATA_SITES_DIRECTORY + '/Default')) {
            Fs.mkdirSync(APP_DATA_SITES_DIRECTORY + '/Default')
        }
        if (!Fs.existsSync(APP_DATA_SITES_DIRECTORY + '/Tenant2')) {
            Fs.mkdirSync(APP_DATA_SITES_DIRECTORY + '/Tenant2')
        }

        await write(APP_DATA_SITES_DIRECTORY + '/Default/appsettings.json', JSON.stringify(DEFAULT_TENANT_SETTINGS))
    }

    async start(): Promise<void> {
        console.log('Starting S2fx server...')
        console.log(PROJECT_PATH)
        this.server = ChildProcess.spawn('dotnet', ['run', '-c', 'Debug', '-p', PROJECT_PATH], { cwd: PROJECT_DIRECTORY })
        await this.ensureTenantStarted()
        console.log('S2fx server has been started.')
    }

    async initTenant() {
        console.log('Initializing tenant...')
        await this.longTimeoutAxiosInstance.get('/System/Setup/InitDb')
        await this.longTimeoutAxiosInstance.get('/System/Setup/LoadSeeds')
        await sleep(3000)
    }

    async stop(): Promise<void> {
        let self = this
        if (self.server) {
            self.server.kill('SIGINT')
            while(!self.server.killed) {
                await sleep(1000)
            }
            self.server = null
        }
    }

    async clean(): Promise<void> {
        await this.dropDatabase()
    }

    async close() {
        await this.stop()
        console.log('S2fx server has been stopped.')
        await this.clean()
        console.log('All done.')
    }

    private async dropDatabase() {
        await this.executeSql('DROP DATABASE IF EXISTS s2fx_test;')
    }

    private async executeSql(sql: string) {
        let pg = new Client({
            user: 's2fx',
            password: 's2fx',
            host: 'localhost',
            database: 'postgres',
            port: 5432,
        })
        await pg.connect()
        try {
            await pg.query(sql)
        }
        finally {
            await pg.end()
        }
    }

    private async ensureTenantStarted() {
        while(true) {
            if(await this.tryRequestServer()) {
                break
            }
            else {
                await sleep(1000)
            }
        }
    }

    private async tryRequestServer(): Promise<boolean> {
        try {
            await this.axiosInstance.get('/System/Setup/Version')
            return true
        }
        catch {
            return false
        }
    }

}
