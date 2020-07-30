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
import { Watchlist } from '../_models/watchlist';

@Injectable({
	providedIn: 'root'
})
export class FilmService {

	baseUrl = environment.apiUrl;

	constructor(private http: HttpClient) { }

	// TODO: refactor these methods
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

	// TODO: move to separate service?
	getGenres() {
		return this.http.get<RecordName[]>(this.baseUrl + "genre/recordNames");
	}

	// TODO: move to separate service?
	getCountries() {
		return this.http.get<RecordName[]>(this.baseUrl + "country/recordNames");
	}

	// TODO: move to separate service?
	getLanguages() {
		return this.http.get<RecordName[]>(this.baseUrl + "language/recordNames");
	}

	// TODO: move to separate service?
	getPersons() {
		return this.http.get<RecordName[]>(this.baseUrl + "person/recordNames");
	}

	// TODO: move to separate service?
	getFilmRole() {
		return this.http.get<RecordName[]>(this.baseUrl + "filmRole/recordNames");
	}


	// TODO: move to separate service?
	setMainPhoto(photoId: any) {
		return this.http.post(this.baseUrl + "photo/setMain/" + photoId, {});
	}

	// TODO: move to separate service?
	deletePhoto(photoId: any) {
		return this.http.delete(this.baseUrl + "photo/" + photoId);
	}
	
	delete(id: any) {
		return this.http.delete(this.baseUrl + "film/" + id);
	}

	// TODO: move to separate service?
	getParticipantsByFilter(name?: string, pageNumber: any = 1, itemsPerPage: any = 5): Observable<PaginatedResult<Person[]>> {

		const paginatedResult: PaginatedResult<Person[]> = new PaginatedResult<Person[]>();

		let params = new HttpParams();
		params = params.append('pageSize', itemsPerPage);
		params = params.append('pageNumber', pageNumber);		
		if (name != null)
			params = params.append('name', name);

		return this.http.get<Person[]>(this.baseUrl + "person/", {observe: 'response', params})
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

	// TODO: move to separate service?
	deleteParticipant(id: any) {
		return this.http.delete(this.baseUrl + "person/" + id);
	}	

	// TODO: move to separate service?
	getParticipant(id) {
		return this.http.get<Person>(this.baseUrl + "person/" + id);
	}

	// TODO: move to separate service?
	createParticipant(participantToCreate) {
		return this.http.post(this.baseUrl + "person", participantToCreate);
	}

	// TODO: move to separate service?
	updateParticipant(id, participantToUpdate) {
		return this.http.put(this.baseUrl + "person/" + id, participantToUpdate);
	}

	// TODO: move to separate service?
	getUsersByFilter(name?: string, pageNumber: any = 1, itemsPerPage: any = 5): Observable<PaginatedResult<User[]>> {

		const paginatedResult: PaginatedResult<User[]> = new PaginatedResult<User[]>();

		let params = new HttpParams();
		params = params.append('pageSize', itemsPerPage);
		params = params.append('pageNumber', pageNumber);		
		if (name != null)
			params = params.append('name', name);

		return this.http.get<User[]>(this.baseUrl + "user/", {observe: 'response', params})
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

	// TODO: move to separate service?
	deleteUser(id: any) {
		return this.http.delete(this.baseUrl + "user/" + id);
	}

	// TODO: move to separate service?
	getUser(id) {
		return this.http.get<User>(this.baseUrl + "user/" + id);
	}

	// TODO: move to separate service?
	updateUser(id, userToUpdate) {
		return this.http.put(this.baseUrl + "user/" + id, userToUpdate);
	}

	// TODO: move to separate service?
	unrateFilm(id) {
		return this.http.post(this.baseUrl + "rating/unrate/" + id, {});
	}

	// TODO: move to separate service?
	rateFilm(id, objectToPost) {
		return this.http.post(this.baseUrl + "rating/rate/" + id, objectToPost);
	}

	// TODO: move to separate service?
	getLoggedUserRating(filmId) {
		return this.http.get(this.baseUrl + "rating/getLoggedUserRating/" + filmId);
	}	

	// TODO: move to separate service?
	reviewFilm(objectToPost) {
		return this.http.post(this.baseUrl + "review/", objectToPost);
	}

	// TODO: move to separate service?
	getFilmReviews(filmId: any, pageNumber: any = 1, itemsPerPage: any = 5): Observable<PaginatedResult<any[]>> {
		// return this.http.post(this.baseUrl + "review/", objectToPost);

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

	// TODO: move to separate service?
	getWatchlist(id) {
		return this.http.get<Watchlist>(this.baseUrl + "watchlist/" + id);
	}

	// TODO: move to separate service?
	createWatchlist(watchlistToCreate) {
		return this.http.post(this.baseUrl + "watchlist", watchlistToCreate);
	}

	// TODO: move to separate service?
	updateWatchlist(id, watchlistToUpdate) {
		return this.http.put(this.baseUrl + "watchlist/" + id, watchlistToUpdate);
	}

	getFilms() {
		return this.http.get<RecordName[]>(this.baseUrl + "film/recordNames");
	}

	// TODO: move to separate service?
	getWatchlistByFilter(userId?: any, title?: string, pageNumber: any = 1, itemsPerPage: any = 5): Observable<PaginatedResult<Watchlist[]>> {

		const paginatedResult: PaginatedResult<Watchlist[]> = new PaginatedResult<Watchlist[]>();

		let params = new HttpParams();
		params = params.append('pageSize', itemsPerPage);
		params = params.append('pageNumber', pageNumber);
		
		if (userId != null)
			params = params.append('orderByDescending', userId);
		if (title != null)
			params = params.append('title', title);

		return this.http.get<Watchlist[]>(this.baseUrl + "watchlist/", {observe: 'response', params})
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

	// TODO: move to separate service?
	deleteWatchlist(watchlistId: any) {
		return this.http.delete(this.baseUrl + "watchlist/" + watchlistId);
	}
}
