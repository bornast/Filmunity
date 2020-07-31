import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { DropzoneModule } from 'ngx-dropzone-wrapper';
import { DROPZONE_CONFIG } from 'ngx-dropzone-wrapper';
import { DropzoneConfigInterface } from 'ngx-dropzone-wrapper';

import { AddListingComponent } from './AddListing/AddListing.component';
import { GlobalModule } from '../globalFrontendComponents/global.module';
import { PagesRoutes } from './pages.routing';
import { FormsModule } from '@angular/forms';
import { WatchlistEditorComponent } from './watchlist-editor/watchlist-editor.component';
import { UserWatchlistsComponent } from './user-watchlists/user-watchlists.component';
import { FriendshipRequestListComponent } from './friendship-request-list/friendship-request-list.component';
import { FriendsListComponent } from './friends-list/friends-list.component';

const DEFAULT_DROPZONE_CONFIG: DropzoneConfigInterface = {
	// Change this to your upload POST address:
	url: 'https://httpbin.org/post',
	maxFilesize: 50,
	acceptedFiles: 'image/*'
};


@NgModule({
	imports: [
		CommonModule,
		GlobalModule,
		DropzoneModule,
		FormsModule,
		RouterModule.forChild(PagesRoutes),
	],
	declarations: [
		AddListingComponent,
		WatchlistEditorComponent,
		UserWatchlistsComponent,
		FriendshipRequestListComponent,
		FriendsListComponent
	],
	providers: [
		{
			provide: DROPZONE_CONFIG,
			useValue: DEFAULT_DROPZONE_CONFIG
		}
	]
})

export class PagesModule { }
