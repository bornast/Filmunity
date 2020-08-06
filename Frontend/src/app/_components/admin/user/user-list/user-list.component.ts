import { Component, OnInit } from '@angular/core';
import { Pagination, PaginatedResult } from 'src/app/_models/pagination';
import { FilmService } from 'src/app/_services/film.service';
import { ActivatedRoute } from '@angular/router';
import { ToastService } from 'src/app/_services/toast.service';
import { Person } from 'src/app/_models/person';
import { User } from 'src/app/_models/user';
import { UserService } from 'src/app/_services/user.service';

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

	constructor(private userService: UserService, private route: ActivatedRoute, private toast: ToastService) { }

	ngOnInit() {
		this.loadUsers();
	}

	loadUsers() {
		this.userService.getUsersByFilter(this.searchTxt, this.pageNumber, 4).subscribe((users) => {
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
				image: user.mainPhoto != null ? user.mainPhoto.url : "/assets/images/no-image-user.png"
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
		if (confirm("Are you sure you want to delete this record?")) {
			this.userService.deleteUser(id).subscribe(() => {
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
