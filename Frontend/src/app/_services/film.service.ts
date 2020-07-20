import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Film } from '../_models/film';
import { RecordName } from '../_models/recordName';

@Injectable({
	providedIn: 'root'
})
export class FilmService {

	baseUrl = environment.apiUrl;

	constructor(private http: HttpClient) { }

	getTopRatedFilms(filmType?: any, itemsPerPage: any = 10) {

		let params = new HttpParams();
		params = params.append('pageSize', itemsPerPage);
		params = params.append('orderByDescending', "Rating");
		if (filmType != null)
			params = params.append('filmType', filmType);		

		return this.http.get<Film[]>(this.baseUrl + "film/", {params});
	}

	getFilmsByFilter(filmType?: any, orderBy?: string, genreId?: string, title?: string, itemsPerPage: any = 10) {

		let params = new HttpParams();
		params = params.append('pageSize', itemsPerPage);
		if (filmType != null)
			params = params.append('filmType', filmType);
		if (orderBy != null)
			params = params.append('orderByDescending', orderBy);
		if (genreId != null)
			params = params.append('genreId', genreId);
		if (title != null)
			params = params.append('title', title);

		return this.http.get<Film[]>(this.baseUrl + "film/", {params});
	}

	getGenres() {
		return this.http.get<RecordName[]>(this.baseUrl + "genre/recordNames");
	}
}
