import { Injectable, Inject } from '@angular/core';
import { Component, ElementRef, OnInit, Input, ViewChild } from '@angular/core';
import { NgForm, FormGroup } from '@angular/forms';
import { NgClass, NgStyle } from '@angular/common';
import { Router, NavigationEnd } from '@angular/router';
import { getStyle, hexToRgba } from '@coreui/coreui/dist/js/coreui-utilities';
import { CustomTooltips } from '@coreui/coreui-plugin-chartjs-custom-tooltips';
import { NgS2fxClient  } from '../../s2fx-client-angular/s2client'


@Component({
    selector:       'workspace',
    templateUrl:    'workspace.component.html'
})
export class WorkspaceComponent implements OnInit {

    public readonly documents: any[] = []

    constructor(private router: Router) {
        /*
        router.events.subscribe((val) => {
        })
        */
    }

    async ngOnInit() {
        this.addDocument({title: 'Fcnked'})
    }

    async addDocument(doc: any): Promise<void> {
        this.documents.push(doc)
    }

    trackByDocumentId(index, doc) {
        return index;
    }


}
