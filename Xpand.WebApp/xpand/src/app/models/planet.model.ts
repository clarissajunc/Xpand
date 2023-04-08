import { SafeUrl } from '@angular/platform-browser';
import { Captain } from './captain.model';
import { PlanetStatus } from './planet-status.model';

export interface Planet {
    id: number;

    name: string;

    image: any[];

    imageUrl: SafeUrl;

    description?: string;

    descriptionAuthor?: Captain;

    status: PlanetStatus;

    robots?: string[];
}
