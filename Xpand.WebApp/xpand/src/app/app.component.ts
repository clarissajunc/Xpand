import { Component, OnInit } from '@angular/core';
import { map, Observable } from 'rxjs';
import { Planet } from './models';
import { PlanetService } from './services';
import { DomSanitizer } from '@angular/platform-browser';

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {

    public planets$: Observable<Planet[]> = new Observable<Planet[]>();

    constructor(private _service: PlanetService, private _domSanitizer: DomSanitizer) {

    }

    public ngOnInit(): void {
        this.planets$ = this._service.getAll()
            .pipe(
                map((planets) => {
                    planets.forEach(planet => {
                        planet.imageUrl = this._domSanitizer.bypassSecurityTrustUrl('data:image/jpg;base64, ' + planet.image);
                    });

                    return planets;
                })
            );
    }

    title = 'xpand';

    // planets = [
    //     <Planet>{
    //         name: 'Tau 23',
    //         image: 'assets/planet4.png',
    //         description: 'While visiting this planet, the robots have uncovered various forms of life',
    //         descriptionAuthor: 'Jonathan Smith',
    //         robots: ['T2011', 'T2020', 'T2031'],
    //         status: PlanetStatus.OK
    //     },
    //     <Planet>{
    //         name: 'Zeita 7',
    //         image: 'assets/planet5.png',
    //         description: '0.2% nutrients in the soil. Unfortunately that cannot sustain life.',
    //         descriptionAuthor: 'Hannah Intellis',
    //         robots: ['T21', 'T28', 'T29'],
    //         status: PlanetStatus.NotOK
    //     },
    //     <Planet>{
    //         name: 'Sigma 17',
    //         image: 'assets/planet6.png',
    //         description: undefined,
    //         descriptionAuthor: undefined,
    //         robots: undefined,
    //         status: PlanetStatus.EnRoute
    //     },
    //     <Planet>{
    //         name: 'Kappa 44',
    //         image: 'assets/planet6.png',
    //         description: 'We\'ve found another sapient species and have engaged in communication',
    //         descriptionAuthor: 'Eva Brains',
    //         robots: ['T201'],
    //         status: PlanetStatus.OK
    //     },
    //     <Planet>{
    //         name: 'Tau 24',
    //         image: 'assets/planet7.png',
    //         description: 'Just a huge floating rock',
    //         descriptionAuthor: 'Jonathan Smith',
    //         robots: ['T18', 'T19', 'T31'],
    //         status: PlanetStatus.NotOK
    //     }
    // ]
}
