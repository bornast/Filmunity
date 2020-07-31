import { Routes } from '@angular/router';

import { AddListingComponent } from './AddListing/AddListing.component';
import { UserWatchlistsComponent } from './user-watchlists/user-watchlists.component';
import { FriendshipRequestListComponent } from './friendship-request-list/friendship-request-list.component';
import { FriendsListComponent } from './friends-list/friends-list.component';

export const PagesRoutes: Routes = [
	{
		path: 'add-listing',
		component: AddListingComponent
	},
	{
		path: 'user-watchlists',
		component: UserWatchlistsComponent
	},
	{
		path: 'friendship-request-list',
		component: FriendshipRequestListComponent
	},
	{
		path: 'friend-list',
		component: FriendsListComponent
	}
];
