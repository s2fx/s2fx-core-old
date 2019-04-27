import { Component, ElementRef, OnInit, Input, ViewChild } from '@angular/core';
import { NgForm, FormGroup } from '@angular/forms';
import { NgClass, NgStyle } from '@angular/common';
import { Router } from '@angular/router';
import { getStyle, hexToRgba } from '@coreui/coreui/dist/js/coreui-utilities';
import { CustomTooltips } from '@coreui/coreui-plugin-chartjs-custom-tooltips';
import { S2fxClient } from 's2fx-client'


@Component({
    templateUrl: 'dashboard.component.html'
})
export class DashboardComponent implements OnInit {

    readonly client: S2fxClient

    constructor() {
        this.client = new S2fxClient('http://localhost:59129/Default')
    }

    async ngOnInit() {
        let menus = await this.client.viewContract.getMainMenu()
        console.log(menus)
    }
}
