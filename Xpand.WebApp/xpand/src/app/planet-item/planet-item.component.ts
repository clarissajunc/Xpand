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
                    iconSrc: 'assets/screenfail.svg',
                    text: 'TODO',
                    cssClass: 'status__todo',
                };
            case PlanetStatus.EnRoute:
                return {
                    iconSrc: 'assets/not-enrolled.svg',
                    text: 'En Route',
                    cssClass: 'status__enroute',
                };
            case PlanetStatus.NotOK:
                return {
                    iconSrc: 'assets/check.svg',
                    text: 'participating',
                    cssClass: 'OK',
                };
            case PlanetStatus.OK:
                return {
                    iconSrc: 'assets/check.svg',
                    text: 'participating',
                    cssClass: '!OK',
                };
            default:
                return undefined;
        }
    }
}
