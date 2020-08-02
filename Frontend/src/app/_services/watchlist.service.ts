import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { PaginatedResult } from '../_models/pagination';
import { Watchlist } from '../_models/watchlist';
import { map } from 'rxjs/operators';

@Injectable({
	providedIn: 'root'
})
export class WatchlistService {

	baseUrl = environment.apiUrl;

	constructor(private http: HttpClient) { }

	getWatchlistByFilter(userId?: any, title?: string, pageNumber: any = 1, itemsPerPage: any = 5): Observable<PaginatedResult<Watchlist[]>> {

		const paginatedResult: PaginatedResult<Watchlist[]> = new PaginatedResult<Watchlist[]>();

		let params = new HttpParams();
		params = params.append('pageSize', itemsPerPage);
		params = params.append('pageNumber', pageNumber);
		params = params.append('orderByDescending', "CreatedAt");

		if (userId != null)
			params = params.append('userId', userId);
		if (title != null)
			params = params.append('title', title);

		return this.http.get<Watchlist[]>(this.baseUrl + "watchlist/", { observe: 'response', params })
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

	deleteWatchlist(watchlistId: any) {
		return this.http.delete(this.baseUrl + "watchlist/" + watchlistId);
	}

	getWatchlist(id) {
		return this.http.get<Watchlist>(this.baseUrl + "watchlist/" + id);
	}

	createWatchlist(watchlistToCreate) {
		return this.http.post(this.baseUrl + "watchlist", watchlistToCreate);
	}

	updateWatchlist(id, watchlistToUpdate) {
		return this.http.put(this.baseUrl + "watchlist/" + id, watchlistToUpdate);
	}

}
