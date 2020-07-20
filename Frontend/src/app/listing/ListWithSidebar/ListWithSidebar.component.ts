import { Component, OnInit, AfterViewInit, ViewEncapsulation, Input } from '@angular/core';
import { FilmService } from 'src/app/_services/film.service';
import { FILMTYPE } from 'src/app/_constants/filmTypeConst';
import { Film } from 'src/app/_models/film';
import { ActivatedRoute } from '@angular/router';
import { PaginatedResult, Pagination } from 'src/app/_models/pagination';

@Component({
	selector: 'list-with-sidebar',
	templateUrl: './ListWithSidebar.component.html',
	styleUrls: ['./ListWithSidebar.component.scss'],
	encapsulation: ViewEncapsulation.None
})
export class ListWithSidebarComponent implements OnInit {
	
	displayFilterSidebar: boolean;
	filmsForList: any[];
	filters: any = {};
	pagination: Pagination;

	constructor(private filmService: FilmService, private route: ActivatedRoute) { }

	ngOnInit() {

		this.route.data.subscribe( params => {
			if ('searchTxt' in params) {
			  this.filters.searchTxt = params.searchTxt;
			}
			this.displayFilterSidebar = true;
		  });

		this.loadFilms();
	}

	loadFilms() { 
		this.filmService.getFilmsByFilter(FILMTYPE.movie, this.filters.orderBy, this.filters.genreId, this.filters.searchTxt, this.filters.pageNumber).subscribe((movies) => {
			this.pagination = movies.pagination;
			this.filmsForList = this.transformFilmForList(movies);
		});
	}

	private transformFilmForList(films: PaginatedResult<Film[]>): any[] {

		let filmsForList = [];

		films.result.forEach(film => {
			// TODO: genre should be multiple tags with genre
			// TODO: add both imdb and filmunity rating
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

	filter(filters: {}) {
		this.filters = filters;
		this.loadFilms();
	}

	changePage(pageNumber: number) {
		this.filters.pageNumber = pageNumber;
		this.loadFilms();
	}

}
