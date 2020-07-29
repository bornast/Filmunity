import { Photo } from "./photo";
import { RecordName } from "./recordName";

export interface User {
    id: number;
    username: string;
    firstName: string;
    lastName: string;
    interests: string;
    roles: RecordName[];
    mainPhoto: Photo;
    photos: Photo[];
}