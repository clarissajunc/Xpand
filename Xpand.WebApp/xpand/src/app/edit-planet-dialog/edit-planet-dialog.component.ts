import { Component, Inject } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import {  MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Observable } from 'rxjs';
import { Captain, EditPlanet, Planet, PlanetStatus } from '../models';
import { CaptainService, PlanetService } from '../services';

@Component({
    selector: 'app-edit-planet-dialog',
    templateUrl: './edit-planet-dialog.component.html',
    styleUrls: ['./edit-planet-dialog.component.scss']
})
export class EditPlanetDialogComponent {

    public isValid: boolean = false;

    public planet: Planet;

    public planetForm: FormGroup;

    public captains$: Observable<Captain[]>;

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

    constructor(
        private _planetService: PlanetService,
        private _captainService: CaptainService,
        private _dialog: MatDialogRef<EditPlanetDialogComponent>,
        @Inject(MAT_DIALOG_DATA) data: any
    ) {
        if (!data || !data.planet) {
            this._dialog.close();
        }

        this.planet = data.planet;

        this.planetForm = new FormGroup({
            authorId: new FormControl(this.planet.descriptionAuthor?.id),
            description: new FormControl(this.planet.description),
            status: new FormControl(this.planet.status)
        });

        this._dialog.updateSize('80%', 'auto');

        this.captains$ = this._captainService.getAll();
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

        this._planetService.updatePlanet(this.planet.id, <EditPlanet>{
            id: this.planet.id,
            description: this.planetForm.controls['description'].value,
            planetStatus: this.planetForm.controls['status'].value,
            descriptionAuthorId: this.planetForm.controls['authorId'].value,
        }).subscribe(() => {
            this._dialog.close(true);
        });
    }

    public close(): void {
        this._dialog.close(false);
    }
}
