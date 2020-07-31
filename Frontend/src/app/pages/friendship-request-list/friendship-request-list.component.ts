import { Component, OnInit } from '@angular/core';
import { User } from 'src/app/_models/user';
import { Pagination } from 'src/app/_models/pagination';
import { FilmService } from 'src/app/_services/film.service';
import { ToastService } from 'src/app/_services/toast.service';

@Component({
  selector: 'app-friendship-request-list',
  templateUrl: './friendship-request-list.component.html',
  styleUrls: ['./friendship-request-list.component.css']
})
export class FriendshipRequestListComponent implements OnInit {

	friendshipRequests: User[];

	constructor(private filmService: FilmService, private toast: ToastService) { }

	ngOnInit() {
		this.loadFriendshipRequests();
	}

	loadFriendshipRequests() {		
		this.filmService.getFriendshipRequests().subscribe((friendshipRequests) => {
			this.friendshipRequests = friendshipRequests;
		});
	}

	acceptFriendshipRequest(userId: any) {
		this.filmService.acceptFriendshipRequest(userId).subscribe(() => {
			this.loadFriendshipRequests();
			this.toast.success("Friendship request accepted!");
		}, () => {
			this.toast.error("Failed to accept!");
		});
	}

	declineFriendshipRequest(userId: any) {
		this.filmService.declineFriendshipRequest(userId).subscribe(() => {
			this.loadFriendshipRequests();
			this.toast.success("Friendship request declined!");
		}, () => {
			this.toast.error("Failed to decline!");
		});
	}

}
