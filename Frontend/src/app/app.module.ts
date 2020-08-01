import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { DropzoneModule } from 'ngx-dropzone-wrapper';
import { DROPZONE_CONFIG } from 'ngx-dropzone-wrapper';
import { DropzoneConfigInterface } from 'ngx-dropzone-wrapper';

import { AppComponent } from './app.component';
import { AppRoutes } from './app.routing';



import { ToastrModule } from 'ngx-toastr';
import { ErrorInterceptorProvider } from './_services/error.interceptor';
import { JwtModule } from '@auth0/angular-jwt';
import { AdminHeaderComponent } from './_components/admin/header/header.component';
import { AdminSidebarComponent } from './_components/admin/sidebar/sidebar.component';
import { AdminMenuItems } from './_components/admin/header/admin-menu-items';
import { AdminPanelLayoutComponent } from './_components/layouts/admin-panel/admin-panel-layout.component';
import { FrontendPanelLayoutComponent } from './_components/layouts/frontend-panel/frontend-panel.component';
import { HeaderComponent } from './_components/common/header/header.component';
import { FooterComponent } from './_components/common/footer/footer.component';
import { MenuComponent } from './_components/common/menu/menu.component';
import { MenuItems } from './_components/common/menu/menu-items';
import { AuthLayoutComponent } from './_components/layouts/auth/auth-layout.component';
import { HomeComponent } from './_components/common/home/home.component';
import { RecentWatchlistsComponent } from './_components/watchlist/recent-watchlists/recent-watchlists.component';
import { PopularFilmsComponent } from './_components/film/popular-films/popular-films.component';
import { PaginationComponent } from './_components/common/pagination/pagination.component';
import { BannerComponent } from './_components/common/banner/banner.component';
import { PhotoEditorComponent } from './_components/common/photo-editor/photo-editor.component';
import { CommonModule } from '@angular/common';
import { SlickCarouselModule } from 'ngx-slick-carousel';
import { FileUploadModule } from 'ng2-file-upload';
import { AdminFilmListComponent } from './_components/admin/film/film-list/film-list.component';
import { AdminFilmEditorComponent } from './_components/admin/film/film-editor/film-editor.component';
import { LoginComponent } from './_components/auth/login/login.component';
import { RegisterComponent } from './_components/auth/register/register.component';
import { AdminParticipantListComponent } from './_components/admin/participant/participant-list/participant-list.component';
import { AdminParticipantEditorComponent } from './_components/admin/participant/participant-editor/participant-editor.component';
import { AdminUserListComponent } from './_components/admin/user/user-list/user-list.component';
import { AdminUserEditorComponent } from './_components/admin/user/user-editor/user-editor.component';
import { FilmListComponent } from './_components/film/film-list/film-list.component';
import { FilmDetail } from './_components/film/film-detail/film-detail.component';
import { GallerySliderComponent } from './_components/common/gallery-slider/gallery-slider.component';
import { FilmFiltersComponent } from './_components/film/film-filters/film-filters.component';
import { WatchlistListComponent } from './_components/watchlist/watchlist-list/watchlist-list.component';
import { WatchlistDetailComponent } from './_components/watchlist/watchlist-detail/watchlist-detail.component';
import { UsersListComponent } from './_components/user/users-list/users-list.component';
import { UserDetailComponent } from './_components/user/user-detail/user-detail.component';
import { NouisliderModule } from 'ng2-nouislider';
import { RatingModule } from 'ng-starrating';
import { MyProfileComponent } from './_components/user/my-profile/my-profile.component';
import { WatchlistEditorComponent } from './_components/watchlist/watchlist-editor/watchlist-editor.component';
import { UserWatchlistsComponent } from './_components/user/user-watchlists/user-watchlists.component';
import { FriendshipRequestListComponent } from './_components/friendship/friendship-request-list/friendship-request-list.component';
import { FriendsListComponent } from './_components/friendship/friends-list/friends-list.component';

export function tokenGetter() {
	return localStorage.getItem('filmunity-token');
}

const DEFAULT_DROPZONE_CONFIG: DropzoneConfigInterface = {
	// Change this to your upload POST address:
	url: 'https://httpbin.org/post',
	maxFilesize: 50,
	acceptedFiles: 'image/*'
};

@NgModule({
	declarations: [
		AppComponent,
		AdminPanelLayoutComponent,
		FrontendPanelLayoutComponent,
		AuthLayoutComponent,
		HeaderComponent,
		FooterComponent,
		MenuComponent,
		AdminHeaderComponent,
		AdminSidebarComponent,
		HomeComponent,
		RecentWatchlistsComponent,
		PopularFilmsComponent,
		PaginationComponent,
		BannerComponent,
		PhotoEditorComponent,
		AdminFilmListComponent,
		AdminFilmEditorComponent,
		AdminParticipantListComponent,
		AdminParticipantEditorComponent,
		AdminUserListComponent,
		AdminUserEditorComponent,
		LoginComponent,
		RegisterComponent,
		FilmListComponent,
		FilmDetail,
		GallerySliderComponent,
		FilmFiltersComponent,
		WatchlistListComponent,
		WatchlistDetailComponent,
		UsersListComponent,
		UserDetailComponent,
		MyProfileComponent,
		WatchlistEditorComponent,
		UserWatchlistsComponent,
		FriendshipRequestListComponent,
		FriendsListComponent
	],
	imports: [
		BrowserModule,
		FormsModule,
		CommonModule,
		SlickCarouselModule,
		FileUploadModule,
		BrowserAnimationsModule,
		NouisliderModule,
		RatingModule,
		ToastrModule.forRoot({
			timeOut: 8000,
			positionClass: 'toast-top-right'
		}),
		DropzoneModule,
		RouterModule.forRoot(AppRoutes, { scrollPositionRestoration: 'enabled' }),
		HttpClientModule,
		JwtModule.forRoot({
			config: {
				tokenGetter: tokenGetter,
				whitelistedDomains: ['localhost:5000'], // to what endpoits do we send authorization headers
				blacklistedRoutes: ['localhost:5000/api/auth'] // to what endpoints do we not send authorization headers
			}
		})
	],
	providers: [
		MenuItems,
		AdminMenuItems,
		{
			provide: DROPZONE_CONFIG,
			useValue: DEFAULT_DROPZONE_CONFIG
		},
		ErrorInterceptorProvider
	],
	bootstrap: [AppComponent]
})
export class AppModule { }
