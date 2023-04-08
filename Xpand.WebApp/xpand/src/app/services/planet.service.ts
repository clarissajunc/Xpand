import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, Observable, tap } from 'rxjs';
import { Captain, EditPlanet, Planet } from '../models';

@Injectable({
    providedIn: 'root'
})
export class PlanetService {

    private readonly _url = 'https://localhost:44364/dashboard';

    constructor(private _httpClient: HttpClient) { }

    public getAll(): Observable<Planet[]> {
        return this._httpClient.get<Planet[]>(this._url)
            .pipe(
                tap(() => console.log('[PlanetService]: Loaded the planets.')),
                catchError((error) => {
                    console.error('[PlanetService]: Error loading the planets.')

                    throw error;
                })
            )
    }

    public updatePlanet(id: number, planet: EditPlanet): Observable<void> {
        return this._httpClient.post<void>(`${this._url}/${id}`, planet)
            .pipe(
                tap(() => console.log('[PlanetService]: Loaded the planets.')),
                catchError((error) => {
                    console.error('[PlanetService]: Error loading the planets.')

                    throw error;
                })
            )
    }
}
