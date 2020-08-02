import { Component, OnInit } from '@angular/core';
import { User } from 'src/app/_models/user';
import { FilmService } from 'src/app/_services/film.service';
import { ActivatedRoute } from '@angular/router';
import { Watchlist } from 'src/app/_models/watchlist';
import { ToastService } from 'src/app/_services/toast.service';
import { RecordName } from 'src/app/_models/recordName';
import { WatchlistService } from 'src/app/_services/watchlist.service';
import { UserService } from 'src/app/_services/user.service';
import { FriendshipService } from 'src/app/_services/friendship.service';

@Component({
  selector: 'app-user-view',
  templateUrl: './user-detail.component.html',
  styleUrls: ['./user-detail.component.css']
})
export class UserDetailComponent implements OnInit {

	loggedUserId: any;
	user: User;
	watchlists: Watchlist[];
	friendshipStatus: RecordName;

	constructor(private userService: UserService, private friendshipService: FriendshipService, private route: ActivatedRoute, private toast: ToastService, private watchlistService: WatchlistService) { }

	ngOnInit() {
		this.loggedUserId = localStorage.getItem("filmunity-userId");

		let userId = this.route.snapshot.params['id'];
		this.getUser(userId);
		this.getFriendshipStatus(userId);
	}	

	getUser(id: any) {
		this.userService.getUser(id).subscribe((user) => {
			this.user = user;
			this.getWatchlist(user.id);
		});
	}

	getFriendshipStatus(userId: any) {
		if (!this.loggedUserId)
			return;
		this.friendshipService.getFriendshipStatus(userId).subscribe((friendshipStatus) => {
			this.friendshipStatus = friendshipStatus;
		});
	}

	getWatchlist(userId: any) {
		this.watchlistService.getWatchlistByFilter(userId, null, 1, 999).subscribe((watchlists) => {
			this.watchlists = watchlists.result;
		});
	}

	sendFriendRequest(userId) {
		this.friendshipService.sendFriendRequest(userId).subscribe(() => {
			this.toast.success("Friend request successfully sent!");
			this.getFriendshipStatus(userId);
		}, () => {
			this.toast.error("Failed to send a friend request!");
		});
	}

}
