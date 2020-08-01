import { Routes } from '@angular/router';


import { AuthGuard } from './_guards/auth.guard';
import { FrontendPanelLayoutComponent } from './_components/layouts/frontend-panel/frontend-panel.component';
import { AdminPanelLayoutComponent } from './_components/layouts/admin-panel/admin-panel-layout.component';
import { AuthLayoutComponent } from './_components/layouts/auth/auth-layout.component';


export const AppRoutes: Routes = [
	{
		path: '',
		redirectTo: 'home',
		pathMatch: 'full',
	},
	{
		path: '',
		component: FrontendPanelLayoutComponent,
		children: [{
			path: 'home',
			loadChildren: () => import('./_components/dashboard/dashboard.module').then(m => m.DashboardModule)
		},
		{
			path: 'listing',
			loadChildren: () => import('./_components/listing/listing.module').then(m => m.ListingModule)
		},
		{
			path: 'pages',
			loadChildren: () => import('./_components/pages/pages.module').then(m => m.PagesModule)
		}]
	},
	{
		path: 'admin',
		component: AdminPanelLayoutComponent,
		canActivate: [AuthGuard],
		data: {roles: ['Admin', 'Moderator']},
		children: [{
			path: '',
			loadChildren: () => import('./_components/admin/admin.module').then(m => m.AdminModule)
		}]
	},
	{
		path: 'session',
		component: AuthLayoutComponent,		
		children: [{
			path: '',
			loadChildren: () => import('./_components/auth/session.module').then(m => m.SessionModule)
		}]
	}
];
