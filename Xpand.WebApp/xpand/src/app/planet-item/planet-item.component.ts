import { Component, Input } from '@angular/core';
import { Planet, PlanetStatus } from '../models';

@Component({
    selector: 'planet-item',
    templateUrl: './planet-item.component.html',
    styleUrls: ['./planet-item.component.scss']
})
export class PlanetItemComponent {

    @Input()
    public planet!: Planet;

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
}
