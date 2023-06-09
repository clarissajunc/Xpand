import { HttpClientModule } from '@angular/common/http';
import { APP_INITIALIZER, ErrorHandler, NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { MatDialogModule } from '@angular/material/dialog';
import { BrowserModule } from '@angular/platform-browser';
import { AppComponent } from './app.component';
import { EditPlanetDialogComponent } from './edit-planet-dialog/edit-planet-dialog.component';
import { PlanetItemComponent } from './planet-item/planet-item.component';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
import { MatInputModule } from '@angular/material/input';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatButtonModule } from '@angular/material/button';
import { TextFieldModule } from '@angular/cdk/text-field';
import { AppConfigService } from './services';
import { ErrorDialogComponent } from './error-dialog/error-dialog.component';
import { GlobalErrorHandler } from './handlers';

@NgModule({
    declarations: [
        AppComponent,
        PlanetItemComponent,
        EditPlanetDialogComponent,
        ErrorDialogComponent
    ],
    imports: [
        BrowserModule,
        MatDialogModule,
        HttpClientModule,
        ReactiveFormsModule,
        MatFormFieldModule,
        MatInputModule,
        MatSelectModule,
        MatButtonModule,
        TextFieldModule,
        BrowserAnimationsModule
    ],
    providers: [
        {
            provide: APP_INITIALIZER,
            deps: [AppConfigService],
            multi: true,
            useFactory: (appConfigService: AppConfigService) => {
                return () => appConfigService.load();
            }
        },
        {
            provide: ErrorHandler,
            useClass: GlobalErrorHandler,
        }
    ],
    bootstrap: [AppComponent]
})
export class AppModule { }
