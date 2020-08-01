import { Component, OnInit } from '@angular/core';
import { FilmService } from 'src/app/_services/film.service';
import { ToastService } from 'src/app/_services/toast.service';
import { Pagination } from 'src/app/_models/pagination';
import { User } from 'src/app/_models/user';

@Component({
  selector: 'app-users-list',
  templateUrl: './users-list.component.html',
  styleUrls: ['./users-list.component.css']
})
export class UsersListComponent implements OnInit {

	searchTxt: any;
	loggedUserId: any;
	users: User[];
	pagination: Pagination;
	pageNumber: any = 1;

	constructor(private filmService: FilmService) { }

	ngOnInit() {
		this.loadUsers();
	}

	loadUsers() {		
		this.filmService.getUsersByFilter(this.searchTxt, this.pageNumber, 4).subscribe((users) => {
			this.pagination = users.pagination;
			this.users = users.result;
		});
	}

	changePage(pageNumber: number) {
		this.pageNumber = pageNumber;
		this.loadUsers();
	}

	filter() {
		this.pageNumber = 1;
		this.loadUsers();
	}

}
