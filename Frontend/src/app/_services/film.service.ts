import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Film } from '../_models/film';
import { RecordName } from '../_models/recordName';
import { PaginatedResult } from '../_models/pagination';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { Person } from '../_models/person';
import { User } from '../_models/user';

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

	getFilmsByFilter(filmType?: any, orderBy?: string, genreId?: string, title?: string, pageNumber: any = 1, itemsPerPage: any = 5): Observable<PaginatedResult<Film[]>> {

		const paginatedResult: PaginatedResult<Film[]> = new PaginatedResult<Film[]>();

		let params = new HttpParams();
		params = params.append('pageSize', itemsPerPage);
		params = params.append('pageNumber', pageNumber);
		if (filmType != null)
			params = params.append('filmType', filmType);
		if (orderBy != null)
			params = params.append('orderByDescending', orderBy);
		if (genreId != null)
			params = params.append('genreId', genreId);
		if (title != null)
			params = params.append('title', title);

		return this.http.get<Film[]>(this.baseUrl + "film/", {observe: 'response', params})
		.pipe(
			map(response => {
				paginatedResult.result = response.body;
				if (response.headers.get('Pagination') != null) {
					paginatedResult.pagination = JSON.parse(response.headers.get('Pagination'));
				}
				return paginatedResult;
			})
		);
	}

	getFilm(id) {
		return this.http.get<Film>(this.baseUrl + "film/" + id);
	}

	createFilm(filmToCreate) {
		return this.http.post(this.baseUrl + "film", filmToCreate);
	}

	updateFilm(id, filmToUpdate) {
		return this.http.put(this.baseUrl + "film/" + id, filmToUpdate);
	}
	
	getGenres() {
		return this.http.get<RecordName[]>(this.baseUrl + "genre/recordNames");
	}

	getCountries() {
		return this.http.get<RecordName[]>(this.baseUrl + "country/recordNames");
	}

	getLanguages() {
		return this.http.get<RecordName[]>(this.baseUrl + "language/recordNames");
	}

	getPersons() {
		return this.http.get<RecordName[]>(this.baseUrl + "person/recordNames");
	}

	getFilmRole() {
		return this.http.get<RecordName[]>(this.baseUrl + "filmRole/recordNames");
	}	
	
	deleteFilm(id: any) {
		return this.http.delete(this.baseUrl + "film/" + id);
	}

	unrateFilm(id) {
		return this.http.post(this.baseUrl + "rating/unrate/" + id, {});
	}

	rateFilm(id, objectToPost) {
		return this.http.post(this.baseUrl + "rating/rate/" + id, objectToPost);
	}

	reviewFilm(objectToPost) {
		return this.http.post(this.baseUrl + "review/", objectToPost);
	}

	commentFilm(objectToPost) {
		return this.http.post(this.baseUrl + "filmComment/", objectToPost);
	}

	getFilmComments(filmId: any, pageNumber: any = 1, itemsPerPage: any = 5): Observable<PaginatedResult<any[]>> {
		const paginatedResult: PaginatedResult<any[]> = new PaginatedResult<any[]>();

		let params = new HttpParams();
		params = params.append('pageSize', itemsPerPage);
		params = params.append('pageNumber', pageNumber);		
		params = params.append('filmId', filmId);
		params = params.append('orderByDescending', "CreatedAt");

		return this.http.get<any[]>(this.baseUrl + "filmComment/", {observe: 'response', params})
		.pipe(
			map(response => {
				paginatedResult.result = response.body;
				if (response.headers.get('Pagination') != null) {
					paginatedResult.pagination = JSON.parse(response.headers.get('Pagination'));
				}
				return paginatedResult;
			})
		);

	}

	getFilmReviews(filmId: any, pageNumber: any = 1, itemsPerPage: any = 5): Observable<PaginatedResult<any[]>> {
		const paginatedResult: PaginatedResult<any[]> = new PaginatedResult<any[]>();

		let params = new HttpParams();
		params = params.append('pageSize', itemsPerPage);
		params = params.append('pageNumber', pageNumber);		
		params = params.append('filmId', filmId);
		params = params.append('orderByDescending', "CreatedAt");

		return this.http.get<any[]>(this.baseUrl + "review/", {observe: 'response', params})
		.pipe(
			map(response => {
				paginatedResult.result = response.body;
				if (response.headers.get('Pagination') != null) {
					paginatedResult.pagination = JSON.parse(response.headers.get('Pagination'));
				}
				return paginatedResult;
			})
		);

	}

	getFilms() {
		return this.http.get<RecordName[]>(this.baseUrl + "film/recordNames");
	}		

	markAsWatched(filmId) {
		return this.http.post(this.baseUrl + "film/markAsWatched/" + filmId, {});
	}

}
