import { Routes } from '@angular/router';

import { FilmListComponent } from './film/film-list/film-list.component';
import { FilmEditorComponent } from './film/film-editor/film-editor.component';
import { ParticipantListComponent } from './participant/participant-list/participant-list.component';
import { ParticipantEditorComponent } from './participant/participant-editor/participant-editor.component';
import { UserListComponent } from './user/user-list/user-list.component';
import { UserEditorComponent } from './user/user-editor/user-editor.component';

export const AdminRoutes: Routes = [
	{
		path: 'film-list',
		component: FilmListComponent
	},
	{
		path: 'film-editor',
		component: FilmEditorComponent
	},
	{
		path: 'film-editor/:id',
		component: FilmEditorComponent
	},
	{
		path: 'participant-list',
		component: ParticipantListComponent
	},
	{
		path: 'participant-editor',
		component: ParticipantEditorComponent
	},
	{
		path: 'participant-editor/:id',
		component: ParticipantEditorComponent
	},
	{
		path: 'user-list',
		component: UserListComponent
	},
	{
		path: 'user-editor/:id',
		component: UserEditorComponent
	},
];
