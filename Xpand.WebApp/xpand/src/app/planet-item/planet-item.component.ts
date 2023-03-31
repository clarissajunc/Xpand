import { Component, Input } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { EditPlanetDialogComponent } from '../edit-planet-dialog/edit-planet-dialog.component';
import { Planet, PlanetStatus } from '../models';

@Component({
    selector: 'planet-item',
    templateUrl: './planet-item.component.html',
    styleUrls: ['./planet-item.component.scss']
})
export class PlanetItemComponent {

    @Input()
    public planet!: Planet;

    constructor(private _dialogHelper: MatDialog) {

    }
    public getStatusStyle(): any {
        switch (this.planet.status) {
            case PlanetStatus.TODO:
                return {
                    text: 'TODO',
                    cssClass: 'status__todo',
                };
            case PlanetStatus.EnRoute:
                return {
                    text: 'En Route',
                    cssClass: 'status__enroute',
                };
            case PlanetStatus.NotOK:
                return {
                    text: 'OK',
                    cssClass: 'status__ok',
                };
            case PlanetStatus.OK:
                return {
                    text: '!OK',
                    cssClass: 'status__notok',
                };
            default:
                return undefined;
        }
    }

    public onPlanetClicked(): void {
        this._dialogHelper.open(EditPlanetDialogComponent, {
            data: {
                planet: this.planet
            }
        })
    }
}
