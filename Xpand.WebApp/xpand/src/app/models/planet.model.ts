import { SafeUrl } from '@angular/platform-browser';
import { PlanetStatus } from './planet-status.model';

export interface Planet {
    id: number;

    name: string;

    image: any[];

    imageUrl: SafeUrl;

    description?: string;

    descriptionAuthor?: string;

    status: PlanetStatus;

    robots?: string[];
}
