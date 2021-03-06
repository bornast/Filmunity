import { Component, OnInit } from '@angular/core';
import { Pagination } from 'src/app/_models/pagination';
import { Watchlist } from 'src/app/_models/watchlist';
import { ToastService } from 'src/app/_services/toast.service';
import { WatchlistService } from 'src/app/_services/watchlist.service';

@Component({
	selector: 'app-user-watchlists',
	templateUrl: './user-watchlists.component.html',
	styleUrls: ['./user-watchlists.component.css']
})
export class UserWatchlistsComponent implements OnInit {

	loggedUserId: any;
	watchlists: Watchlist[];
	pagination: Pagination;
	pageNumber: any = 1;

	constructor(private watchlistService: WatchlistService, private toast: ToastService) { }

	ngOnInit() {
		this.loggedUserId = localStorage.getItem('filmunity-userId');
		this.loadWatchlists();
	}

	loadWatchlists() {		
		this.watchlistService.getWatchlistByFilter(this.loggedUserId, null,this.pageNumber, 4).subscribe((watchlists) => {
			this.pagination = watchlists.pagination;
			this.watchlists = watchlists.result;
		});
	}

	changePage(pageNumber: number) {
		this.pageNumber = pageNumber;
		this.loadWatchlists();
	}

	delete(id: any) {
		if (confirm("Are you sure you want to delete this record?")) {
			this.watchlistService.deleteWatchlist(id).subscribe(() => {
				this.pageNumber = 1;
				this.loadWatchlists();
				this.toast.success("Successfully delete!");
			}, () => {
				this.toast.error("Failed to delete!");
			});
		}
	}

}
