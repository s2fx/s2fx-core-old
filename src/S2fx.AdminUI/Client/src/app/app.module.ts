import { BrowserModule } from '@angular/platform-browser'
import { CommonModule } from '@angular/common'
import { NgModule } from '@angular/core'
import { FormsModule } from '@angular/forms'
import { LocationStrategy, HashLocationStrategy } from '@angular/common'

import { PerfectScrollbarModule } from 'ngx-perfect-scrollbar'
import { PERFECT_SCROLLBAR_CONFIG } from 'ngx-perfect-scrollbar'
import { PerfectScrollbarConfigInterface } from 'ngx-perfect-scrollbar'

const DEFAULT_PERFECT_SCROLLBAR_CONFIG: PerfectScrollbarConfigInterface = {
    suppressScrollX: true
}

import { AppComponent } from './app.component'

// Import containers
import { DefaultLayoutComponent } from './containers'

import { P404Component } from './views/error/404.component'
import { P500Component } from './views/error/500.component'
import { RegisterComponent } from './views/register/register.component'
import { LoginPageComponent } from './pages/login/login.page.component'
import { SetupPageComponent } from './pages/setup/setup.page.component'

// 导入 Midway 客户端模块
import { S2fxClientModule } from './s2fx-client/s2fx-client.module'


const APP_CONTAINERS = [
    DefaultLayoutComponent
];

import {
    AppAsideModule,
    AppBreadcrumbModule,
    AppHeaderModule,
    AppFooterModule,
    AppSidebarModule,
} from '@coreui/angular'

// Import routing module
import { AppRoutingModule } from './app.routing'

// Import 3rd party components
import { BsDropdownModule } from 'ngx-bootstrap/dropdown'
import { TabsModule } from 'ngx-bootstrap/tabs'
import { ChartsModule } from 'ng2-charts/ng2-charts'

@NgModule({
    imports: [
        BrowserModule,
        CommonModule,
        AppRoutingModule,
        AppAsideModule,
        AppBreadcrumbModule.forRoot(),
        AppFooterModule,
        AppHeaderModule,
        AppSidebarModule,
        PerfectScrollbarModule,
        BsDropdownModule.forRoot(),
        TabsModule.forRoot(),
        ChartsModule,
        FormsModule,    

        S2fxClientModule,
    ],
    declarations: [
        AppComponent,
        ...APP_CONTAINERS,
        P404Component,
        P500Component,
        RegisterComponent,
        LoginPageComponent,
        SetupPageComponent,
    ],
    providers: [{
        provide: LocationStrategy,
        useClass: HashLocationStrategy
    }],
    bootstrap: [AppComponent]
})
export class AppModule { }
