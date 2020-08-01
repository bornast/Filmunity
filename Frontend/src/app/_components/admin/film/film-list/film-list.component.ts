import { Component, OnInit, AfterViewInit, ViewEncapsulation } from '@angular/core';
import { FilmService } from 'src/app/_services/film.service';
import { ActivatedRoute } from '@angular/router';
import { Film } from 'src/app/_models/film';
import { Pagination, PaginatedResult } from 'src/app/_models/pagination';
import { ToastService } from 'src/app/_services/toast.service';

@Component({
	selector: 'admin-film-list',
	templateUrl: './film-list.component.html',
	styleUrls: ['./film-list.component.scss'],
	encapsulation: ViewEncapsulation.None
})
export class FilmListComponent implements OnInit {

	filmsForList: any[];
	filmType: any;
	pagination: Pagination;
	pageNumber: any = 1;
	searchTxt: string;

	constructor(private filmService: FilmService, private route: ActivatedRoute, private toast: ToastService) { }

	ngOnInit() {
		this.loadFilms();
	}

	loadFilms() {
		this.filmService.getFilmsByFilter(this.filmType, null, null, this.searchTxt, this.pageNumber, 4).subscribe((films) => {
			this.pagination = films.pagination;
			this.filmsForList = this.transformFilmForList(films);
		});
	}

	private transformFilmForList(films: PaginatedResult<Film[]>): any[] {

		let filmsForList = [];

		films.result.forEach(film => {
			let filmForList = {
				id: film.id,
				genre: film.genres,
				title: film.title,
				description: film.description,
				image: film.mainPhoto != null ? film.mainPhoto.url : "",
				rating: film.rating,
				imdbRating: film.imdbRating
			};

			filmsForList.push(filmForList);
		});

		return filmsForList;
	}

	changePage(pageNumber: number) {
		this.pageNumber = pageNumber;
		this.loadFilms();
	}

	delete(id: any) {
		if (confirm("Are you sure to delete this record")) {
			this.filmService.delete(id).subscribe(() => {
				this.pageNumber = 1;
				this.loadFilms();
				this.toast.success("Successfully delete!");
			}, () => {
				this.toast.error("Failed to delete!");
			});
		}
	}

	filter() {
		this.pageNumber = 1;
		this.loadFilms();
	}

}
