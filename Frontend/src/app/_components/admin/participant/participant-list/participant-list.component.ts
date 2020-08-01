import { Component, OnInit } from '@angular/core';
import { FilmService } from 'src/app/_services/film.service';
import { ActivatedRoute } from '@angular/router';
import { ToastService } from 'src/app/_services/toast.service';
import { PaginatedResult, Pagination } from 'src/app/_models/pagination';
import { Person } from 'src/app/_models/person';

@Component({
	selector: 'admin-participant-list',
	templateUrl: './participant-list.component.html',
	styleUrls: ['./participant-list.component.css']
})
export class AdminParticipantListComponent implements OnInit {

	participantsForList: any[];
	pagination: Pagination;
	pageNumber: any = 1;
	searchTxt: string;

	constructor(private filmService: FilmService, private route: ActivatedRoute, private toast: ToastService) { }

	ngOnInit() {
		this.loadParticipants();
	}

	loadParticipants() {
		this.filmService.getParticipantsByFilter(this.searchTxt, this.pageNumber, 4).subscribe((participants) => {
			this.pagination = participants.pagination;
			this.participantsForList = this.transformParticipantForList(participants);
		});
	}

	private transformParticipantForList(participants: PaginatedResult<Person[]>): any[] {

		let participantsForList = [];

		participants.result.forEach(participant => {
			let participantForList = {
				id: participant.id,
				name: participant.firstName + " " + participant.lastName,
				description: participant.description,
				image: participant.mainPhoto != null ? participant.mainPhoto.url : ""
			};

			participantsForList.push(participantForList);
		});

		return participantsForList;
	}

	changePage(pageNumber: number) {
		this.pageNumber = pageNumber;
		this.loadParticipants();
	}

	delete(id: any) {
		if (confirm("Are you sure to delete this record")) {
			this.filmService.deleteParticipant(id).subscribe(() => {
				this.pageNumber = 1;
				this.loadParticipants();
				this.toast.success("Successfully delete!");
			}, () => {
				this.toast.error("Failed to delete!");
			});
		}
	}

	filter() {
		this.pageNumber = 1;
		this.loadParticipants();
	}

}
