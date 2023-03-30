import { PlanetStatus } from './planet-status.model';

export interface Planet {
    name: string;

    image: string;

    description?: string;

    descriptionAuthor?: string;

    status: PlanetStatus;

    robots?: string[];
}
