import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AgmCoreModule } from '@agm/core';
import { RouterModule } from '@angular/router';
import { SlickCarouselModule } from 'ngx-slick-carousel';
import { PaginationComponent } from '../globalFrontendComponents/Pagination/Pagination.component';
import { BannerComponent } from './Banner/Banner.component';
import { PopularCategoriesComponent } from './PopuplarCategories/PopularCategories.component';
import { RecentBlogComponent } from './RecentBlog/RecentBlog.component';
import { FormsModule } from '@angular/forms';
import { PhotoEditorComponent } from './photo-editor/photo-editor.component';
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
		PopularCategoriesComponent,
		RecentBlogComponent,
		PhotoEditorComponent		
	],
	exports: [
		PaginationComponent,
		BannerComponent,
		PopularCategoriesComponent,
		RecentBlogComponent,
		PhotoEditorComponent
	]
})

export class GlobalModule { }
