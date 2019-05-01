import { Component, OnDestroy, Inject, OnInit } from '@angular/core';
import { DOCUMENT } from '@angular/common';

import { NgxSpinnerService } from 'ngx-spinner';

import * as utils from '../../utils'
import { NgS2fxClient, model } from '@s2fx/client-angular'
import { BusyIndicatedExecutor } from '../../services/busy-indicated-executor'

interface NavAttributes {
    [propName: string]: any;
}

interface NavWrapper {
    attributes: NavAttributes;
    element: string;
}

interface NavBadge {
    text: string;
    variant: string;
}

interface NavLabel {
    class?: string;
    variant: string;
}

export interface NavData {
    name?: string;
    url?: string;
    icon?: string;
    badge?: NavBadge;
    title?: boolean;
    children?: NavData[];
    variant?: string;
    attributes?: NavAttributes;
    divider?: boolean;
    class?: string;
    label?: NavLabel;
    wrapper?: NavWrapper;
    queryParams?: any
}

@Component({
    templateUrl: './main-frame.component.html'
})
export class MainFrameComponent implements OnDestroy, OnInit {
    private changes: MutationObserver
    navItems: NavData[] = []
    sidebarMinimized = true
    element: HTMLElement
    navMenu: model.MenuItem[]
    isBusy = false
    busyIndicatorText: string

    constructor(private readonly busyIndicator: BusyIndicatedExecutor, private readonly client: NgS2fxClient, @Inject(DOCUMENT) _document?: any) {

        this.changes = new MutationObserver((mutations) => {
            this.sidebarMinimized = _document.body.classList.contains('sidebar-minimized');
        });
        this.element = _document.body;
        this.changes.observe(<Element>this.element, {
            attributes: true,
            attributeFilter: ['class']
        });
    }

    async ngOnInit(): Promise<void> {
        await this.busyIndicator.executeAppBlockedTask(async context => {
            await this.loadMenu()
        });
    }

    async ngOnDestroy(): Promise<void> {
        this.changes.disconnect();
    }

    private async loadMenu(): Promise<void> {
        let self = this
        this.client.httpClient
        this.navMenu = await (this.client as any).metadataContract.getMainMenu() as model.MenuItem[]
        let newNavItems = []
        for (let nm of this.navMenu) {
            let ni = self.navMenuToNavItem(nm, true)
            newNavItems.push(ni)
        }
        self.navItems = newNavItems
        await utils.wait(3000) //TODO
    }

    private navMenuToNavItem(navMenu: model.MenuItem, isTopLevel: boolean): NavData {
        let self = this;
        let navData: NavData = {
            //name:           navMenu.Text,
            //url:            navMenu.ActionId != null && navMenu.ActionId > 0 ? 'workspace' : null,       // `workspace?action=${navMenu.ActionId}` : null,
            //queryParams:    navMenu.ActionId != null? { action: 1 } : {}
            icon: navMenu.Icon,
        }

        // dropdown
        if(navMenu.Children && navMenu.Children.length > 0) {
            let children = (navMenu.Children as model.MenuItem[]).map<NavData>(x => self.navMenuToNavItem(x, false))
            navData.children = children
            navData.name = navMenu.Text
            return navData
        }

        // link
        navData.name = navMenu.Text
        navData.url = '/workspace'
        if(navMenu.ActionId != null) {
            navData.queryParams = {
                action: navMenu.ActionId
            }
        }
        return navData
    }

}
