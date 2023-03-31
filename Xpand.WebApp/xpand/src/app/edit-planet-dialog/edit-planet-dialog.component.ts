import { Component, Inject } from '@angular/core';
import {  MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Planet } from '../models';

@Component({
    selector: 'app-edit-planet-dialog',
    templateUrl: './edit-planet-dialog.component.html',
    styleUrls: ['./edit-planet-dialog.component.scss']
})
export class EditPlanetDialogComponent {

    public isValid: boolean = false;

    public planet: Planet;

    constructor(
        private _dialog: MatDialogRef<EditPlanetDialogComponent>,
        @Inject(MAT_DIALOG_DATA) data: any
    ) {
        if (!data || !data.planet) {
            this._dialog.close();
        }

        this.planet = data.planet;

        this._dialog.updateSize('80%', 'auto');
    }

    public close(): void {
        this._dialog.close();
    }
}
