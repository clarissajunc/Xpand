import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, Observable, tap } from 'rxjs';
import { EditPlanet, Planet } from '../models';

@Injectable({
    providedIn: 'root'
})
export class PlanetService {

    private readonly _url = 'https://localhost:44364';

    constructor(private _httpClient: HttpClient) { }

    public getAll(): Observable<Planet[]> {
        return this._httpClient.get<Planet[]>(`${this._url}/dashboard`)
            .pipe(
                tap(() => console.log('[PlanetService]: Loaded the planets.')),
                catchError((error) => {
                    console.error('[PlanetService]: Error loading the planets.')

                    throw error;
                })
            )
    }

    public get(id: number): Observable<Planet> {
        return this._httpClient.get<Planet>(`${this._url}/${id}`)
            .pipe(
                tap(() => console.log('[PlanetService]: Loaded the planet.')),
                catchError((error) => {
                    console.error('[PlanetService]: Error loading the planet.')

                    throw error;
                })
            )
    }

    public updatePlanet(id: number, planet: EditPlanet): Observable<void> {
        return this._httpClient.post<void>(`${this._url}/dashboard/${id}`, planet)
            .pipe(
                tap(() => console.log('[PlanetService]: Loaded the planets.')),
                catchError((error) => {
                    console.error('[PlanetService]: Error loading the planets.')

                    throw error;
                })
            )
    }
}
