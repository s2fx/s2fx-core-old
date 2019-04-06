import { NgModule } from '@angular/core'
import { CommonModule } from '@angular/common'
import { HttpModule } from '@angular/http'

import * as services from './services'
import { S2fxHttpClient } from './http-client'

@NgModule({
    imports: [
        CommonModule,
        HttpModule,
     ],
    declarations: [], 
    exports: [
    ],
    providers: [S2fxHttpClient, services.MetadataService, services.EntityService ]
})
export class S2fxClientModule { }
