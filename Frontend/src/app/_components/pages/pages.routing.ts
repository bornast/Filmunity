import { Routes } from '@angular/router';

import { MyProfileComponent } from './my-profile/my-profile.component';
import { UserWatchlistsComponent } from './user-watchlists/user-watchlists.component';
import { FriendshipRequestListComponent } from './friendship-request-list/friendship-request-list.component';
import { FriendsListComponent } from './friends-list/friends-list.component';

export const PagesRoutes: Routes = [
	{
		path: 'add-listing',
		component: MyProfileComponent
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
