import { Photo } from "./photo";
import { RecordName } from "./recordName";
import { Film } from "./film";

export interface Watchlist {
    id: number;
    title: string;
    description: string;
    films: Film[];
    mainPhoto: Photo;
    photos: Photo[];
}

