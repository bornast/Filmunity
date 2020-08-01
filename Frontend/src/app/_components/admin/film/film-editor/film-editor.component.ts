import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { FilmService } from 'src/app/_services/film.service';
import { RecordName } from 'src/app/_models/recordName';
import { FILMTYPE } from 'src/app/_constants/filmTypeConst';
import { ToastService } from 'src/app/_services/toast.service';
import { CRUDACTION } from 'src/app/_constants/crudActionConst';
import { Film } from 'src/app/_models/film';
import { ActivatedRoute } from '@angular/router';
import { ENTITYTYPE } from 'src/app/_constants/entityTypeConst';

@Component({
	selector: 'admin-film-editor',
	templateUrl: './film-editor.component.html',
	styleUrls: ['./film-editor.component.scss'],
	encapsulation: ViewEncapsulation.None
})
export class FilmEditorComponent implements OnInit {

	entityTypeId: any = ENTITYTYPE.film;
	crudAction: any = CRUDACTION.create;
	film: Film;
	filmToSave: any = {
		genreIds: [],
		participantsRoles: [],
		photos: []
	};

	types: RecordName[];
	countries: RecordName[];
	languages: RecordName[];
	genres: RecordName[];
	persons: RecordName[];
	filmRoles: RecordName[];	

	constructor(private filmService: FilmService, private toast: ToastService, private route: ActivatedRoute) { }

	ngOnInit() {
		let id = this.route.snapshot.params['id'];
		if (id) {
			this.crudAction = CRUDACTION.update;
			this.getFilm(id);			
		}
		else {
			this.loadData();
		}		
	}

	getFilm(id: any) {
		this.filmService.getFilm(id).subscribe((film) => {
			this.film = film;
			this.loadData();
		});
	}

	save() {
		if (this.crudAction == CRUDACTION.create) {
			this.filmService.createFilm(this.filmToSave).subscribe((film) => {
				this.toast.success("Successfully created!");
				this.crudAction = CRUDACTION.update;
				this.getFilm(film["id"]);
			}, (error) => {
				this.toast.error("Failed to create!");
				console.log("error is ", error);
			});
		}
		else {
			this.filmService.updateFilm(this.film.id, this.filmToSave).subscribe((film) => {
				this.toast.success("Successfully updated!");
				this.getFilm(film["id"]);
			}, (error) => {
				this.toast.error("Failed to update!");
				console.log("error is ", error);
			});
		}		
	}

	loadData() {
		if (this.crudAction == CRUDACTION.update) 
			this.prepareSelectedData();
		this.loadTypes();
		this.loadCountires();
		this.loadLanguages();
		this.loadGenres();
		this.loadParticipants();
	}

	private prepareSelectedData() {
		this.filmToSave.title = this.film.title;
		this.filmToSave.description = this.film.description;
		this.filmToSave.year = this.film.year;
		this.filmToSave.duration = this.film.duration;
		this.filmToSave.typeId = this.film.type.id;
		this.filmToSave.countryId = this.film.country.id;
		this.filmToSave.languageId = this.film.language.id;
		this.film.genres.forEach(genre => {
			this.filmToSave.genreIds.push(genre.id);
		});
		this.filmToSave.participantsRoles = [];
		this.film.participants.forEach(participant => {
			let participantToAdd = {
				participantId: participant.participant.id,
				roleId: participant.role.id
			};
			this.filmToSave.participantsRoles.push(participantToAdd);
		});
		this.filmToSave.photos = this.film.photos;
	}

	private loadTypes() {
		this.types = [
			{
				id: FILMTYPE.movie, name: "Movie"
			},
			{
				id: FILMTYPE.tvShow, name: "Tv Show"
			}
		];
	}

	private loadCountires() {
		this.filmService.getCountries().subscribe((countries) => {
			this.countries = countries;
		});
	}

	private loadLanguages() {
		this.filmService.getLanguages().subscribe((languages) => {
			this.languages = languages;
		});
	}

	private loadGenres() {
		this.filmService.getGenres().subscribe((genres) => {
			this.genres = genres;
		});
	}

	private loadParticipants() {
		this.filmService.getPersons().subscribe((persons) => {
			this.persons = persons;
		});
		this.filmService.getFilmRole().subscribe((filmRoles) => {
			this.filmRoles = filmRoles;
		});
		if (this.crudAction == CRUDACTION.create)
			this.addEmptyParticipant();
	}

	updateParticipant(index: number, personIndex: string) {
		this.filmToSave.participantsRoles[index].participantId = this.persons[personIndex].id;
	}

	updateParticipantRole(index: number, roleIndex: string) {
		this.filmToSave.participantsRoles[index].roleId = this.filmRoles[roleIndex].id;
	}

	addEmptyParticipant() {
		let emptyParticipant = {
			participantId: "",
			roleId: ""
		};
		this.filmToSave.participantsRoles.push(emptyParticipant);
	}

	removeParticipant(index: number) {
		this.filmToSave.participantsRoles.splice(index, 1);
	}

	areParticipantsValid() {
		for (let participant of this.filmToSave.participantsRoles) {
			if (!participant.participantId || !participant.roleId) {
				return false;
			}
		}

		return true;
	}

}
