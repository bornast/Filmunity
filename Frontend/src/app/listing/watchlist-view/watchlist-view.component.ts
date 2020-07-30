import { Component, OnInit } from '@angular/core';
import { FilmService } from 'src/app/_services/film.service';
import { ActivatedRoute } from '@angular/router';
import { Watchlist } from 'src/app/_models/watchlist';

@Component({
  selector: 'app-watchlist-view',
  templateUrl: './watchlist-view.component.html',
  styleUrls: ['./watchlist-view.component.css']
})
export class WatchlistViewComponent implements OnInit {

	gallerySlider: any;
	watchlist: Watchlist;
	reviews: any[];

	constructor(private filmService: FilmService,  private route: ActivatedRoute) { }

	ngOnInit() {
		this.getWatchlist(this.route.snapshot.params['id']);
	}

	getWatchlist(id: any) {
		this.filmService.getWatchlist(id).subscribe((watchlist) => {
			this.watchlist = watchlist;
			console.log("watchlist", watchlist);
			if (!this.gallerySlider)
				this.prepareGallerySlider();
		});
	}

	prepareGallerySlider() {
		this.gallerySlider = [];

		this.watchlist.photos.forEach(photo => {
			let galleryPhoto = {
				image: photo.url
			};
			this.gallerySlider.push(galleryPhoto);
		});
	}	

}
