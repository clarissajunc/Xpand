import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, Observable, tap, throwError } from 'rxjs';
import { Captain, EditPlanet, Planet } from '../models';
import { AppConfigService } from './app-config.service';

@Injectable({
    providedIn: 'root'
})
export class PlanetService {

    private readonly _url: string;

    constructor(private _httpClient: HttpClient, _appConfig: AppConfigService) { 
        this._url = `${_appConfig.apiBaseUrl}/dashboard`
    }

    public getAll(): Observable<Planet[]> {
        return this._httpClient.get<Planet[]>(this._url)
            .pipe(
                tap(() => console.log('[PlanetService]: Loaded the planets.')),
                catchError((error) => {
                    console.error('[PlanetService]: Error loading the planets.')

                    return throwError(() => error);
                })
            )
    }

    public updatePlanet(id: number, planet: EditPlanet): Observable<void> {
        return this._httpClient.post<void>(`${this._url}/${id}`, planet)
            .pipe(
                tap(() => console.log('[PlanetService]: Loaded the planets.')),
                catchError((error) => {
                    console.error('[PlanetService]: Error loading the planets.')

                    return throwError(() => error);
                })
            )
    }
}
