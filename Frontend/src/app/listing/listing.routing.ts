import { Routes } from '@angular/router';

import { ListWithSidebarComponent } from './ListWithSidebar/ListWithSidebar.component';
import { ListingDetailOneComponent } from './ListingDetailOne/ListingDetailOne.component';
import { WatchlistEditorComponent } from '../pages/watchlist-editor/watchlist-editor.component';
import { WatchlistListComponent } from './watchlist-list/watchlist-list.component';
import { WatchlistViewComponent } from './watchlist-view/watchlist-view.component';
import { UsersListComponent } from './users-list/users-list.component';
import { UserViewComponent } from './user-view/user-view.component';

export const ListingRoutes: Routes = [
	{
		path: 'list/with-sidebar',
		component: ListWithSidebarComponent
	},	
	{
		path: 'film/:id',
		component: ListingDetailOneComponent
	},
	{
		path: 'watchlist',
		component: WatchlistEditorComponent
	},
	{
		path: 'watchlist/:id',
		component: WatchlistEditorComponent
	},
	{
		path: 'list/watchlist-list',
		component: WatchlistListComponent
	},
	{
		path: 'watchlist-view/:id',
		component: WatchlistViewComponent
	},
	{
		path: 'list/user-list',
		component: UsersListComponent
	},
	{
		path: 'user-view/:id',
		component: UserViewComponent
	}
];
