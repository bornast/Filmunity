import { Component, OnInit, AfterViewInit, ViewEncapsulation } from '@angular/core';
import { FilmService } from 'src/app/_services/film.service';
import { Film } from 'src/app/_models/film';
import { FILMTYPE } from 'src/app/_constants/filmTypeConst';
import { PaginatedResult } from 'src/app/_models/pagination';
import { Watchlist } from 'src/app/_models/watchlist';
import { WatchlistService } from 'src/app/_services/watchlist.service';

@Component({
  selector: 'home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
  encapsulation: ViewEncapsulation.None
})
export class HomeComponent implements OnInit{

	moviesForCarousel: any[];
	tvShowsForCarousel: any[];
	watchlistsForCarousel: any[];

	constructor(private filmService: FilmService, private watchlistService: WatchlistService) {}

	ngOnInit() {
		this.filmService.getTopRatedFilms(FILMTYPE.movie, 5).subscribe((movies) => {
			this.moviesForCarousel = this.transformFilmForCarousel(movies);
		});

		this.filmService.getTopRatedFilms(FILMTYPE.tvShow, 5).subscribe((tvShows) => {
			this.tvShowsForCarousel = this.transformFilmForCarousel(tvShows);
		});

		this.watchlistService.getWatchlistByFilter(null, null, 1, 3).subscribe((watchlists) => {
			this.watchlistsForCarousel = this.transformWatchlistForCarousel(watchlists.result);
		});
	}

	private transformWatchlistForCarousel(watchlists: Watchlist[]): any[] {

		let watchlistsForCarousel = [];

		watchlists.forEach(watchlist => {

			let watchlistForCarousel = {
				id: watchlist.id,
				title: watchlist.title,
				desc : watchlist.description,
				image: watchlist.mainPhoto != null ? watchlist.mainPhoto.url : null,
			};

			watchlistsForCarousel.push(watchlistForCarousel)

		});

		return watchlistsForCarousel;
	}

	private transformFilmForCarousel(films: Film[]): any[] {

		let filmsForCarousel = [];

		films.forEach(film => {

			let filmForCarousel = {
				id: film.id,
				title: film.title,
				subTitle : film.rating > 0 ? 'Rating: ' + film.rating : 'Rating: N/A',
				image: film.mainPhoto != null ? film.mainPhoto.url : null,
				isWatched: film.isWatched
			};

			filmsForCarousel.push(filmForCarousel)

		});

		return filmsForCarousel;
	}

}
