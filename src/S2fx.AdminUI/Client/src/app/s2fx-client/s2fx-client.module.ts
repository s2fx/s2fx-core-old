import { NgModule } from '@angular/core'
import { CommonModule } from '@angular/common'
import { HttpModule } from '@angular/http'

import { S2fxHttpClient } from './http-client'
import * as services from './services'
import * as contracts from './contracts'

@NgModule({
    imports: [
        CommonModule,
        HttpModule,
     ],
    declarations: [], 
    exports: [
    ],
    providers: [S2fxHttpClient, services.S2fxClient, contracts.MetadataContract, contracts.EntityContract, contracts.SetupContract]
})
export class S2fxClientModule { }
