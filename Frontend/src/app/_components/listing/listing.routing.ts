import { Routes } from '@angular/router';

import { FilmListComponent } from './film-list/film-list.component';
import { FilmDetail } from './film-detail/film-detail.component';
import { WatchlistEditorComponent } from '../pages/watchlist-editor/watchlist-editor.component';
import { WatchlistListComponent } from './watchlist-list/watchlist-list.component';
import { WatchlistViewComponent } from './watchlist-view/watchlist-view.component';
import { UsersListComponent } from './users-list/users-list.component';
import { UserViewComponent } from './user-view/user-view.component';

export const ListingRoutes: Routes = [
	{
		path: 'list/with-sidebar',
		component: FilmListComponent
	},	
	{
		path: 'film/:id',
		component: FilmDetail
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
