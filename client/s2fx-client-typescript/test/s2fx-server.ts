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

const REQUEST_PERIOD = 5000

export class S2fxServer {
    private server: any = null
    private readonly axiosInstance = axios.create({
        baseURL: 'http://localhost:59129/Default',
        timeout: REQUEST_PERIOD
    })

    constructor() {
    }

    async prepare(): Promise<void> {
        await this.dropDatabase()
        await this.executeSql('CREATE DATABASE s2fx_test OWNER s2fx ENCODING UTF8')
    }

    async start(): Promise<void> {
        console.log('Starting S2fx server...')
        console.log(PROJECT_PATH)
        this.server = ChildProcess.spawn('dotnet', ['run', '-c', 'Debug', '-p', PROJECT_PATH], { cwd: PROJECT_DIRECTORY })
        await this.ensureTenantStarted()
        console.log('S2fx server has been started.')
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
            await this.axiosInstance.get('/Metadata/MetaEntity/Single?name=Core.User')
            return true
        }
        catch {
            return false
        }
    }

}
