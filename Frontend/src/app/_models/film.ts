import { Photo } from "./photo";

export interface Film {
    id: number;
    title: string;
    description: string;
    mainPhoto: Photo;
	photos: Photo[];
	rating: number;
	imdbRating: number;
}

