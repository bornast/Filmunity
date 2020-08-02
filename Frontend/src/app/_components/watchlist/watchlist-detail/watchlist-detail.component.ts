import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Watchlist } from 'src/app/_models/watchlist';
import { WatchlistService } from 'src/app/_services/watchlist.service';

@Component({
  selector: 'app-watchlist-view',
  templateUrl: './watchlist-detail.component.html',
  styleUrls: ['./watchlist-detail.component.css']
})
export class WatchlistDetailComponent implements OnInit {

	gallerySlider: any;
	watchlist: Watchlist;

	constructor(private watchlistService: WatchlistService,  private route: ActivatedRoute) { }

	ngOnInit() {
		this.getWatchlist(this.route.snapshot.params['id']);
	}

	getWatchlist(id: any) {
		this.watchlistService.getWatchlist(id).subscribe((watchlist) => {
			this.watchlist = watchlist;
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
