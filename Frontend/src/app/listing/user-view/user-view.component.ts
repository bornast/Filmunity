import { Component, OnInit } from '@angular/core';
import { User } from 'src/app/_models/user';
import { FilmService } from 'src/app/_services/film.service';
import { ActivatedRoute } from '@angular/router';
import { Watchlist } from 'src/app/_models/watchlist';
import { ToastService } from 'src/app/_services/toast.service';
import { RecordName } from 'src/app/_models/recordName';

@Component({
  selector: 'app-user-view',
  templateUrl: './user-view.component.html',
  styleUrls: ['./user-view.component.css']
})
export class UserViewComponent implements OnInit {

	loggedUserId: any;
	user: User;
	watchlists: Watchlist[];
	friendshipStatus: RecordName;

	constructor(private filmService: FilmService,  private route: ActivatedRoute, private toast: ToastService) { }

	ngOnInit() {
		this.loggedUserId = localStorage.getItem("filmunity-userId");

		let userId = this.route.snapshot.params['id'];
		this.getUser(userId);
		this.getFriendshipStatus(userId);
	}	

	getUser(id: any) {
		this.filmService.getUser(id).subscribe((user) => {
			this.user = user;
			this.getWatchlist(user.id)
		});
	}

	getFriendshipStatus(userId: any) {
		this.filmService.getFriendshipStatus(userId).subscribe((friendshipStatus) => {
			this.friendshipStatus = friendshipStatus;
		});
	}

	getWatchlist(userId: any) {
		this.filmService.getWatchlistByFilter(userId, null, 1, 999).subscribe((watchlists) => {
			this.watchlists = watchlists.result;
		});
	}

	sendFriendRequest(userId) {
		this.filmService.sendFriendRequest(userId).subscribe(() => {
			this.toast.success("Friend request successfully sent!");
			this.getFriendshipStatus(userId);
		}, () => {
			this.toast.error("Failed to send a friend request!");
		});
	}

}
