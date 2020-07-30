import { Routes } from '@angular/router';

import { ListWithSidebarComponent } from './ListWithSidebar/ListWithSidebar.component';
import { ListFullWidthComponent } from './ListFullWidth/ListFullWidth.component';
import { ListFullWidthMapComponent } from './ListFullWidthMap/ListFullWidthMap.component';

import { GridWithSidebarComponent } from './GridWithSidebar/GridWithSidebar.component';
import { GridFullWidthComponent } from './GridFullWidth/GridFullWidth.component';
import { GridFullWidthMapComponent } from './GridFullWidthMap/GridFullWidthMap.component';

import { HalfScreenMapListComponent } from './HalfScreenMapList/HalfScreenMapList.component';
import { HalfScreenMapGridComponent } from './HalfScreenMapGrid/HalfScreenMapGrid.component';

import { ListingDetailOneComponent } from './ListingDetailOne/ListingDetailOne.component';
import { ListingDetailTwoComponent } from './ListingDetailTwo/ListingDetailTwo.component';
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
		path: 'list/full-width',
		component: ListFullWidthComponent
	},
	{
		path: 'list/full-width-map',
		component: ListFullWidthMapComponent
	},
	{
		path: 'grid/with-sidebar',
		component: GridWithSidebarComponent
	},
	{
		path: 'grid/full-width',
		component: GridFullWidthComponent
	},
	{
		path: 'grid/full-width-map',
		component: GridFullWidthMapComponent
	},
	{
		path: 'half-map/list',
		component: HalfScreenMapListComponent
	},
	{
		path: 'half-map/grid',
		component: HalfScreenMapGridComponent
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
		path: 'detail/version2',
		component: ListingDetailTwoComponent
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
