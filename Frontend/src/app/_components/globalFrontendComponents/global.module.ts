import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AgmCoreModule } from '@agm/core';
import { RouterModule } from '@angular/router';
import { SlickCarouselModule } from 'ngx-slick-carousel';
import { PaginationComponent } from '../pagination/pagination.component';
import { BannerComponent } from '../banner/banner.component';
import { PopularFilmsComponent } from '../popular-films/popular-films.component';
import { RecentWatchlistsComponent } from '../recent-watchlists/recent-watchlists.component';
import { FormsModule } from '@angular/forms';
import { PhotoEditorComponent } from '../photo-editor/photo-editor.component';
import { FileUploadModule } from 'ng2-file-upload';

@NgModule({
	imports: [
		FormsModule,
		CommonModule,
		RouterModule,
		SlickCarouselModule,
		FileUploadModule,
		AgmCoreModule.forRoot({ apiKey: 'AIzaSyBtdO5k6CRntAMJCF-H5uZjTCoSGX95cdk' })],
	declarations: [
		PaginationComponent,
		BannerComponent,
		PopularFilmsComponent,
		RecentWatchlistsComponent,
		PhotoEditorComponent		
	],
	exports: [
		PaginationComponent,
		BannerComponent,
		PopularFilmsComponent,
		RecentWatchlistsComponent,
		PhotoEditorComponent
	]
})

export class GlobalModule { }
