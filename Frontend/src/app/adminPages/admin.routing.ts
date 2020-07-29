import { Routes } from '@angular/router';

import { ListComponent } from './List/List.component';
import { FilmEditorComponent } from './film-editor/film-editor.component';

export const AdminRoutes: Routes = [
{
	path: 'film-list',
	component: ListComponent
},
{
	path: 'film-editor',
	component: FilmEditorComponent
},
{
	path: 'film-editor/:id',
	component: FilmEditorComponent
}];
