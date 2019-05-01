import { Injectable, Inject } from '@angular/core';
import { Component, ElementRef, OnInit, AfterViewInit, OnDestroy, Input, ViewChild} from '@angular/core';
import { NgForm, FormGroup } from '@angular/forms';
import { NgClass, NgStyle } from '@angular/common';
import { Router, NavigationEnd, ActivatedRoute } from '@angular/router';
import { getStyle, hexToRgba } from '@coreui/coreui/dist/js/coreui-utilities';
import { CustomTooltips } from '@coreui/coreui-plugin-chartjs-custom-tooltips';
import { NgxSpinnerService } from 'ngx-spinner';
import {TabsetComponent} from 'ngx-bootstrap'

import { NgS2fxClient  } from '../../s2fx-client-angular/s2client'
import * as utils from '../../utils'
import { combineLatest, Subscription } from 'rxjs';
import { map } from 'rxjs/operators';

@Component({
    selector:       'workspace',
    templateUrl:    'workspace.component.html',
    styles:         ['.workspace-wrapper { height: 100%; }']
})
export class WorkspaceComponent implements OnInit, OnDestroy {
    readonly documents: any[] = []

    isBusy = false
    busyIndicatorText: string
    queryParamsSubscription: Subscription

    constructor(private spinner: NgxSpinnerService, private route: ActivatedRoute) {
    }

    async ngOnInit() {
        this.queryParamsSubscription = this.route.queryParams.subscribe(async queryParams => {
            let actionId = queryParams.action as number
            await this.actionRequest(actionId)
        });
    }

    ngOnDestroy(): void {
        this.queryParamsSubscription.unsubscribe();
    }

    async addDocument(doc: any): Promise<void> {
        this.documents.push(doc)
    }

    async switchDocument(doc: any): Promise<void> {
        doc.active = true
    }

    private async actionRequest(actionId: number): Promise<void> {
        let docs = this.documents.filter(d => d.actionId === actionId)
        if(docs.length == 1) {
            this.switchDocument(docs[0])
        }
        else {
            let doc = {
                active:     true,
                actionId:   actionId,
                title:      `ActionID=${actionId}`
            }
            await this.addDocument(doc)
        }
    }

    trackByDocumentId(index, doc) {
        return doc.actionId;
    }

    private async runLongTimeTask(action: () => Promise<void>): Promise<void> {
        this.isBusy = true
        try {
            await action()
        }
        finally {
            this.isBusy = false
        }

    }
}
