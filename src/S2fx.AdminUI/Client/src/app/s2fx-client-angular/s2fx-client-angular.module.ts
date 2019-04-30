import { NgModule } from '@angular/core'
import { CommonModule } from '@angular/common'
import { HttpClientModule, HttpClient } from '@angular/common/http';
import { HttpModule } from '@angular/http'
import { PlatformLocation } from '@angular/common';

import { NgS2HttpClient } from './http.ng'
import { NgS2fxClient } from './s2client';
import { ListViewComponent } from './components/list-view.component';
import { DocumentComponent } from './components/document.component';

@NgModule({
    imports: [
        CommonModule,
        HttpModule,
        HttpClientModule
    ],
    declarations: [],
    exports: [
        ListViewComponent,
        DocumentComponent
    ],
    providers: [
        {
            provide: NgS2fxClient,
            useFactory: (http: HttpClient, pl: PlatformLocation) => {
                let tenant = 'Default'
                let baseUri = (pl as any).location.origin.trim('/') + `/${tenant}`
                let s2HttpClient = new NgS2HttpClient(http, baseUri)
                return new NgS2fxClient(s2HttpClient, tenant)
            },
            deps: [HttpClient, PlatformLocation]
        }
    ]
})
export class S2fxClientAngularModule { }
