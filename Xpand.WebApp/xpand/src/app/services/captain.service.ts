import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, Observable, tap } from 'rxjs';
import { Captain } from '../models';

@Injectable({
    providedIn: 'root'
})
export class CaptainService {

    private readonly _url = 'https://localhost:44364/captains';

    constructor(private _httpClient: HttpClient) { }

    public getAll(): Observable<Captain[]> {
        return this._httpClient.get<Captain[]>(this._url)
            .pipe(
                tap(() => console.log('[CaptainService]: Loaded the captains.')),
                catchError((error) => {
                    console.error('[CaptainService]: Error loading the captains.')

                    throw error;
                })
            )
    }
}
