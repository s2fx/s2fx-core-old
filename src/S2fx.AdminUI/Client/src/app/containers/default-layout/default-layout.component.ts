import { Component, OnDestroy, Inject, OnInit } from '@angular/core';

import { DOCUMENT } from '@angular/common';
import { NgS2fxClient } from '../../s2fx-client-angular/s2client'
import { MenuItem, ViewContract } from "s2fx-client"
//import { navItems } from '../../_nav';

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
    selector: 'app-dashboard',
    templateUrl: './default-layout.component.html'
})
export class DefaultLayoutComponent implements OnDestroy, OnInit {
    public navItems: NavData[] = []
    public sidebarMinimized = true
    private changes: MutationObserver
    public element: HTMLElement
    public navMenu: any[]
    constructor(private readonly client: NgS2fxClient, @Inject(DOCUMENT) _document?: any) {

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
        await this.loadMenu()
    }

    ngOnDestroy(): void {
        this.changes.disconnect();
    }

    private async loadMenu(): Promise<void> {
        let self = this
        let viewContract = new ViewContract(this.client.httpClient)
        this.navMenu = await viewContract.getMainMenu() as any[]
        let newNavItems = []
        for (let nm of this.navMenu) {
            let ni = self.navMenuToNavItem(nm, true)
            newNavItems.push(ni)
        }
        self.navItems = newNavItems
    }

    private navMenuToNavItem(navMenu: any, isTopLevel: boolean): NavData {
        let self = this;
        let navData: NavData = {
            //name:           navMenu.Text,
            //url:            navMenu.ActionId != null && navMenu.ActionId > 0 ? 'workspace' : null,       // `workspace?action=${navMenu.ActionId}` : null,
            //queryParams:    navMenu.ActionId != null? { action: 1 } : {}
            icon: navMenu.Icon,
        }

        // dropdown
        if(navMenu.Children && navMenu.Children.length > 0) {
            let children = (navMenu.Children as any[]).map<NavData>(x => self.navMenuToNavItem(x, false))
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
