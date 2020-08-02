import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';

@Injectable({
	providedIn: 'root'
})
export class PhotoService {

	baseUrl = environment.apiUrl;

	constructor(private http: HttpClient) { }

	setMainPhoto(photoId: any) {
		return this.http.post(this.baseUrl + "photo/setMain/" + photoId, {});
	}

	deletePhoto(photoId: any) {
		return this.http.delete(this.baseUrl + "photo/" + photoId);
	}

}
