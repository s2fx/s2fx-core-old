import { Component, ElementRef, OnInit, Input, ViewChild } from '@angular/core';
import { NgForm, FormGroup } from '@angular/forms';
import { NgClass, NgStyle } from '@angular/common';
import { Router } from '@angular/router';
import { getStyle, hexToRgba } from '@coreui/coreui/dist/js/coreui-utilities';
import { CustomTooltips } from '@coreui/coreui-plugin-chartjs-custom-tooltips';
import { NgxSpinnerService } from 'ngx-spinner';

import { SetupContract } from '../s2fx-client/contracts'
import { ISetupOptions } from '../s2fx-client/models'

@Component({
  selector: 'app-dashboard',
  templateUrl: 'setup.page.component.html'
})
export class SetupPageComponent implements OnInit {

    isBusy: boolean = false
    busyIndicatorText: string 

    options: ISetupOptions = {
        AdminPassword: '',
        DbName: '',
        IsDemo: true,
        EnabledFeatures: []
    };


    constructor(private setupContract: SetupContract, private spinner: NgxSpinnerService) {
    }

    async ngOnInit() {
        this.isBusy = true
        this.busyIndicatorText = "Loading..."
        this.spinner.show()
        try {
        }
        finally {
            this.isBusy = false
            this.spinner.hide()
        }
    }

    async onStartButtonClicked() {
        this.isBusy = true
        this.busyIndicatorText = "Processing..."
        this.spinner.show()
        try {
            await this.setupContract.startSetup(this.options)
            console.log(this.options)
            await new Promise((resolve) => setTimeout(resolve, 1000));
        }
        finally {
            this.isBusy = false
            this.spinner.hide()
        }
    }


}
