import { Component, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
    selector: 'app-error-dialog',
    templateUrl: './error-dialog.component.html',
    styleUrls: ['./error-dialog.component.scss']
})
export class ErrorDialogComponent {

    public title: string;

    public text: string;

    constructor(
        @Inject(MAT_DIALOG_DATA) data: any,
        private _dialog: MatDialogRef<ErrorDialogComponent>) {

        this.title = data.title;
        this.text = data.message;

        this._dialog.updateSize('auto', 'auto');
    }

    public close(): void {
        this._dialog.close();
    }
}
