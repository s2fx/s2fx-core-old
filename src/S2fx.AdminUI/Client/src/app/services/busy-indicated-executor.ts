import { Injectable } from "@angular/core";
import { NgxSpinnerService } from 'ngx-spinner';

export class ExecutorContext {
    text: string
    constructor(private readonly spinner: NgxSpinnerService) {
    }

    async show(): Promise<void> {
        await this.spinner.show()
    }

    async hide(): Promise<void> {
        await this.spinner.hide()
    }
}

@Injectable({
    providedIn: 'root'
})
export class BusyIndicatedExecutor {
    private _isBusy = false

    constructor(private readonly spinner: NgxSpinnerService) {
    }

    get isBusy(): boolean { return this._isBusy }

    async executeAppBlockedTask(action: (context: ExecutorContext) => Promise<void> ): Promise<void> {
        if(this._isBusy) {
            return;
        }
        this._isBusy = true
        let context = new ExecutorContext(this.spinner)
        await this.spinner.show()
        try {
            await action(context)
        }
        finally{
            this._isBusy = false
            await this.spinner.hide()
        }
    }

    async executeLongTermTask(action: () => Promise<void>): Promise<void> {
        await action()
    }

}
