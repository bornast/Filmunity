import { Component, OnInit } from '@angular/core';
import { CRUDACTION } from 'src/app/_constants/crudActionConst';
import { Person } from 'src/app/_models/person';
import { RecordName } from 'src/app/_models/recordName';
import { FilmService } from 'src/app/_services/film.service';
import { ToastService } from 'src/app/_services/toast.service';
import { ActivatedRoute } from '@angular/router';
import { GENDER } from 'src/app/_constants/genderConst';
import { ENTITYTYPE } from 'src/app/_constants/entityTypeConst';
import { FilmParticipantService } from 'src/app/_services/film-participant.service';

@Component({
  selector: 'admin-participant-editor',
  templateUrl: './participant-editor.component.html',
  styleUrls: ['./participant-editor.component.css']
})
export class AdminParticipantEditorComponent implements OnInit {

	entityTypeId: any = ENTITYTYPE.person;
	crudAction: any = CRUDACTION.create;
	participant: Person;
	participantToSave: any = {
		photos: []
	};

	genders: RecordName[];	

	constructor(private filmParticipantService: FilmParticipantService, private toast: ToastService, private route: ActivatedRoute) { }

	ngOnInit() {
		let id = this.route.snapshot.params['id'];
		if (id) {
			this.crudAction = CRUDACTION.update;
			this.getParticipant(id);			
		}
		else {
			this.loadData();
		}		
	}

	getParticipant(id: any) {
		this.filmParticipantService.getParticipant(id).subscribe((participant) => {
			this.participant = participant;
			this.loadData();
		});
	}

	save() {
		if (this.crudAction == CRUDACTION.create) {
			this.filmParticipantService.createParticipant(this.participantToSave).subscribe((participant) => {
				this.toast.success("Successfully created!");
				this.crudAction = CRUDACTION.update;
				this.getParticipant(participant["id"]);
			}, (error) => {
				this.toast.error("Failed to create!");
				console.log("error is ", error);
			});
		}
		else {
			this.filmParticipantService.updateParticipant(this.participant.id, this.participantToSave).subscribe((participant) => {
				this.toast.success("Successfully updated!");
				this.getParticipant(participant["id"]);
			}, (error) => {
				this.toast.error("Failed to update!");
				console.log("error is ", error);
			});
		}		
	}

	loadData() {
		if (this.crudAction == CRUDACTION.update) 
			this.prepareSelectedData();
		this.loadGenders();
	}

	private prepareSelectedData() {
		this.participantToSave.firstName = this.participant.firstName;
		this.participantToSave.lastName = this.participant.lastName;
		this.participantToSave.dateOfBirth = "";
		if (this.participant.dateOfBirth) {
			let date = new Date(this.participant.dateOfBirth);
			let year = date.getFullYear();
			let month = (date.getMonth() + 1 < 10 ? '0' + (date.getMonth() + 1) : (date.getMonth() + 1));
			let day = (date.getDate() < 10 ? '0' + date.getDate() : date.getDate());
			this.participantToSave.dateOfBirth = year + "-" + month + "-" + day;
		};
		this.participantToSave.description = this.participant.description;
		this.participantToSave.genderId = this.participant.genderId;
		this.participantToSave.photos = this.participant.photos;
	}

	private loadGenders() {
		this.genders = [
			{
				id: GENDER.male, name: "Male"
			},
			{
				id: GENDER.female, name: "Female"
			}
		];
	}	

}
