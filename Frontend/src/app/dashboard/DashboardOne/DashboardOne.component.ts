import { Component, OnInit, AfterViewInit, ViewEncapsulation } from '@angular/core';
import { FilmService } from 'src/app/_services/film.service';
import { Film } from 'src/app/_models/film';
import { FILMTYPE } from 'src/app/_constants/filmTypeConst';

@Component({
  selector: 'dashboard-one',
  templateUrl: './DashboardOne.component.html',
  styleUrls: ['./DashboardOne.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class DashboardOneComponent implements OnInit{

	moviesForCarousel: any[];
	tvShowsForCarousel: any[];

	bannerTitle: string = 'Find Nearby Attractions';
	bannerDesc : string = 'Expolore top-rated attractions, activities and more';
	bannerImage: string = 'assets/images/main-search-background-01.jpg';	                

	recentBlogTitle : string = 'Recent Blog';
	blogs : any = [
							{
								tag   : 'Tips',
								date  : '22 August 2018',
								title : 'Take a Look at Hotels for All Budgets',
								desc  : 'Sed sed tristique nibh iam porta volutpat finibus. Donec in aliquet urneget mattis lorem. Pellentesque pellentesque', 
								image : 'assets/images/post-1.jpg'
							},
							{
								tag   : 'Tips',
								date  : '22 August 2018',
								title : 'The 50 Greatest Street Arts In London',
								desc  : 'Sed sed tristique nibh iam porta volutpat finibus. Donec in aliquet urneget mattis lorem. Pellentesque pellentesque.', 
								image : 'assets/images/post-2.jpg'
							},
							{
								tag   : 'Tips',
								date  : '22 August 2018',
								title : 'The Best Cofee Shops In Sydney Neighborhoods',
								desc  : 'Sed sed tristique nibh iam porta volutpat finibus. Donec in aliquet urneget mattis lorem. Pellentesque pellentesque.', 
								image : 'assets/images/post-3.jpg'
							}
						];

	constructor(private filmService: FilmService) {}

	ngOnInit() {
		this.filmService.getTopRatedFilms(FILMTYPE.movie, 7).subscribe((movies) => {
			this.moviesForCarousel = this.transformFilmForCarousel(movies);
		});

		this.filmService.getTopRatedFilms(FILMTYPE.tvShow, 7).subscribe((movies) => {
			this.tvShowsForCarousel = this.transformFilmForCarousel(movies);
		});
	}

	private transformFilmForCarousel(films: Film[]): any[] {

		let filmsForCarousel = [];

		films.forEach(film => {

			let filmForCarousel = {
				title: film.title,
				subTitle : '--get rating here---',
				image: film.mainPhoto != null ? film.mainPhoto.url : "",
			};

			filmsForCarousel.push(filmForCarousel)

		});

		return filmsForCarousel;
	}

}
