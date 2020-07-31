import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { AgmCoreModule } from '@agm/core';
import { SlickCarouselModule } from 'ngx-slick-carousel';
import { NouisliderModule } from 'ng2-nouislider';

import { ListWithSidebarComponent } from './ListWithSidebar/ListWithSidebar.component';
import { ListingDetailOneComponent } from './ListingDetailOne/ListingDetailOne.component';
import { GallerySliderComponent } from '../globalFrontendComponents/GallerySlider/GallerySlider.component';
import { SidebarLayoutOneComponent } from './SidebarLayoutOne/SidebarLayoutOne.component';
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
		ListWithSidebarComponent,
		ListingDetailOneComponent,
		GallerySliderComponent,
		SidebarLayoutOneComponent,
		WatchlistListComponent,
		WatchlistViewComponent,
		UsersListComponent,
		UserViewComponent
	]
})

export class ListingModule { }
