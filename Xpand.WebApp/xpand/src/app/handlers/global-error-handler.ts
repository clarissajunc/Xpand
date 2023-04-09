import { ErrorHandler, Injectable, NgZone } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ErrorDialogComponent } from '../error-dialog/error-dialog.component';

@Injectable()
export class GlobalErrorHandler implements ErrorHandler {

    constructor(
        private _dialogHelper: MatDialog,
        private _zone: NgZone) { }

    public handleError(error: any) {
        this._zone.run(() =>
            this._dialogHelper.open(ErrorDialogComponent, {
                data: {
                    title: 'Oops! Something is wrong :(',
                    message: error?.error?.detail ?? 'An undefined error occurred'
                }
            }));

        console.error(error);
    }
}