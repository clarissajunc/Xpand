import { Component, Inject } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import {  MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { EditPlanet, Planet, PlanetStatus } from '../models';
import { PlanetService } from '../services';

@Component({
    selector: 'app-edit-planet-dialog',
    templateUrl: './edit-planet-dialog.component.html',
    styleUrls: ['./edit-planet-dialog.component.scss']
})
export class EditPlanetDialogComponent {

    public isValid: boolean = false;

    public planet: Planet;

    public planetForm: FormGroup;

    constructor(
        private _service: PlanetService,
        private _dialog: MatDialogRef<EditPlanetDialogComponent>,
        @Inject(MAT_DIALOG_DATA) data: any
    ) {
        if (!data || !data.planet) {
            this._dialog.close();
        }

        this.planet = data.planet;

        this.planetForm = new FormGroup({
            description: new FormControl(this.planet.description),
            status: new FormControl(this.planet.status)
        });

        this._dialog.updateSize('80%', 'auto');
    }

    public get availableStates(): PlanetStatus[] {
        switch (this.planetForm?.controls['status'].value) {
            case PlanetStatus.TODO: 
                return [PlanetStatus.TODO, PlanetStatus.EnRoute];
            case PlanetStatus.EnRoute: 
                return [PlanetStatus.EnRoute, PlanetStatus.OK, PlanetStatus.NotOK];
            case PlanetStatus.OK: 
            case PlanetStatus.NotOK: 
                return [PlanetStatus.OK, PlanetStatus.NotOK];
            default: 
                return [PlanetStatus.TODO, PlanetStatus.EnRoute, PlanetStatus.OK, PlanetStatus.NotOK];
        }
    }

    public getStateString(state: PlanetStatus): string {
        switch (state) {
            case PlanetStatus.TODO: return 'TODO';
            case PlanetStatus.EnRoute: return 'En Route';
            case PlanetStatus.OK: return 'OK';
            case PlanetStatus.NotOK: return '!OK';
            default: return '';
        }
    }


    public onSave(): void {
        console.log(this.planetForm.value);

        this._service.updatePlanet(this.planet.id, <EditPlanet>{
            id: this.planet.id,
            description: this.planetForm.controls['description'].value,
            planetStatus: this.planetForm.controls['status'].value,
            descriptionAuthorId: this.planetForm.controls['description'].value ? 4 : null,
        }).subscribe(() => {
            this._dialog.close(true);
        });
    }

    public close(): void {
        this._dialog.close(false);
    }
}
