import { Injectable, Inject } from '@angular/core';
import { Component, ElementRef, OnInit, Input, ViewChild } from '@angular/core';
import { NgForm, FormGroup } from '@angular/forms';
import { NgClass, NgStyle } from '@angular/common';
import { Router } from '@angular/router';
import { getStyle, hexToRgba } from '@coreui/coreui/dist/js/coreui-utilities';
import { CustomTooltips } from '@coreui/coreui-plugin-chartjs-custom-tooltips';
import { NgS2fxClient  } from '../../s2fx-client-angular/s2client'


@Component({
    templateUrl: 'dashboard.component.html'
})
export class DashboardComponent implements OnInit {

    constructor(private readonly client: NgS2fxClient) {
    }

    async ngOnInit() {
    }
}
