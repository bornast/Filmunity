import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Person } from '../_models/person';
import { PaginatedResult } from '../_models/pagination';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable({
	providedIn: 'root'
})
export class FilmParticipantService {

	baseUrl = environment.apiUrl;

	constructor(private http: HttpClient) { }

	deleteParticipant(id: any) {
		return this.http.delete(this.baseUrl + "person/" + id);
	}

	getParticipant(id) {
		return this.http.get<Person>(this.baseUrl + "person/" + id);
	}

	createParticipant(participantToCreate) {
		return this.http.post(this.baseUrl + "person", participantToCreate);
	}

	updateParticipant(id, participantToUpdate) {
		return this.http.put(this.baseUrl + "person/" + id, participantToUpdate);
	}

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

}
