import { Component, OnInit } from '@angular/core';
import { ENTITYTYPE } from 'src/app/_constants/entityTypeConst';
import { CRUDACTION } from 'src/app/_constants/crudActionConst';
import { User } from 'src/app/_models/user';
import { RecordName } from 'src/app/_models/recordName';
import { ActivatedRoute } from '@angular/router';
import { ToastService } from 'src/app/_services/toast.service';
import { FilmService } from 'src/app/_services/film.service';
import { ROLE } from 'src/app/_constants/roleConst';
import { UserService } from 'src/app/_services/user.service';

@Component({
  selector: 'app-user-editor',
  templateUrl: './user-editor.component.html',
  styleUrls: ['./user-editor.component.css']
})
export class AdminUserEditorComponent implements OnInit {

	entityTypeId: any = ENTITYTYPE.user;
	user: User;
	userToSave: any = {
		roleIds: [],
		photos: []
	};

	roles: RecordName[];	

	constructor(private userService: UserService, private toast: ToastService, private route: ActivatedRoute) { }

	ngOnInit() {
		let id = this.route.snapshot.params['id'];
		if (id) {
			this.getUser(id);			
		}
	}

	getUser(id: any) {
		this.userService.getUser(id).subscribe((user) => {
			this.user = user;
			this.loadData();
		});
	}

	save() {
		this.userService.updateUser(this.user.id, this.userToSave).subscribe((user) => {
			this.toast.success("Successfully updated!");
			this.getUser(user["id"]);
		}, (error) => {
			this.toast.error("Failed to update!");
		});		
	}

	loadData() { 
		this.prepareSelectedData();
		this.loadRoles();
	}

	private prepareSelectedData() {
		this.userToSave.firstName = this.user.firstName;
		this.userToSave.lastName = this.user.lastName;		
		this.userToSave.interests = this.user.interests;
		this.user.roles.forEach(role => {
			this.userToSave.roleIds.push(role.id);
		});
		this.userToSave.photos = this.user.photos;
	}

	private loadRoles() {
		this.roles = [
			{
				id: ROLE.admin, name: "Admin"
			},
			{
				id: ROLE.moderator, name: "Moderator"
			},
			{
				id: ROLE.user, name: "User"
			}
		];
	}

}
