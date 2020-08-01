import { Component, OnInit } from '@angular/core';
import { Pagination, PaginatedResult } from 'src/app/_models/pagination';
import { FilmService } from 'src/app/_services/film.service';
import { ActivatedRoute } from '@angular/router';
import { ToastService } from 'src/app/_services/toast.service';
import { Person } from 'src/app/_models/person';
import { User } from 'src/app/_models/user';

@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.css']
})
export class AdminUserListComponent implements OnInit {

	usersForList: any[];
	pagination: Pagination;
	pageNumber: any = 1;
	searchTxt: string;

	constructor(private filmService: FilmService, private route: ActivatedRoute, private toast: ToastService) { }

	ngOnInit() {
		this.loadUsers();
	}

	loadUsers() {
		this.filmService.getUsersByFilter(this.searchTxt, this.pageNumber, 4).subscribe((users) => {
			this.pagination = users.pagination;
			this.usersForList = this.transformUserForList(users);
		});
	}

	private transformUserForList(users: PaginatedResult<User[]>): any[] {

		let usersForList = [];

		users.result.forEach(user => {
			let userForList = {
				id: user.id,
				name: user.firstName + " " + user.lastName,
				username: user.username,
				interests: user.interests,
				image: user.mainPhoto != null ? user.mainPhoto.url : ""
			};

			usersForList.push(userForList);
		});

		return usersForList;
	}

	changePage(pageNumber: number) {
		this.pageNumber = pageNumber;
		this.loadUsers();
	}

	delete(id: any) {
		if (confirm("Are you sure to delete this record")) {
			this.filmService.deleteUser(id).subscribe(() => {
				this.pageNumber = 1;
				this.loadUsers();
				this.toast.success("Successfully delete!");
			}, () => {
				this.toast.error("Failed to delete!");
			});
		}
	}

	filter() {
		this.pageNumber = 1;
		this.loadUsers();
	}

}
