import { Photo } from "./photo";
import { RecordName } from "./recordName";

export interface Film {
    id: number;
    title: string;
    description: string;
    mainPhoto: Photo;
	photos: Photo[];
	rating: number;
	imdbRating: number;
	genres: any[];
	type: RecordName;
	country: RecordName;
	language: RecordName;
    year: string;
    duration: string;
    participants: any[];
}

