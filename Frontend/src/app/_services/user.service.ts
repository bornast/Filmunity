import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { User } from '../_models/user';
import { PaginatedResult } from '../_models/pagination';
import { map } from 'rxjs/operators';

@Injectable({
	providedIn: 'root'
})
export class UserService {

	baseUrl = environment.apiUrl;

	constructor(private http: HttpClient) { }

	getUsersByFilter(name?: string, pageNumber: any = 1, itemsPerPage: any = 5): Observable<PaginatedResult<User[]>> {

		const paginatedResult: PaginatedResult<User[]> = new PaginatedResult<User[]>();

		let params = new HttpParams();
		params = params.append('pageSize', itemsPerPage);
		params = params.append('pageNumber', pageNumber);
		if (name != null)
			params = params.append('name', name);

		return this.http.get<User[]>(this.baseUrl + "user/", { observe: 'response', params })
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

	deleteUser(id: any) {
		return this.http.delete(this.baseUrl + "user/" + id);
	}

	getUser(id) {
		return this.http.get<User>(this.baseUrl + "user/" + id);
	}

	updateUser(id, userToUpdate) {
		return this.http.put(this.baseUrl + "user/" + id, userToUpdate);
	}

	getLoggedUserRating(filmId) {
		return this.http.get(this.baseUrl + "rating/getLoggedUserRating/" + filmId);
	}

}
