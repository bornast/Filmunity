import { Photo } from "./photo";

export interface Person {
    id: number;
    firstName: string;
	lastName: string;
	description: string;
    dateOfBirth: Date;
	mainPhoto: Photo;
	photos: Photo[];
	genderId: any;
}