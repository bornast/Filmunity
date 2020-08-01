import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { AgmCoreModule } from '@agm/core';
import { SlickCarouselModule } from 'ngx-slick-carousel';
import { NouisliderModule } from 'ng2-nouislider';

import { FilmListComponent } from './film-list/film-list.component';
import { FilmDetail } from './film-detail/film-detail.component';
import { GallerySliderComponent } from '../gallery-slider/gallery-slider.component';
import { FilmFiltersComponent } from './film-filters/film-filters.component';
import { RatingModule } from 'ng-starrating';
import { ListingRoutes } from './listing.routing';
import { GlobalModule } from '../globalFrontendComponents/global.module';
import { FormsModule } from '@angular/forms';
import { WatchlistListComponent } from './watchlist-list/watchlist-list.component';
import { WatchlistViewComponent } from './watchlist-view/watchlist-view.component';
import { UsersListComponent } from './users-list/users-list.component';
import { UserViewComponent } from './user-view/user-view.component';


@NgModule({
	imports: [
		CommonModule,
		FormsModule,
		GlobalModule,
		SlickCarouselModule,
		NouisliderModule,
		RatingModule,
		AgmCoreModule.forRoot({ apiKey: 'AIzaSyBtdO5k6CRntAMJCF-H5uZjTCoSGX95cdk' }),
		RouterModule.forChild(ListingRoutes),
	],
	declarations: [
		FilmListComponent,
		FilmDetail,
		GallerySliderComponent,
		FilmFiltersComponent,
		WatchlistListComponent,
		WatchlistViewComponent,
		UsersListComponent,
		UserViewComponent
	]
})

export class ListingModule { }
