import { Component, ElementRef, OnInit, Input, ViewChild } from '@angular/core';
import { NgForm, FormGroup } from '@angular/forms';
import { NgClass, NgStyle } from '@angular/common';
import { Router } from '@angular/router';
import { getStyle, hexToRgba } from '@coreui/coreui/dist/js/coreui-utilities';
import { CustomTooltips } from '@coreui/coreui-plugin-chartjs-custom-tooltips';

import { SetupContract } from '../../s2fx-client/contracts'
import { ISetupOptions } from '../../s2fx-client/models'

@Component({
  selector: 'app-dashboard',
  templateUrl: 'setup.page.component.html'
})
export class SetupPageComponent implements OnInit {

    isBusy: boolean = false

    options: ISetupOptions = {
        AdminPassword: '',
        DbName: '',
        IsDemo: true,
        EnabledFeatures: []
    };


    constructor(private setupContract: SetupContract) {
    }

    async ngOnInit() {
        this.isBusy = true
        try {
        }
        finally {
            this.isBusy = false
        }
    }

    async onStartButtonClicked() {
        this.isBusy = true
        try {
            await this.setupContract.startSetup(this.options)
            console.log(this.options)
            await new Promise((resolve) => setTimeout(resolve, 1000));
        }
        finally {
            this.isBusy = false
        }
    }


}
