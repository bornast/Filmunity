import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient, HttpParams } from '@angular/common/http';
import { User } from '../_models/user';
import { Observable } from 'rxjs';
import { PaginatedResult } from '../_models/pagination';
import { map } from 'rxjs/operators';
import { RecordName } from '../_models/recordName';

@Injectable({
	providedIn: 'root'
})
export class FriendshipService {

	baseUrl = environment.apiUrl;

	constructor(private http: HttpClient) { }

	// TODO: move to separate service?
	getFriendshipRequests() {
		return this.http.get<User[]>(this.baseUrl + "friendship/getAllFriendRequests");
	}

	// TODO: move to separate service?
	acceptFriendshipRequest(userId) {
		return this.http.post(this.baseUrl + "friendship/acceptFriendRequest/" + userId, {});
	}

	// TODO: move to separate service?
	declineFriendshipRequest(userId) {
		return this.http.post(this.baseUrl + "friendship/declineFriendRequest/" + userId, {});
	}

	// TODO: move to separate service?
	getAllFriends(pageNumber: any = 1, itemsPerPage: any = 5): Observable<PaginatedResult<User[]>> {

		const paginatedResult: PaginatedResult<User[]> = new PaginatedResult<User[]>();

		let params = new HttpParams();
		params = params.append('pageSize', itemsPerPage);
		params = params.append('pageNumber', pageNumber);

		return this.http.get<User[]>(this.baseUrl + "friendship/getAllFriends", { observe: 'response', params })
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
	sendFriendRequest(userId) {
		return this.http.post(this.baseUrl + "friendship/sendFriendRequest/" + userId, {});
	}

	// TODO: move to separate service?
	getFriendshipStatus(userId) {
		return this.http.get<RecordName>(this.baseUrl + "friendship/getFriendshipStatus/" + userId, {});
	}

}
