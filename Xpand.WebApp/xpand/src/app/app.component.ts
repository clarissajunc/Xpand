import { Component } from '@angular/core';
import { Planet, PlanetStatus } from './models';

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.scss']
})
export class AppComponent {
    title = 'xpand';

    planets = [
        <Planet>{
            name: 'Tau 23',
            image: 'assets/planet4.png',
            description: 'While visiting this planet, the robots have uncovered various forms of life',
            descriptionAuthor: 'Jonathan Smith',
            robots: ['T2011', 'T2020', 'T2031'],
            status: PlanetStatus.OK
        },
        <Planet>{
            name: 'Zeita 7',
            image: 'assets/planet5.png',
            description: '0.2% nutrients in the soil. Unfortunately that cannot sustain life.',
            descriptionAuthor: 'Hannah Intellis',
            robots: ['T21', 'T28', 'T29'],
            status: PlanetStatus.NotOK
        },
        <Planet>{
            name: 'Sigma 17',
            image: 'assets/planet6.png',
            description: undefined,
            descriptionAuthor: undefined,
            robots: undefined,
            status: PlanetStatus.EnRoute
        },
        <Planet>{
            name: 'Kappa 44',
            image: 'assets/planet6.png',
            description: 'We\'ve found another sapient species and have engaged in communication',
            descriptionAuthor: 'Eva Brains',
            robots: ['T201'],
            status: PlanetStatus.OK
        },
        <Planet>{
            name: 'Tau 24',
            image: 'assets/planet7.png',
            description: 'Just a huge floating rock',
            descriptionAuthor: 'Jonathan Smith',
            robots: ['T18', 'T19', 'T31'],
            status: PlanetStatus.NotOK
        }
    ]
}
