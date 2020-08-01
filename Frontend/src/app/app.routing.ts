import { Routes } from '@angular/router';


import { AuthGuard } from './_guards/auth.guard';
import { FrontendPanelLayoutComponent } from './_components/layouts/frontend-panel/frontend-panel.component';
import { AdminPanelLayoutComponent } from './_components/layouts/admin-panel/admin-panel-layout.component';
import { AuthLayoutComponent } from './_components/layouts/auth/auth-layout.component';
import { HomeComponent } from './_components/common/home/home.component';
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
import { WatchlistEditorComponent } from './_components/watchlist/watchlist-editor/watchlist-editor.component';
import { WatchlistListComponent } from './_components/watchlist/watchlist-list/watchlist-list.component';
import { WatchlistDetailComponent } from './_components/watchlist/watchlist-detail/watchlist-detail.component';
import { UsersListComponent } from './_components/user/users-list/users-list.component';
import { UserDetailComponent } from './_components/user/user-detail/user-detail.component';
import { MyProfileComponent } from './_components/user/my-profile/my-profile.component';
import { UserWatchlistsComponent } from './_components/user/user-watchlists/user-watchlists.component';
import { FriendshipRequestListComponent } from './_components/friendship/friendship-request-list/friendship-request-list.component';
import { FriendsListComponent } from './_components/friendship/friends-list/friends-list.component';
import { FILMTYPE } from './_constants/filmTypeConst';


export const AppRoutes: Routes = [
	{
		path: '',
		redirectTo: 'home',
		pathMatch: 'full',
	},
	{
		path: '',
		component: FrontendPanelLayoutComponent,
		children: [
			{ path: 'home', component: HomeComponent },
			{
				path: 'film-list',
				component: FilmListComponent
			},
			{
				path: 'movie-list',
				component: FilmListComponent,
				data: {filmType: FILMTYPE.movie}

			},
			{
				path: 'tvshow-list',
				component: FilmListComponent,
				data: {filmType: FILMTYPE.tvShow}

			},
			{
				path: 'film/:id',
				component: FilmDetail
			},
			{
				path: 'watchlist-list',
				component: WatchlistListComponent
			},
			{
				path: 'watchlist/:id',
				component: WatchlistDetailComponent
			},
			{
				path: 'watchlist-editor',
				component: WatchlistEditorComponent
			},
			{
				path: 'watchlist-editor/:id',
				component: WatchlistEditorComponent
			},
			{
				path: 'user-list',
				component: UsersListComponent
			},
			{
				path: 'user/:id',
				component: UserDetailComponent
			},
			{
				path: 'friend-list',
				component: FriendsListComponent
			},
			{
				path: 'my-profile',
				component: MyProfileComponent
			},
			{
				path: 'user-watchlists',
				component: UserWatchlistsComponent
			},
			{
				path: 'friendship-request-list',
				component: FriendshipRequestListComponent
			}
		]
	},
	{
		path: 'admin',
		component: AdminPanelLayoutComponent,
		canActivate: [AuthGuard],
		children: [
			{
				path: 'film-list',
				component: AdminFilmListComponent,
				data: { roles: ['Admin', 'Moderator'] }
			},
			{
				path: 'film-editor',
				component: AdminFilmEditorComponent,
				data: { roles: ['Admin', 'Moderator'] }
			},
			{
				path: 'film-editor/:id',
				component: AdminFilmEditorComponent,
				data: { roles: ['Admin', 'Moderator'] }
			},
			{
				path: 'participant-list',
				component: AdminParticipantListComponent,
				data: { roles: ['Admin', 'Moderator'] }
			},
			{
				path: 'participant-editor',
				component: AdminParticipantEditorComponent,
				data: { roles: ['Admin', 'Moderator'] }
			},
			{
				path: 'participant-editor/:id',
				component: AdminParticipantEditorComponent,
				data: { roles: ['Admin', 'Moderator'] }
			},
			{
				path: 'user-list',
				component: AdminUserListComponent,
				data: { roles: ['Admin'] }
			},
			{
				path: 'user-editor/:id',
				component: AdminUserEditorComponent,
				data: { roles: ['Admin'] }
			}
		]
	},
	{
		path: 'auth',
		component: AuthLayoutComponent,
		children: [
			{
				path: 'login',
				component: LoginComponent
			}, {
				path: 'signup',
				component: RegisterComponent
			}
		]
	}
];
