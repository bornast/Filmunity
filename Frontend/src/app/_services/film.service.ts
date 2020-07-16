import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Film } from '../_models/film';
import { FILMTYPE } from '../_constants/filmTypeConst';

@Injectable({
	providedIn: 'root'
})
export class FilmService {

	baseUrl = environment.apiUrl + 'film/';

	constructor(private http: HttpClient) { }

	getTopRatedFilms(filmType?: any, itemsPerPage: any = 10) {

		let params = new HttpParams();
		params = params.append('pageSize', itemsPerPage);
		params = params.append('orderByDescending', "Rating");
		if (filmType != null)
			params = params.append('filmType', filmType);		

		return this.http.get<Film[]>(this.baseUrl, {params});
	}

}
