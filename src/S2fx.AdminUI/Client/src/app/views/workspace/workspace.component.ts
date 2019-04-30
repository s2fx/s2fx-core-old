import { Injectable, Inject } from '@angular/core';
import { Component, ElementRef, OnInit, Input, ViewChild } from '@angular/core';
import { NgForm, FormGroup } from '@angular/forms';
import { NgClass, NgStyle } from '@angular/common';
import { Router, NavigationEnd, ActivatedRoute } from '@angular/router';
import { getStyle, hexToRgba } from '@coreui/coreui/dist/js/coreui-utilities';
import { CustomTooltips } from '@coreui/coreui-plugin-chartjs-custom-tooltips';
import { NgS2fxClient  } from '../../s2fx-client-angular/s2client'
import {TabsetComponent} from 'ngx-bootstrap'


@Component({
    selector:       'workspace',
    templateUrl:    'workspace.component.html'
})
export class WorkspaceComponent implements OnInit {
    @ViewChild('documentTabs') documentTabs: TabsetComponent
    readonly documents: any[] = []

    constructor(private route: ActivatedRoute) {
    }

    async ngOnInit() {
        this.route.queryParams.subscribe(async queryParams => {
            let actionId = queryParams.action as number
            await this.actionRequest(actionId)
        })
    }

    async addDocument(doc: any): Promise<void> {
        this.documents.push(doc)
    }

    private async actionRequest(actionId: number): Promise<void> {
        let docs = this.documents.filter(d => d.actionId === actionId)
        if(docs.length == 1) {
            docs[0].active = true
            return
        }
        else {
            let doc = {
                active:     true,
                actionId:   actionId,
                title:      `ActionID=${actionId}`
            }
            this.addDocument(doc)
        }
    }

    trackByDocumentId(index, doc) {
        return index;
    }

}
