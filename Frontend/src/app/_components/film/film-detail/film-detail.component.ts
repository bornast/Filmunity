import { Component, OnInit, AfterViewInit, ViewEncapsulation } from '@angular/core';
import { FilmService } from 'src/app/_services/film.service';
import { ToastService } from 'src/app/_services/toast.service';
import { ActivatedRoute } from '@angular/router';
import { Film } from 'src/app/_models/film';
import { StarRatingComponent } from 'ng-starrating';
import { Pagination } from 'src/app/_models/pagination';
import { UserService } from 'src/app/_services/user.service';

@Component({
	selector: 'list-detail-one',
	templateUrl: './film-detail.component.html',
	styleUrls: ['./film-detail.component.scss'],
	encapsulation: ViewEncapsulation.None
})
export class FilmDetail implements OnInit {

	loggedUserId: any;
	loggedUserRating: any;
	gallerySlider: any;
	film: Film;
	loggedUserComment: string;
	reviews: any[];
	reviewPagination: Pagination;
	reviewPageNumber: any = 1;

	constructor(private filmService: FilmService, private toast: ToastService, private route: ActivatedRoute, private userService: UserService) { }

	ngOnInit() {
		this.loggedUserId = localStorage.getItem("filmunity-userId");
		this.getFilm(this.route.snapshot.params['id']);
	}

	getFilm(id: any) {
		this.filmService.getFilm(id).subscribe((film) => {
			this.film = film;

			if (!this.gallerySlider)
				this.prepareGallerySlider();

			if (this.loggedUserId && !this.loggedUserRating)
				this.getLoggedUserRating();
			
			this.getReviews();
		});
	}

	prepareGallerySlider() {

		if (!this.film.photos || this.film.photos.length == 0)
			return;
		this.gallerySlider = [];

		this.film.photos.forEach(photo => {
			let galleryPhoto = {
				image: photo.url
			};
			this.gallerySlider.push(galleryPhoto);
		});
	}

	comment() {
		let commentObject = {
			filmId: this.film.id,
			comment: this.loggedUserComment
		};

		this.filmService.reviewFilm(commentObject).subscribe(() => {
			this.toast.success("Comment successfully submitted!");
			this.getReviews();
		});
	}

	onRate($event: { oldValue: number, newValue: number, starRating: StarRatingComponent }) {

		if ($event.oldValue > 0) {
			this.filmService.unrateFilm(this.film.id).subscribe(() => {
				this.rateFilm($event.newValue);
			});
		}
		else {
			this.rateFilm($event.newValue);
		}
	}

	rateFilm(rating) {
		this.filmService.rateFilm(this.film.id, { rating: rating }).subscribe(() => {
			this.toast.success("Successfully rated!");
			this.getFilm(this.film.id);
		});
	}

	getLoggedUserRating() {		
		this.userService.getLoggedUserRating(this.film.id).subscribe((response) => {
			this.loggedUserRating = response["rating"];
		});
	}

	getReviews() {	
		this.filmService.getFilmReviews(this.film.id, this.reviewPageNumber, 4).subscribe((reviews) => {
			this.reviewPagination = reviews.pagination;
			this.reviews = reviews.result;
		});
	}

	changeReviewPage(pageNumber: number) {
		this.reviewPageNumber = pageNumber;
		this.getReviews();
	}

}
