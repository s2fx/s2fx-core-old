// Angular
import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms'

import { LoginPageComponent } from './login/login.page.component';
import { SetupPageComponent } from './setup/setup.page.component';

// Theme Routing
//import { ThemeRoutingModule } from './theme-routing.module';

@NgModule({
    imports: [
        CommonModule,
        FormsModule,
    ],
    declarations: [
        LoginPageComponent,
        SetupPageComponent
    ]
})
export class PagesModule { }
