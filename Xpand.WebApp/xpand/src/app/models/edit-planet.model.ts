import { PlanetStatus } from ".";

export interface EditPlanet {
    id: number;

    description: string;

    descriptionAuthorId: number;

    planetStatus: PlanetStatus;
}