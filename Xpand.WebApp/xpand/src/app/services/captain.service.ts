import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, Observable, tap, throwError } from 'rxjs';
import { Captain } from '../models';
import { AppConfigService } from './app-config.service';

@Injectable({
    providedIn: 'root'
})
export class CaptainService {

    private readonly _url: string;

    constructor(private _httpClient: HttpClient, _appConfig: AppConfigService) {
        this._url = `${_appConfig.apiBaseUrl}/captains`
    }

    public getAll(): Observable<Captain[]> {
        return this._httpClient.get<Captain[]>(this._url)
            .pipe(
                tap(() => console.log('[CaptainService]: Loaded the captains.')),
                catchError((error) => {
                    console.error('[CaptainService]: Error loading the captains.')

                    return throwError(() => error);
                })
            )
    }
}
