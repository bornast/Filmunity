import { Component, OnInit, AfterViewInit, ViewEncapsulation } from '@angular/core';
import { DropzoneComponent , DropzoneDirective,
   DropzoneConfigInterface } from 'ngx-dropzone-wrapper';
import { ENTITYTYPE } from 'src/app/_constants/entityTypeConst';
import { User } from 'src/app/_models/user';
import { RecordName } from 'src/app/_models/recordName';
import { FilmService } from 'src/app/_services/film.service';
import { ToastService } from 'src/app/_services/toast.service';
import { ActivatedRoute } from '@angular/router';
import { ROLE } from 'src/app/_constants/roleConst';
declare var $ : any;

@Component({
  selector: 'add-listing',
  templateUrl: './AddListing.component.html',
  styleUrls: ['./AddListing.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class AddListingComponent implements OnInit{

	entityTypeId: any = ENTITYTYPE.user;
	user: User;
	userToSave: any = {
		roleIds: [],
		photos: []
	};

	roles: RecordName[];	

	constructor(private filmService: FilmService, private toast: ToastService, private route: ActivatedRoute) { }

	ngOnInit() {
		this.getUser(localStorage.getItem("filmunity-userId"));
	}

	getUser(id: any) {
		this.filmService.getUser(id).subscribe((user) => {
			this.user = user;
			this.loadData();
		});
	}

	save() {
		this.filmService.updateUser(this.user.id, this.userToSave).subscribe((user) => {
			this.toast.success("Successfully updated!");
			this.getUser(user["id"]);
		}, (error) => {
			this.toast.error("Failed to update!");
			console.log("error is ", error);
		});		
	}

	loadData() { 
		this.prepareSelectedData();
		this.loadRoles();
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

	private prepareSelectedData() {
		this.userToSave.firstName = this.user.firstName;
		this.userToSave.lastName = this.user.lastName;
		this.userToSave.interests = this.user.interests;
		this.userToSave.roleIds = [];
		this.user.roles.forEach(role => {
			this.userToSave.roleIds.push(role.id);
		});
		this.userToSave.photos = this.user.photos;
	}
}
