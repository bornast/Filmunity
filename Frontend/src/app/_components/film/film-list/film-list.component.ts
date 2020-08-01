import { Component, OnInit, AfterViewInit, ViewEncapsulation, Input } from '@angular/core';
import { FilmService } from 'src/app/_services/film.service';
import { FILMTYPE } from 'src/app/_constants/filmTypeConst';
import { Film } from 'src/app/_models/film';
import { ActivatedRoute } from '@angular/router';
import { PaginatedResult, Pagination } from 'src/app/_models/pagination';

@Component({
	selector: 'list-with-sidebar',
	templateUrl: './film-list.component.html',
	styleUrls: ['./film-list.component.scss'],
	encapsulation: ViewEncapsulation.None
})
export class FilmListComponent implements OnInit {
	
	displayFilterSidebar: boolean;
	filmsForList: any[];
	filters: any = {};
	pagination: Pagination;

	constructor(private filmService: FilmService, private route: ActivatedRoute) { }

	ngOnInit() {
		this.filters.searchTxt = this.route.snapshot.queryParamMap.get("searchTxt");
		this.filters.filmType = this.route.snapshot.queryParamMap.get("filmType") ?? this.route.snapshot.data['filmType'];
		this.displayFilterSidebar = true;		
		this.loadFilms();
	}

	loadFilms() { 
		this.filmService.getFilmsByFilter(this.filters.filmType, this.filters.orderBy, this.filters.genreId, this.filters.searchTxt, this.filters.pageNumber).subscribe((movies) => {
			this.pagination = movies.pagination;
			this.filmsForList = this.transformFilmForList(movies);
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

	filter(filters: any = {}) {
		filters.filmType = this.filters.filmType;
		this.filters = filters;
		this.loadFilms();
	}

	changePage(pageNumber: number) {
		this.filters.pageNumber = pageNumber;
		this.loadFilms();
	}

}
