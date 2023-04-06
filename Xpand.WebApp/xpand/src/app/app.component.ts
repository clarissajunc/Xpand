import { Component, OnInit } from '@angular/core';
import { BehaviorSubject, combineLatest, map, mergeMap, Observable, Subject } from 'rxjs';
import { Planet } from './models';
import { PlanetService } from './services';
import { DomSanitizer } from '@angular/platform-browser';

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {

    public refreshSubject = new BehaviorSubject<any>(1);

    public planets$: Observable<Planet[]> = new Observable<Planet[]>();

    constructor(private _service: PlanetService, private _domSanitizer: DomSanitizer) {

    }

    public ngOnInit(): void {
        this.planets$ = this.refreshSubject.asObservable().pipe(
            mergeMap(() => this._service.getAll()),
            map((planets) => {
                // planets.forEach(planet => {
                //     planet.imageUrl = this._domSanitizer.bypassSecurityTrustUrl('data:image/jpg;base64, ' + planet.image);
                // });
                //todo
                return planets;
            })
        );
    }

    title = 'xpand';

    public onUpdated(): void {
        this.refreshSubject.next(1);
    }
}
