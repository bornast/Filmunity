import { Component, OnInit } from '@angular/core';
import { ENTITYTYPE } from 'src/app/_constants/entityTypeConst';
import { Watchlist } from 'src/app/_models/watchlist';
import { CRUDACTION } from 'src/app/_constants/crudActionConst';
import { RecordName } from 'src/app/_models/recordName';
import { FilmService } from 'src/app/_services/film.service';
import { ToastService } from 'src/app/_services/toast.service';
import { ActivatedRoute } from '@angular/router';
import { WatchlistService } from 'src/app/_services/watchlist.service';

@Component({
	selector: 'app-watchlist-editor',
	templateUrl: './watchlist-editor.component.html',
	styleUrls: ['./watchlist-editor.component.css']
})
export class WatchlistEditorComponent implements OnInit {

	entityTypeId: any = ENTITYTYPE.watchlist;
	crudAction: any = CRUDACTION.create;
	watchlist: Watchlist;
	watchlistToSave: any = {
		photos: [],
		films: []
	};

	films: RecordName[];

	constructor(private filmService: FilmService, private watchlistService: WatchlistService, private toast: ToastService, private route: ActivatedRoute) { }

	ngOnInit() {
		let id = this.route.snapshot.params['id'];
		if (id) {
			this.crudAction = CRUDACTION.update;
			this.getWatchlist(id);
		}
		else {
			this.loadData();
		}
	}

	getWatchlist(id: any) {
		this.watchlistService.getWatchlist(id).subscribe((watchlist) => {
			this.watchlist = watchlist;
			this.loadData();
		});
	}

	save() {
		if (this.crudAction == CRUDACTION.create) {
			this.watchlistService.createWatchlist(this.watchlistToSave).subscribe((watchlist) => {
				this.toast.success("Successfully created!");
				this.crudAction = CRUDACTION.update;
				this.getWatchlist(watchlist["id"]);
			}, () => {
				this.toast.error("Failed to create!");
			});
		}
		else {
			this.watchlistService.updateWatchlist(this.watchlist.id, this.watchlistToSave).subscribe((watchlist) => {
				this.toast.success("Successfully updated!");
				this.getWatchlist(watchlist["id"]);
			}, () => {
				this.toast.error("Failed to update!");
			});
		}
	}

	loadData() {
		if (this.crudAction == CRUDACTION.update)
			this.prepareSelectedData();
		this.loadFilms();
	}

	private prepareSelectedData() {
		this.watchlistToSave.title = this.watchlist.title;
		this.watchlistToSave.description = this.watchlist.description;

		this.watchlistToSave.films = [];
		let sequence = 1;
		this.watchlist.films.forEach(film => {
			let filmToAdd = {
				sequence: film.sequence,
				filmId: film.id
			};
			this.watchlistToSave.films.push(filmToAdd);
			sequence++;
		});

		this.watchlistToSave.films.sort(this.setFilmOrder);
		this.watchlistToSave.photos = this.watchlist.photos;
	}

	setFilmOrder(a, b) {
		const filmA = a.sequence;
		const filmB = b.sequence;

		let comparison = 0;
		if (filmA > filmB) {
			comparison = 1;
		} else if (filmA < filmB) {
			comparison = -1;
		}
		return comparison;
	}

	private loadFilms() {
		this.filmService.getFilms().subscribe((films) => {
			this.films = films;
		});

		if (this.crudAction == CRUDACTION.create)
			this.addEmptyFilm();
	}

	topReorder(index: number) {
		if (index == 0)
			return;

		var temp = this.watchlistToSave.films[index].filmId;
		this.watchlistToSave.films[index].filmId = this.watchlistToSave.films[index - 1].filmId;
		this.watchlistToSave.films[index - 1].filmId = temp;
	}

	bottomReorder(index: number) {
		if (index >= this.watchlistToSave.films.length - 1)
			return;

		var temp = this.watchlistToSave.films[index].filmId;
		this.watchlistToSave.films[index].filmId = this.watchlistToSave.films[index + 1].filmId;
		this.watchlistToSave.films[index + 1].filmId = temp;
	}

	addEmptyFilm() {
		let emptyFilm = {
			sequence: this.watchlistToSave.films.length + 1,
			filmId: ""
		};
		this.watchlistToSave.films.push(emptyFilm);
	}

	removeFilm(index: number) {
		this.watchlistToSave.films.splice(index, 1);
	}

	areFilmsValid() {
		for (let watchlist of this.watchlistToSave.films) {
			if (!watchlist.sequence || !watchlist.filmId) {
				return false;
			}
		}

		return true;
	}

}
