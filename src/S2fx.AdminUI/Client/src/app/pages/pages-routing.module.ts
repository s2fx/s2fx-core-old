import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { LoginPageComponent } from './login/login.page.component';
import { SetupPageComponent } from './setup/setup.page.component';

const routes: Routes = [
    {
        path: '',
        data: {
            title: 'Pages'
        },
        children: [
            {
                path: '',
                redirectTo: 'login'
            },
            {
                path: 'login',
                component: LoginPageComponent,
                data: {
                    title: 'Login'
                }
            },
            {
                path: 'setup',
                component: SetupPageComponent,
                data: {
                    title: 'Setup'
                }
            }
        ]
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class PagesRoutingModule { }
