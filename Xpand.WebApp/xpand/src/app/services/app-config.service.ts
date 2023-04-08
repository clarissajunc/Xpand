import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, tap } from 'rxjs';

@Injectable({
    providedIn: 'root'
})
export class AppConfigService {

    private appConfig: any;
    
    constructor(private _httpClient: HttpClient) { }

    public load(): Observable<any>{
        return this._httpClient.get(('/assets/configuration.json'))
            .pipe(
                tap((config)=> this.appConfig = config)
            );
    }

    public get apiBaseUrl(): string {
        if (!this.appConfig){
            throw new Error('Configuration not loaded.');
        }

        return this.appConfig.apiBaseUrl;
    }
}
