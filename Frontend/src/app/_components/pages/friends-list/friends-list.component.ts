import { Component, OnInit } from '@angular/core';
import { User } from 'src/app/_models/user';
import { FilmService } from 'src/app/_services/film.service';
import { Pagination } from 'src/app/_models/pagination';

@Component({
  selector: 'app-friends-list',
  templateUrl: './friends-list.component.html',
  styleUrls: ['./friends-list.component.css']
})
export class FriendsListComponent implements OnInit {

	friends: User[];
	pagination: Pagination;
	pageNumber: any = 1;

	constructor(private filmService: FilmService) { }

	ngOnInit() {
		this.loadFriends();
	}

	loadFriends() {		
		this.filmService.getAllFriends(this.pageNumber, 4).subscribe((friends) => {

			this.pagination = friends.pagination;
			this.friends = friends.result;
		});
	}

	changePage(pageNumber: number) {
		this.pageNumber = pageNumber;
		this.loadFriends();
	}

}
