import { Component, OnInit } from '@angular/core';
import { User } from 'src/app/_models/user';
import { FilmService } from 'src/app/_services/film.service';
import { ActivatedRoute } from '@angular/router';
import { Watchlist } from 'src/app/_models/watchlist';

@Component({
  selector: 'app-user-view',
  templateUrl: './user-view.component.html',
  styleUrls: ['./user-view.component.css']
})
export class UserViewComponent implements OnInit {

	user: User;
	watchlists: Watchlist[];

	constructor(private filmService: FilmService,  private route: ActivatedRoute) { }

	ngOnInit() {
		this.getUser(this.route.snapshot.params['id']);
	}

	getUser(id: any) {
		this.filmService.getUser(id).subscribe((user) => {
			this.user = user;
			this.getWatchlist(user.id)
		});
	}

	getWatchlist(userId: any) {
		this.filmService.getWatchlistByFilter(userId, null, 1, 999).subscribe((watchlists) => {
			this.watchlists = watchlists.result;
		});
	}

}
