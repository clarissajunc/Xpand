import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { MatDialogModule } from '@angular/material/dialog';
import { BrowserModule } from '@angular/platform-browser';
import { AppComponent } from './app.component';
import { EditPlanetDialogComponent } from './edit-planet-dialog/edit-planet-dialog.component';
import { PlanetItemComponent } from './planet-item/planet-item.component';

@NgModule({
    declarations: [
        AppComponent,
        PlanetItemComponent,
        EditPlanetDialogComponent
    ],
    imports: [
        BrowserModule,
        MatDialogModule,
        HttpClientModule
    ],
    providers: [],
    bootstrap: [AppComponent]
})
export class AppModule { }
