import { Component, OnInit, AfterViewInit, ViewEncapsulation } from '@angular/core';
import { FilmService } from 'src/app/_services/film.service';
import { Film } from 'src/app/_models/film';
import { FILMTYPE } from 'src/app/_constants/filmTypeConst';
import { PaginatedResult } from 'src/app/_models/pagination';
import { Watchlist } from 'src/app/_models/watchlist';

@Component({
  selector: 'dashboard-one',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class HomeComponent implements OnInit{

	moviesForCarousel: any[];
	tvShowsForCarousel: any[];
	watchlistsForCarousel: any[];

	bannerTitle: string = 'Find Nearby Attractions';
	bannerDesc : string = 'Expolore top-rated attractions, activities and more';
	bannerImage: string = 'assets/images/main-search-background-01.jpg';

	recentBlogTitle : string = 'Recent Blog';

	constructor(private filmService: FilmService) {}

	ngOnInit() {
		this.filmService.getTopRatedFilms(FILMTYPE.movie, 7).subscribe((movies) => {
			this.moviesForCarousel = this.transformFilmForCarousel(movies);
		});

		this.filmService.getTopRatedFilms(FILMTYPE.tvShow, 7).subscribe((tvShows) => {
			this.tvShowsForCarousel = this.transformFilmForCarousel(tvShows);
		});

		this.filmService.getWatchlistByFilter(null, null, 1, 3).subscribe((watchlists) => {
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
				image: watchlist.mainPhoto != null ? watchlist.mainPhoto.url : "",
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
				subTitle : film.rating,
				image: film.mainPhoto != null ? film.mainPhoto.url : "",
			};

			filmsForCarousel.push(filmForCarousel)

		});

		return filmsForCarousel;
	}

}
