import { NgModule } from '@angular/core'
import { CommonModule } from '@angular/common'
import { HttpClientModule } from '@angular/common/http';
import { HttpModule } from '@angular/http'

import { NgS2HttpClient } from './http.ng'
import { NgS2fxClient } from './s2client';

@NgModule({
    imports: [
        CommonModule,
        HttpModule,
        HttpClientModule
     ],
    declarations: [],
    exports: [
    ],
    providers: [NgS2HttpClient, NgS2fxClient]
})
export class S2fxClientAngularModule { }
