import { Component, OnInit, AfterViewInit, ViewEncapsulation } from '@angular/core';
import { FilmService } from 'src/app/_services/film.service';
import { ActivatedRoute } from '@angular/router';
import { Film } from 'src/app/_models/film';
import { Pagination, PaginatedResult } from 'src/app/_models/pagination';

@Component({
	selector: 'admin-list',
	templateUrl: './List.component.html',
	styleUrls: ['./List.component.scss'],
	encapsulation: ViewEncapsulation.None
})
export class ListComponent implements OnInit {

	filmsForList: any[];
	filmType: any;
	pagination: Pagination;
	pageNumber: any = 1;
	searchTxt: string;

	constructor(private filmService: FilmService, private route: ActivatedRoute) { }

	ngOnInit() {
		this.filmType = this.route.snapshot.queryParamMap.get("filmType");
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

}
